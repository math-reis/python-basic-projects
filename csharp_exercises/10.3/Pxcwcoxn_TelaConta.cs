using System;
using System.Collections.Generic;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcscoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcwcoxn
{
    class MinhaTela : AplicacaoTela
    {
        public MinhaTela(String caminho)
            : base(caminho)
        { }

        public void Executar()
        {
            //...
            try
            {
                //...
                Menu menu = new Menu(
                new ItemMenu[] {
                        new ItemMenu(new KeyValuePair<int,string>(1, "Criar Conta"), Incluir, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Excluir Conta"), Excluir, false),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Lista contas do cliente"), Listar, false),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Depositar/Sacar"), DepositarSacar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(5, "Alterar limite"), Alterar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(0, "Sair"), null, true)
                        }, null);
                Console.ForegroundColor = ConsoleColor.White;
                Tela.ControlaMenu("Conta", menu);
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}\nTecle algo...", e.Message);
                Console.ReadKey();
            }
        }

        void Incluir(object obj)
        {
            try
            {
                #region Seleciona o cliente desejado
                Retorno<TOCliente> retornoSelecao = SelecionarCliente();
                if (!retornoSelecao.Ok)
                {
                    Console.WriteLine("Erro na consulta cliente: {0}", retornoSelecao.Mensagem);
                    return;
                }
                TOCliente toClienteSelecionado = retornoSelecao.Dados;
                if (toClienteSelecionado == null)
                {
                    Console.WriteLine("Nenhum cliente selecionado...");
                    return;
                }
                #endregion

                RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                TOConta toConta = new TOConta();

                #region Solicita dados da conta
                //---- utiliza dados do cliente selecionado
                toConta.CodCliente = toClienteSelecionado.CodCliente;
                toConta.TipoPessoa = toClienteSelecionado.TipoPessoa;
                //---- popula demais infos da conta
                toConta.CodAgencia = Tela.Ler<Int32>("Informe o conteúdo para cod_agencia: ");
                toConta.CodEspecie = Tela.Ler<Int32>("Informe o conteúdo para cod_especie: ");
                toConta.CodConta = Tela.Ler<Int32>("Informe o conteúdo para cod_conta: ");
                //---- Verifica se cont vai ter limite
                if (Tela.Confirma("Deseja adicionar um limite para a sua conta? "))
                {
                    toConta.Limite = Tela.Ler<Double>("Informe o conteúdo para limite: ");
                }
                else
                {
                    //---- certifica-se que a conta NAO tera limite movendo NULL para o campo
                    toConta.Limite = new CampoTabela<double>(null);
                }
                //---- Saldo inicial da conta deve ser zero
                toConta.Saldo = 0; 
                #endregion

                Retorno<Int32> retIncluir = rnConta.Incluir(toConta);
                if (!retIncluir.Ok)
                {
                    Console.WriteLine("Erro na inclusão: {0}", retIncluir.Mensagem);
                }
                else
                {
                    Console.WriteLine(retIncluir.Mensagem.ToString());
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        void Listar(object obj)
        {
            #region Seleciona o cliente desejado
            Retorno<TOCliente> retornoSelecao = SelecionarCliente();
            if (!retornoSelecao.Ok)
            {
                Console.WriteLine("Erro na consulta cliente: {0}", retornoSelecao.Mensagem);
                return;
            }
            TOCliente toClienteSelecionado = retornoSelecao.Dados;
            if (toClienteSelecionado == null)
            {
                Console.WriteLine("Nenhum cliente selecionado...");
                return;
            }
            #endregion

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toContaFiltro = new TOConta();
            //----- utiliza o cliente selecionado como filtrooooo
            toContaFiltro.CodCliente = toClienteSelecionado.CodCliente;
            toContaFiltro.TipoPessoa = toClienteSelecionado.TipoPessoa;
            Retorno<List<TOConta>> retListar = rnConta.Listar(toContaFiltro);
            if (!retListar.Ok)
            {
                Console.WriteLine(retListar.Mensagem);
                return;
            }
            ImprimeLista("Lista\n", retListar.Dados, true);
        }

        TOConta ImprimeLista(String titulo, List<TOConta> listaContas, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[7];
            cabecalho[1] = new CabecalhoLista("CODCLIENTE");
            cabecalho[0] = new CabecalhoLista("CODAGENCIA");
            cabecalho[2] = new CabecalhoLista("CODCONTA");
            cabecalho[3] = new CabecalhoLista("CODESPECIE");
            cabecalho[4] = new CabecalhoLista("LIMITE");
            cabecalho[5] = new CabecalhoLista("SALDO");
            cabecalho[6] = new CabecalhoLista("TIPOPESSOA");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOConta toConta in listaContas)
            {
                LinhaLista linha = new LinhaLista();
                if (toConta.TipoPessoa == "F")
                {
                    linha.Celulas.Add(string.Format(formatador, "{0:cpf}",
                        toConta.CodCliente.LerConteudoOuPadrao()));
                }
                else
                {
                    linha.Celulas.Add(string.Format(formatador, "{0:cnpj}", 
                        toConta.CodCliente.LerConteudoOuPadrao()));
                }

                linha.Celulas.Add(toConta.CodAgencia.ToString());
                linha.Celulas.Add(toConta.CodConta.ToString());
                linha.Celulas.Add(toConta.CodEspecie.ToString());
                if (!toConta.Limite.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toConta.Limite.ToString());
                }
                linha.Celulas.Add(toConta.Saldo.ToString());
                linha.Celulas.Add(toConta.TipoPessoa.ToString());
                registros.Add(linha);
            }
            if (paginacao)
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros, 20);
            }
            else
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros);
            }
            if (itemSelecionado >= 0)
            {
                return listaContas[itemSelecionado];
            }
            return null;
        }

        TOCliente ImprimeLista(String titulo, List<TOCliente> listaClientes, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[3];
            cabecalho[0] = new CabecalhoLista("CODCLIENTE");
            cabecalho[1] = new CabecalhoLista("TIPOPESSOA");
            cabecalho[2] = new CabecalhoLista("NOMECLIENTE");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOCliente toCliente in listaClientes)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(toCliente.CodCliente.ToString());
                linha.Celulas.Add(toCliente.TipoPessoa.ToString());
                linha.Celulas.Add(toCliente.NomeCliente.ToString());

                registros.Add(linha);
            }
            if (paginacao)
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros, 12);
            }
            else
            {
                itemSelecionado = Tela.ImprimeLista(titulo, cabecalho, registros);
            }
            if (itemSelecionado >= 0)
            {
                return listaClientes[itemSelecionado];
            }
            return null;
        }

        /// <summary>
        /// Retorna TRUE se consultou cliente com sucesso
        ///          ===> dados com o cliente selecionado (se nao foi selecionado, retorna NULL)
        /// retorno FALSE em caso de falha
        /// </summary>
        /// <returns></returns>
        Retorno <TOCliente> SelecionarCliente()
        {
            TOCliente toClienteFiltro = new TOCliente();
            if (Tela.Confirma("Deseja filtrar por CPF/CNPJ? "))
            {
                //---filtrar por codcli
                toClienteFiltro.CodCliente = Tela.Ler<double>("Informe CPF/CNPJ: ");
            }
            else if (Tela.Confirma("Deseja filtrar por Nome do Cliente?"))
            {
                //---- filtrar por nome
                toClienteFiltro.NomeCliente = Tela.Ler<string>("Informe o nome: ");
            }
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            Retorno<List<TOCliente>> listagemCliente = rnCliente.Listar(toClienteFiltro);
            if (!listagemCliente.Ok)
            {
                return this.Infra.RetornarFalha<TOCliente>(listagemCliente.Mensagem);
            }
            TOCliente toClienteSelecionado = ImprimeLista("Clientes encontrados", listagemCliente.Dados, true);
            return this.Infra.RetornarSucesso<TOCliente>(toClienteSelecionado, new OperacaoRealizadaMensagem());
        }

        void Alterar(object obj)
        {
            try
            {
                #region Seleciona o cliente desejado
                Retorno<TOCliente> retornoSelecao = SelecionarCliente();
                if (!retornoSelecao.Ok)
                {
                    Console.WriteLine("Erro na consulta cliente: {0}", retornoSelecao.Mensagem);
                    return;
                }
                TOCliente toClienteSelecionado = retornoSelecao.Dados;
                if (toClienteSelecionado == null)
                {
                    Console.WriteLine("Nenhum cliente selecionado...");
                    return;
                }
                #endregion

                RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                TOConta toContaFiltro = new TOConta();
                //---- montar filtro com os dados do cliente selecionado
                toContaFiltro.CodCliente = toClienteSelecionado.CodCliente;
                toContaFiltro.TipoPessoa = toClienteSelecionado.TipoPessoa;
                Retorno<List<TOConta>> retListar = rnConta.Listar(toContaFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }

                TOConta toContaSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para alterar", retListar.Dados, true);
                if (toContaSelecionado != null)
                {
                    TOConta toConta = new TOConta();
                    //popula os campos da PK
                    toConta.CodConta = toContaSelecionado.CodConta;
                    toConta.CodEspecie = toContaSelecionado.CodEspecie;
                    toConta.CodAgencia = toContaSelecionado.CodAgencia;
                    //-----permite alterr APENAS o limite
                    if (Tela.Confirma("Deseja retirar o limite da conta?"))
                    {
                        //---nulificar o campo
                        toConta.Limite = new CampoTabela<double>(null);
                    }
                    else
                    {
                        toConta.Limite = Tela.Ler<Double>("Informe o conteúdo para limite: ");
                    }

                    Retorno<Int32> retAlterar = rnConta.Alterar(toConta);
                    if (!retAlterar.Ok)
                    {
                        Console.WriteLine("Erro na alteração: {0}", retAlterar.Mensagem);
                    }
                    else
                    {
                        Console.WriteLine(retAlterar.Mensagem.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        void Excluir(object obj)
        {
            try
            {
                #region Seleciona o cliente desejado
                Retorno<TOCliente> retornoSelecao = SelecionarCliente();
                if (!retornoSelecao.Ok)
                {
                    Console.WriteLine("Erro na consulta cliente: {0}", retornoSelecao.Mensagem);
                    return;
                }
                TOCliente toClienteSelecionado = retornoSelecao.Dados;
                if (toClienteSelecionado == null)
                {
                    Console.WriteLine("Nenhum cliente selecionado...");
                    return;
                }
                #endregion

                RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                TOConta toContaFiltro = new TOConta();
                //--- utiliza o cliente selecionado no filtro
                toContaFiltro.CodCliente = toClienteSelecionado.CodCliente;
                toContaFiltro.TipoPessoa = toClienteSelecionado.TipoPessoa;
                Retorno<List<TOConta>> retListar = rnConta.Listar(toContaFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }
                TOConta toContaSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para excluir", retListar.Dados, true);
                if (toContaSelecionado != null)
                {
                    if (Tela.Confirma("Confirma a exclusão do conta?"))
                    {
                        Retorno<Int32> retExcluir = rnConta.Excluir(toContaSelecionado);
                        if (!retExcluir.Ok)
                        {
                            Console.WriteLine("Erro na exclusão: {0}", retExcluir.Mensagem);
                        }
                        else
                        {
                            Console.WriteLine(retExcluir.Mensagem.ToString());
                        }
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        void DepositarSacar(object obj)
        {
            char opcao = Tela.Confirma("Qual operacao deseja realizar, (S)aque ou (D)eposito? ", "SD");

            TOConta toConta = new TOConta();
            toConta.CodAgencia = Tela.Ler<Int32>("Informe o conteúdo para cod_agencia: ");
            toConta.CodEspecie = Tela.Ler<Int32>("Informe o conteúdo para cod_especie: ");
            toConta.CodConta = Tela.Ler<Int32>("Informe o conteúdo para cod_conta: ");
            toConta.ValorTransacao = Tela.Ler<double>("Informe o valor da transacao: ");

            if (!Tela.Confirma("Confirma a operacao na conta?"))
            {
                return;
            }

            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            Retorno<TOConta> retornoSaqueDeposito ;
            if (opcao == 'S')
            {
                retornoSaqueDeposito = rnConta.Sacar(toConta);
            }
            else
            {
                retornoSaqueDeposito = rnConta.Depositar(toConta);
            }
            if (!retornoSaqueDeposito.Ok)
            {
                Console.WriteLine("Erro no Saque/Deposito: {0}", retornoSaqueDeposito.Mensagem);
            }
            else
            {
                Console.WriteLine(retornoSaqueDeposito.Mensagem.ToString());
            }
        }
    }
}
