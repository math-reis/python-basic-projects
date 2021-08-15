using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcsfixn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcwfixn
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
                        new ItemMenu(new KeyValuePair<int,string>(1, "Incluir financiamento"), Incluir, false),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Excluir financiamento"), Excluir, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Listar financiamento"), Listar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Aprovar financiamento"), Aprovar, false),
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

        private void Aprovar(object parametro)
        {
            try
            {
                RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
                TOFinanciamento toFinanciamentoFiltro = new TOFinanciamento();
                Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamentoFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }
                TOFinanciamento toFinanciamentoSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para realizar a APROVAÇÃO", retListar.Dados, true);
                if (toFinanciamentoSelecionado != null)
                {
                    if (Tela.Confirma("Confirma a aprovação do financiamento?"))
                    {
                        toFinanciamentoSelecionado.Situacao = "A";
                        Retorno<Int32> retAprovar = rnFinanciamento.Aprovar(toFinanciamentoSelecionado);
                        if (!retAprovar.Ok)
                        {
                            Console.WriteLine("Erro na aprovação: {0}", retAprovar.Mensagem);
                        }
                        else
                        {
                            Console.WriteLine(retAprovar.Mensagem.ToString());
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

        void Incluir(object obj)
        {
            try
            {
                RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
                TOFinanciamento toFinanciamento = new TOFinanciamento();
                //TODO: ler campos da tabela
                String texto;
                toFinanciamento.TipoPessoa = Tela.Confirma("Informe o tipo de pessoa <F/J>: ", "FJ").ToString();
                if (toFinanciamento.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    texto = "Informe o CPF: ";
                }
                else
                {
                    texto = "Informe o CNPJ: ";
                }
                toFinanciamento.CodCliente = Tela.Ler<Double>(texto);
                toFinanciamento.CodFinanciamento = Tela.Ler<Int32>("Informe o código do financiamento: ");
                toFinanciamento.NumeroParcelas = Tela.Ler<Int32>("Informe número parcelas: ");
                toFinanciamento.Situacao = "P";
                toFinanciamento.TaxaJuro = Tela.Ler<Double>("Informe a taxa juros: ");
                //toFinanciamento.ValorFinanciamento = Tela.Ler<Double>("Informe o conteúdo para valor_financiamento: ");
                toFinanciamento.ValorPresente = Tela.Ler<Double>("Informe o valor presente: ");
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
            //campos de filtro para listagem
            String opcao = Tela.Confirma("Deseja listar <t>odos, por <n>ome ou <c>ódigo? ", "TNC").ToString();
            switch (opcao)
            {
                case "N":
                    toFinanciamentoFiltro.NomeCliente = Tela.Ler<String>("Informe o nome do cliente: ");
                    break;
                case "C":
                    toFinanciamentoFiltro.CodFinanciamento= Tela.Ler<Int32>("Informe o código financiamento: ");
                    break;
                default:
                    break;
            }
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
            CabecalhoLista[] cabecalho = new CabecalhoLista[10];
            cabecalho[0] = new CabecalhoLista("CODCLIENTE");
            cabecalho[1] = new CabecalhoLista("CODFINANCIAMENTO");
            cabecalho[2] = new CabecalhoLista("NUMEROPARCELAS");
            cabecalho[3] = new CabecalhoLista("SITUACAO");
            cabecalho[4] = new CabecalhoLista("TAXAJURO");
            cabecalho[5] = new CabecalhoLista("TIPOPESSOA");
            cabecalho[6] = new CabecalhoLista("VALORFINANCIAMENTO");
            cabecalho[7] = new CabecalhoLista("VALORPRESENTE");

            cabecalho[8] = new CabecalhoLista("nome");
            cabecalho[9] = new CabecalhoLista("renda");
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

                linha.Celulas.Add(toFinanciamento.NomeCliente.ToString());
                linha.Celulas.Add(toFinanciamento.RendaFamiliar.ToString());
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

        void Alterar(object obj)
        {
            try
            {
                RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
                TOFinanciamento toFinanciamentoFiltro = new TOFinanciamento();
                toFinanciamentoFiltro.Situacao = "A";
                Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamentoFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }

                TOFinanciamento toFinanciamentoSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para alterar para PENDENTE", retListar.Dados, true);
                if (toFinanciamentoSelecionado != null)
                {
                    toFinanciamentoSelecionado.Situacao = "P";
                    Retorno<Int32> retAlterar = rnFinanciamento.AlterarFinanciamento(toFinanciamentoSelecionado);
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
    }
}
