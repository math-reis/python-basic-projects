using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcsclxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcwclxn
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
                        new ItemMenu(new KeyValuePair<int,string>(1, "Listar"), Listar, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Incluir"), Incluir, false),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Alterar"), Alterar, false),
                        new ItemMenu(new KeyValuePair<int,string>(4, "Excluir"), Excluir, false, true),
                        new ItemMenu(new KeyValuePair<int,string>(0, "Sair"), null, true)
                        }, null);
                Console.ForegroundColor = ConsoleColor.White;
                Tela.ControlaMenu("Cliente", menu);
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
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toCliente = new TOCliente();
                String texto;
                //TODO: ler campos da tabela
                toCliente.TipoPessoa = Tela.Confirma("Informe o tipo de pessoa <F/J>: ", "FJ").ToString();
                if (toCliente.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    texto = "Informe o CPF: ";
                }else
                {
                    texto = "Informe o CNPJ: ";
                }
                toCliente.CodCliente = Tela.Ler<Double>(texto);
                //toCliente.DataAtuRating = Tela.Ler<DateTime>("Informe o conteúdo para data_atu_rating: ");
                //toCliente.DataCadastro = Tela.Ler<DateTime>("Informe o conteúdo para data_cadastro: ");
                //toCliente.DataNasc = Tela.Ler<DateTime>("Informe o conteúdo para data_nasc: ");
                toCliente.NomeCliente = Tela.Ler<String>("Informe o nome do cliente: ");
                //toCliente.RatingCliente = Tela.Ler<String>("Informe o conteúdo para rating_cliente: ");
                //toCliente.RendaFamiliar = Tela.Ler<Double>("Informe o conteúdo para renda_familiar: ");
                toCliente.Telefone = Tela.Ler<Double>("Informe o telefone: ");
                Retorno<Int32> retIncluir = rnCliente.Incluir(toCliente);
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
            RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
            TOCliente toClienteFiltro = new TOCliente();
            String texto;
            if (Tela.Confirma("Deseja listar <t>odos ou algum <e>specífico? ", "TE").ToString()=="E")
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
            Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
            if (!retListar.Ok)
            {
                Console.WriteLine(retListar.Mensagem);
                return;
            }
            ImprimeLista("Lista\n", retListar.Dados, true);
        }

        TOCliente ImprimeLista(String titulo, List<TOCliente> listaClientes, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[5];
            cabecalho[0] = new CabecalhoLista("CPF/CNPJ");
            cabecalho[1] = new CabecalhoLista("DATANASC");
            cabecalho[2] = new CabecalhoLista("Nome do cliente");
            cabecalho[3] = new CabecalhoLista("RENDAFAMILIAR");
            cabecalho[4] = new CabecalhoLista("TELEFONE");
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

        void Alterar(object obj)
        {
            try
            {
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();
                //TODO: incluir os campos de filtro para a listagem
                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }

                TOCliente toClienteSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para alterar", retListar.Dados, true);
                if (toClienteSelecionado != null)
                {
                    TOCliente toCliente = new TOCliente();
                    //popula os campos da PK
                    toCliente.TipoPessoa = toClienteSelecionado.TipoPessoa;
                    toCliente.CodCliente = toClienteSelecionado.CodCliente;
                    //TODO: ler os campos que serão alterados na tabela
                    toCliente.DataAtuRating = Tela.Ler<DateTime>("Informe o conteúdo para data_atu_rating: ");
                    toCliente.DataCadastro = Tela.Ler<DateTime>("Informe o conteúdo para data_cadastro: ");
                    toCliente.DataNasc = Tela.Ler<DateTime>("Informe o conteúdo para data_nasc: ");
                    toCliente.NomeCliente = Tela.Ler<String>("Informe o conteúdo para nome_cliente: ");
                    toCliente.RatingCliente = Tela.Ler<String>("Informe o conteúdo para rating_cliente: ");
                    toCliente.RendaFamiliar = Tela.Ler<Double>("Informe o conteúdo para renda_familiar: ");
                    toCliente.Telefone = Tela.Ler<Double>("Informe o conteúdo para telefone: ");
                    Retorno<Int32> retAlterar = rnCliente.Alterar(toCliente);
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
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();
                //TODO: incluir os campos de filtro para listagem
                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }
                TOCliente toClienteSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para excluir", retListar.Dados, true);
                if (toClienteSelecionado != null)
                {
                    if (Tela.Confirma("Confirma a exclusão do cliente?"))
                    {
                        Retorno<Int32> retExcluir = rnCliente.Excluir(toClienteSelecionado);
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
