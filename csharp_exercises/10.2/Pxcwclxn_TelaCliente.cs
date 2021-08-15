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
                //TODO: ler campos da tabela
                toCliente.CodCliente = Tela.Ler<Double>("Informe o conteúdo para cod_cliente: ");
                toCliente.DataNasc = Tela.Ler<DateTime>("Informe o conteúdo para data_nasc: ");
                toCliente.NomeCliente = Tela.Ler<String>("Informe o conteúdo para nome_cliente: ");
                toCliente.RendaFamiliar = Tela.Ler<Double>("Informe o conteúdo para renda_familiar: ");
                toCliente.Telefone = Tela.Ler<Double>("Informe o conteúdo para telefone: ");
                toCliente.TipoPessoa = Tela.Ler<String>("Informe o conteúdo para tipo_pessoa: ");
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

        TOCliente ImprimeLista(String titulo, List<TOCliente> listaClientes, Boolean paginacao)
        {
            Int32 itemSelecionado = -1;
            Formatador formatador = new Formatador();

            //TODO: incluir os campos do cabeçalho da lista
            CabecalhoLista[] cabecalho = new CabecalhoLista[4];
            cabecalho[0] = new CabecalhoLista("CODCLIENTE");
            cabecalho[1] = new CabecalhoLista("DATACADASTRO");
            cabecalho[2] = new CabecalhoLista("DATANASC");
            cabecalho[3] = new CabecalhoLista("NOMECLIENTE");
            List<LinhaLista> registros = new List<LinhaLista>();
            foreach (TOCliente toCliente in listaClientes)
            {
                LinhaLista linha = new LinhaLista();
                linha.Celulas.Add(toCliente.CodCliente.ToString());

                if (!toCliente.DataCadastro.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.DataCadastro.ToString());
                }
                if (!toCliente.DataNasc.TemConteudo)
                {
                    linha.Celulas.Add(String.Empty);
                }
                else
                {
                    linha.Celulas.Add(toCliente.DataNasc.ToString());
                }
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

        void Alterar(object obj)
        {
            try
            {
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();

                //--- Verificar se deseja filtrar por cpf/cnpj
                bool utilizarFiltro = Tela.Confirma("Deseja filtrar por cpf/cnpj: ");
                if (utilizarFiltro)
                {
                    toClienteFiltro.CodCliente = Tela.Ler<Double>("Informe o CPF/CNPJ: ");
                }

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
                    toCliente.DataNasc = Tela.Ler<DateTime>("Informe o conteúdo para data_nasc: ");
                    toCliente.NomeCliente = Tela.Ler<String>("Informe o conteúdo para nome_cliente: ");
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

                string tipoPessoa = Tela.Ler<string>("Informe o Tipo da Pessoa (F ou J): ");
                if (tipoPessoa != "F" && tipoPessoa != "J")
                {
                    //erro
                    Console.WriteLine("Tipo de pessoa inválida!!!!!!");
                    return;
                }
                if (tipoPessoa == "F")
                {
                    toClienteFiltro.CodCliente = Tela.Ler<double>("Informe o CPF: ");
                    toClienteFiltro.TipoPessoa = "F";
                }
                else
                {
                    toClienteFiltro.CodCliente = Tela.Ler<double>("Informe o CNPJ: ");
                    toClienteFiltro.TipoPessoa = "J";
                }

                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
                if (!retListar.Ok)
                {
                    Console.WriteLine(retListar.Mensagem);
                    return;
                }

                if (retListar.Dados.Count == 0)
                {
                    Console.WriteLine("Cliente não cadastrado.");
                    return;
                }

                TOCliente toClienteSelecionado = retListar.Dados[0];

                Console.WriteLine("Cliente encontrado:");
                Console.WriteLine("Cod_cliente: {0}", toClienteSelecionado.CodCliente);
                Console.WriteLine("Tipo_pessoa: {0}", toClienteSelecionado.TipoPessoa);
                Console.WriteLine("nome_cliente: {0}", toClienteSelecionado.NomeCliente);
                Console.WriteLine("telefone: {0}", toClienteSelecionado.Telefone);
                Console.WriteLine("renda_familiar: {0}", toClienteSelecionado.RendaFamiliar);

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
            catch (Exception e)
            {
                Console.Write("Erro {0}", e.Message);
                Console.ReadKey();
            }
        }
    }
}
