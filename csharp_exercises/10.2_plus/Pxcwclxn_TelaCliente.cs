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
                        new ItemMenu(new KeyValuePair<int,string>(1, "Incluir"), Incluir, false),
                        new ItemMenu(new KeyValuePair<int,string>(2, "Consultar/alterar"), Alterar, false),
                        new ItemMenu(new KeyValuePair<int,string>(3, "Excluir"), Excluir, false, true),
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

                // Solicita e inclui os dados do cliente na tabela
                string texto;
                toCliente.TipoPessoa = Tela.Confirma("Informe o tipo de pessoa <F/J>: ", "FJ").ToString();
                if (toCliente.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    texto = "Informe o CPF: ";
                }
                else
                {
                    texto = "Informe o CNPJ: ";
                }
                toCliente.CodCliente = Tela.Ler<Double>(texto);
                toCliente.NomeCliente = Tela.Ler<String>("Informe o nome do cliente: ");
                toCliente.Telefone = Tela.Ler<Double>("Informe o telefone: ");
                //

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

        void Alterar(object obj)
        {
            try
            {
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();

                // Filtro de listagem // TOFIX
                string texto;
                if (Tela.Confirma("Deseja listar <t>odos os clientes ou algum em <e>specífico? ", "TE").ToString() == "E")
                {
                    if (toClienteFiltro.TipoPessoa.LerConteudoOuPadrao() == "E")
                    {
                        texto = "Informe o CPF ou o CNPJ: ";
                    }
                    else
                    {
                        texto = "Informe o CNPJ: ";
                    }
                    toClienteFiltro.CodCliente = Tela.Ler<Double>(texto);
                }
                //

                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                }

                TOCliente toClienteSelecionado = ImprimeLista("Selecione um item da lista e tecle ENTER para alterar", retListar.Dados, true);
                if (toClienteSelecionado != null)
                {
                    TOCliente toCliente = new TOCliente();
                    // Popula os campos da PK.
                    toCliente.TipoPessoa = toClienteSelecionado.TipoPessoa;
                    toCliente.CodCliente = toClienteSelecionado.CodCliente;
                    // Campos que serão alterados na tabela.
                    toCliente.NomeCliente = Tela.Ler<String>("Informe o nome do cliente: ");
                    toCliente.Telefone = Tela.Ler<Double>("Informe o telefone: ");

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

        TOCliente ImprimeLista(String titulo, List<TOCliente> listaClientes, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            // Campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[4];
            cabecalho[0] = new CabecalhoLista("CPF/CNPJ");
            cabecalho[1] = new CabecalhoLista("Tipo de Pessoa");
            cabecalho[2] = new CabecalhoLista("Nome do Cliente");
            cabecalho[3] = new CabecalhoLista("Telefone");
            //

            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOCliente toCliente in listaClientes)
            {
                // Cria, formata e adiciona as linhas da tabela.
                LinhaLista linha = new LinhaLista();
                string texto;
                if (toCliente.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    texto = string.Format(formatador, "{0:cpf}", toCliente.CodCliente.LerConteudoOuPadrao());
                }
                else
                {
                    texto = string.Format(formatador, "{0:cnpj}", toCliente.CodCliente.LerConteudoOuPadrao());
                }
                linha.Celulas.Add(texto);
                linha.Celulas.Add(toCliente.TipoPessoa.ToString());
                linha.Celulas.Add(toCliente.NomeCliente.ToString());
                if (!toCliente.Telefone.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.Telefone.ToString());
                }
                registros.Add(linha);
                //
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

        void Excluir(object obj)
        {
            try
            {
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();

                // Filtro de exclusão por CPF ou CNPJ.
                string texto;
                if (Tela.Confirma("Deseja excluir pessoa <f>ísica ou <j>urídica? ", "FJ").ToString() == "F")
                {
                    texto = "Insira o CPF: ";
                }
                else
                {
                    texto = "Insira o CNPJ: ";
                }
                toClienteFiltro.CodCliente = Tela.Ler<Double>(texto);
                //

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
