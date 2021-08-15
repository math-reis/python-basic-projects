using System;
using System.Collections.Generic;
using System.Text;
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
                /*
                    1. Criar conta
                    2. Excluir conta
                    3. Lista contas do cliente
                    4. Depositar/Sacar
                    5. Alterar limite
                 */
                Menu menu = new Menu(
                 new ItemMenu[] {
                        new ItemMenu(new KeyValuePair<int,string>(1, "Criar conta"), Incluir, false),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Excluir conta"), Excluir, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Listar contas do cliente"), Listar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Depositar/sacar"), DepositarSacar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(5, "Alterar limite"), Alterar, false),
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

        private void DepositarSacar(object parametro)
        {
            RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
            TOConta toConta = new TOConta();
            Retorno<TOConta> retAlterar;
            toConta.CodAgencia = Tela.Ler<Int32>("Informe a agência: ");
            toConta.CodConta = Tela.Ler<Int32>("Informe a conta: ");
            toConta.CodEspecie = Tela.Ler<Int32>("Informe a espécie: ");
            if (Tela.Confirma("Deseja <s>acar ou <d>epositar? ", "SD") == 'S')
            {
                toConta.ValorTransacao = Tela.Ler<Double>("Informe o valor de saque: ");
                retAlterar = rnConta.Sacar(toConta);
            }else
            {
                toConta.ValorTransacao = Tela.Ler<Double>("Informe o valor de depósito: ");
                retAlterar = rnConta.Depositar(toConta);
            }
            if (!retAlterar.Ok)
            {
                Console.WriteLine("Erro na alteração: {0}", retAlterar.Mensagem);
            }
            else
            {
                Console.WriteLine(retAlterar.Mensagem.ToString());
                Console.WriteLine("Saldo atual {0:N}", retAlterar.Dados.Saldo.LerConteudoOuPadrao());
            }
        }

        void Incluir(object obj)
        {
            try
            {
                TOCliente toCliente = ListarCliente();
                if (toCliente != null)
                {
                    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                    TOConta toConta = new TOConta();
                    //TODO: ler campos da tabela
                    toConta.TipoPessoa = toCliente.TipoPessoa;
                    toConta.CodCliente = toCliente.CodCliente;
                    toConta.CodAgencia = Tela.Ler<Int32>("Informe a agência: ");
                    toConta.CodConta = Tela.Ler<Int32>("Informe a conta: ");
                    toConta.CodEspecie = Tela.Ler<Int32>("Informe a espécie: ");
                    toConta.Limite = Tela.Ler<Double>("Informe o limite: ");
                    toConta.Saldo = 0;
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
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }

        #region Lista de clientes
        TOCliente ListarCliente()
        {
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toClienteFiltro = new TOCliente();
            String texto;
            String opcao = Tela.Confirma("Deseja listar <t>odos os clientes ou algum <e>specífico ou por <n>ome? ", "TEN").ToString();
            if (opcao == "E")
            {
                toClienteFiltro.TipoPessoa = Tela.Confirma("Informe o tipo de pessoa <F/J>: ", "FJ").ToString();
                if (toClienteFiltro.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    texto = "Informe o CPF: ";
                }
                else
                {
                    texto = "Informe o CNPJ: ";
                }
                toClienteFiltro.CodCliente = Tela.Ler<Double>(texto);
            }
            else if (opcao == "N")
            {
                toClienteFiltro.NomeCliente = Tela.Ler<String>("Informe o nome do cliente: ");
            }
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
            if (!retListar.Ok)
            {
                Console.WriteLine(retListar.Mensagem);
                return null;
            }
            return ImprimeListaCliente("Lista de clientes\n", retListar.Dados, true);
        }

        TOCliente ImprimeListaCliente(String titulo, List<TOCliente> listaClientes, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[5];
            cabecalho[0] = new CabecalhoLista("CPF/CNPJ", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[1] = new CabecalhoLista("Data Nasc.");
            cabecalho[2] = new CabecalhoLista("Nome do cliente");
            cabecalho[3] = new CabecalhoLista("Renda Familiar", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[4] = new CabecalhoLista("Telefone");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOCliente toCliente in listaClientes)
            {
                LinhaLista linha = new LinhaLista();
                String texto;
                if (toCliente.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    texto = String.Format(formatador, "{0:cpf}", toCliente.CodCliente.LerConteudoOuPadrao());
                }
                else
                {
                    texto = String.Format(formatador, "{0:cnpj}", toCliente.CodCliente.LerConteudoOuPadrao());
                }
                linha.Celulas.Add(texto);
                if (!toCliente.DataNasc.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.DataNasc.LerConteudoOuPadrao().ToString("dd/MM/yyyy"));
                }
                linha.Celulas.Add(toCliente.NomeCliente.ToString());
                if (!toCliente.RendaFamiliar.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.RendaFamiliar.LerConteudoOuPadrao().ToString("N"));
                }
                if (!toCliente.Telefone.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.Telefone.ToString());
                }
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
                return listaClientes[itemSelecionado];
            }
            return null;
        }
        #endregion

        void Listar(object obj)
        {
            TOCliente toCliente = ListarCliente();
            if (toCliente != null)
            {
                RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                TOConta toContaFiltro = new TOConta();
                //campos de filtro para listagem
                toContaFiltro.TipoPessoa = toCliente.TipoPessoa;
                toContaFiltro.CodCliente = toCliente.CodCliente;

                Retorno<List<TOConta>> retListar = rnConta.Listar(toContaFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                    return;
                }
                ImprimeLista("Lista\n", retListar.Dados, true);
            }
        }

        TOConta ImprimeLista(String titulo, List<TOConta> listaContas, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[6];
            cabecalho[0] = new CabecalhoLista("Agência", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[1] = new CabecalhoLista("Espécie", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[2] = new CabecalhoLista("Conta", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[3] = new CabecalhoLista("Limite", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[4] = new CabecalhoLista("Saldo", CabecalhoLista.AlinhamentoCelula.Direita);
            cabecalho[5] = new CabecalhoLista("Nome do cliente");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOConta toConta in listaContas)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(
                    String.Format(formatador, "{0:agencia}",
                    toConta.CodAgencia.LerConteudoOuPadrao())
                    );
                linha.Celulas.Add(toConta.CodEspecie.ToString());
                linha.Celulas.Add(toConta.CodConta.ToString());
                if (!toConta.Limite.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toConta.Limite.LerConteudoOuPadrao().ToString("N"));
                }
                linha.Celulas.Add(toConta.Saldo.LerConteudoOuPadrao().ToString("N"));
                linha.Celulas.Add(toConta.NomeCliente.LerConteudoOuPadrao());
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

        void Alterar(object obj)
        {
            try
            {
                TOCliente toCliente = ListarCliente();
                if (toCliente != null)
                {
                    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                    TOConta toContaFiltro = new TOConta();
                    //filtro para listagem
                    toContaFiltro.TipoPessoa = toCliente.TipoPessoa;
                    toContaFiltro.CodCliente = toCliente.CodCliente;
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
                        //ler os campos que serão alterados na tabela
                        toConta.TipoPessoa = toCliente.TipoPessoa;
                        toConta.CodCliente = toCliente.CodCliente;
                        toConta.Limite = Tela.Ler<Double>("Informe o conteúdo para limite: ");
                        if (toConta.Limite == 0)
                        {
                            if (Tela.Confirma("Deseja remover o limite da conta?"))
                            {
                                ////=0 => foisetado=true; temconteudo=true; conteudo=0;
                                //toConta.Limite = 0;
                                ////new () => foisetado=false; temconteudo=false; conteudo=?null?
                                //toConta.Limite = new CampoTabela<double>();
                                ////new (null) => foisetado=true; temconteudo=false; conteudo=null
                                toConta.Limite = new CampoTabela<double>(null);
                            }
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
                TOCliente toCliente = ListarCliente();
                if (toCliente != null)
                {
                    RNConta rnConta = this.Infra.InstanciarRN<RNConta>();
                    TOConta toContaFiltro = new TOConta();
                    //filtro para listagem
                    toContaFiltro.TipoPessoa = toCliente.TipoPessoa;
                    toContaFiltro.CodCliente = toCliente.CodCliente;
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
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }
    }
}
