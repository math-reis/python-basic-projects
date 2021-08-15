using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.PxcsFIxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.PxcwFIxn
{
    class MinhaTela : AplicacaoTela
    {
        public MinhaTela(String caminho)
            : base(caminho)
        { }

        public void Executar()
        {
            try
            {
                Menu menu = new Menu(
                new ItemMenu[] {
                        new ItemMenu(new KeyValuePair<int,string>(1, "Incluir financiamento"), Incluir, false),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Excluir financiamento"), Excluir, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Listar financiamento"), Listar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Aprovar financiamento"), Aprovar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(5, "Alterar financiamento"), Alterar, false),
                        new ItemMenu(new KeyValuePair<int,string>(0, "Sair"), null, true)
                        }, null);
                Console.ForegroundColor = ConsoleColor.White;
                Tela.ControlaMenu("Financiamento", menu);
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
                // RN1: Não é permitido incluir financiamento para pessoas físicas ou pessoa jurídica que não estejam cadastrados na tabela CLIENTE.
                TOCliente toCliente = ListarCliente();
                if (toCliente != null)
                {
                    RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
                    TOFinanciamento toFinanciamento = new TOFinanciamento();
                    toFinanciamento.CodCliente = toCliente.CodCliente;
                    toFinanciamento.TipoPessoa = toCliente.TipoPessoa;
                    toFinanciamento.CodCliente = Tela.Ler<Double>("Informe o Código do Cliente: ");
                    toFinanciamento.CodFinanciamento = Tela.Ler<Int32>("Informe o Código do Financiamento: ");
                    toFinanciamento.NumeroParcelas = Tela.Ler<Int32>("Informe o Número de Parcelas: ");
                    toFinanciamento.Situacao = Tela.Ler<String>("Informe a Situacao: ");
                    toFinanciamento.TaxaJuro = Tela.Ler<Double>("Informe a Taxa de Juro: ");
                    toFinanciamento.TipoPessoa = Tela.Ler<String>("Informe o Tipo de Pessoa: ");
                    toFinanciamento.ValorFinanciamento = Tela.Ler<Double>("Informe o Valor do Financiamento: ");
                    toFinanciamento.ValorPresente = Tela.Ler<Double>("Informe o Valor Presente: ");
                    Retorno<Int32> retIncluir = rnFinanciamento.Incluir(toFinanciamento);
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

        void Excluir(object obj)
        {
            try
            {
                RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
                TOFinanciamento toFinanciamentoFiltro = new TOFinanciamento();
                //TODO: incluir os campos de filtro para listagem
                Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamentoFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }
                TOFinanciamento toFinanciamentoSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para excluir", retListar.Dados, true);
                if (toFinanciamentoSelecionado != null)
                {
                    if (Tela.Confirma("Confirma a exclusão do financiamento?"))
                    {
                        Retorno<Int32> retExcluir = rnFinanciamento.Excluir(toFinanciamentoSelecionado);
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

        void Listar(object obj)
        {
            RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
            TOFinanciamento toFinanciamentoFiltro = new TOFinanciamento();
            //TODO: incluir os campos de filtro para listagem
            Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamentoFiltro);
            if (!retListar.Ok)
            {
                Console.WriteLine(retListar.Mensagem);
                return;
            }
            ImprimeLista("Lista\n", retListar.Dados, true);
        }

        TOFinanciamento ImprimeLista(String titulo, List<TOFinanciamento> listaFinanciamentos, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[8];
            cabecalho[0] = new CabecalhoLista("CODCLIENTE");
            cabecalho[1] = new CabecalhoLista("CODFINANCIAMENTO");
            cabecalho[2] = new CabecalhoLista("NUMEROPARCELAS");
            cabecalho[3] = new CabecalhoLista("SITUACAO");
            cabecalho[4] = new CabecalhoLista("TAXAJURO");
            cabecalho[5] = new CabecalhoLista("TIPOPESSOA");
            cabecalho[6] = new CabecalhoLista("VALORFINANCIAMENTO");
            cabecalho[7] = new CabecalhoLista("VALORPRESENTE");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOFinanciamento toFinanciamento in listaFinanciamentos)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(toFinanciamento.CodCliente.ToString());
                linha.Celulas.Add(toFinanciamento.CodFinanciamento.ToString());
                linha.Celulas.Add(toFinanciamento.NumeroParcelas.ToString());
                linha.Celulas.Add(toFinanciamento.Situacao.ToString());
                linha.Celulas.Add(toFinanciamento.TaxaJuro.ToString());
                linha.Celulas.Add(toFinanciamento.TipoPessoa.ToString());
                linha.Celulas.Add(toFinanciamento.ValorFinanciamento.ToString());
                linha.Celulas.Add(toFinanciamento.ValorPresente.ToString());
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
                return listaFinanciamentos[itemSelecionado];
            }
            return null;
        }

        void Aprovar(object obj)
        {

        } 

        void Alterar(object obj)
        {
            try
            {
                RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
                TOFinanciamento toFinanciamentoFiltro = new TOFinanciamento();
                //TODO: incluir os campos de filtro para a listagem
                //RN4: A alteração só permite modificar o valor do campo situação.

                toFinanciamentoFiltro.CodFinanciamento = Tela.Ler<Int32>("Informe o Código do Financiamento: ");




                Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamentoFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }

                TOFinanciamento toFinanciamentoSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para alterar", retListar.Dados, true);
                if (toFinanciamentoSelecionado != null)
                {
                    TOFinanciamento toFinanciamento = new TOFinanciamento();
                    //Popula os campos da PK.
                    toFinanciamento.CodFinanciamento = toFinanciamentoSelecionado.CodFinanciamento;
                    //TODO: ler os campos que serão alterados na tabela.
                    toFinanciamento.Situacao = Tela.Ler<String>("Informe o conteúdo para situacao: ");
                    Retorno<Int32> retAlterar = rnFinanciamento.Alterar(toFinanciamento);
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
    }
}
