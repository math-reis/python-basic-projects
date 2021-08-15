using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcqclxn
{
    /// <summary>
    /// Classe de acesso a tabela CLIENTE
    /// </summary>
    public class BDCliente : AplicacaoDados
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toCliente">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOCliente>> Listar(TOCliente toCliente)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("SELECT ");
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("DATA_ATU_RATING, ");
                this.Sql.Comando.Append("DATA_CADASTRO, ");
                this.Sql.Comando.Append("DATA_NASC, ");
                this.Sql.Comando.Append("NOME_CLIENTE, ");
                this.Sql.Comando.Append("RATING_CLIENTE, ");
                this.Sql.Comando.Append("RENDA_FAMILIAR, ");
                this.Sql.Comando.Append("TELEFONE, ");
                this.Sql.Comando.Append("TIPO_PESSOA ");
                this.Sql.Comando.Append("FROM CLIENTE");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toCliente);
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toCliente);
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOCliente>>(retListar.Mensagem);
                }
                List<TOCliente> lista = new List<TOCliente>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOCliente toClienteLinha = new TOCliente();
                    toClienteLinha.PopularRetorno(linha);
                    lista.Add(toClienteLinha);
                }
                return this.Infra.RetornarSucesso<List<TOCliente>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOCliente>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toCliente">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOCliente toCliente)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("INSERT INTO CLIENTE (");
                this.MontarCamposChave(this.Sql.MontarCampoInsert, toCliente);
                this.MontarCampos(this.Sql.MontarCampoInsert, toCliente);
                this.Sql.Comando.Append(") VALUES (");
                this.Sql.Comando.Append(this.Sql.Temporario.ToString());
                this.Sql.Comando.Append(")");
                Retorno<Int32> retExecutar = this.Executar();
                if (!retExecutar.Ok)
                {
                    return retExecutar;
                }
                return this.Infra.RetornarSucesso<Int32>(retExecutar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toCliente">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOCliente toCliente)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("UPDATE CLIENTE SET ");
                this.MontarCampos(this.Sql.MontarCampoSet, toCliente);
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toCliente);
                Retorno<Int32> retExecutar = this.Executar();
                if (!retExecutar.Ok)
                {
                    return retExecutar;
                }
                if (retExecutar.Dados == 0)
                {
                    return this.Infra.RetornarFalha<Int32>(new RegistroInexistenteMensagem());
                }
                return this.Infra.RetornarSucesso<Int32>(retExecutar.Dados, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de exclusão na tabela
        /// </summary>
        /// <param name="toCliente">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOCliente toCliente)
        {
            try
            {
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("DELETE FROM CLIENTE");
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toCliente);

                Retorno<Int32> retExecutar = this.Executar();
                if (!retExecutar.Ok)
                {
                    return retExecutar;
                }
                if (retExecutar.Dados == 0)
                {
                    return this.Infra.RetornarFalha<Int32>(new RegistroInexistenteMensagem());
                }
                else
                {
                    return this.Infra.RetornarSucesso<Int32>(retExecutar.Dados, new OperacaoRealizadaMensagem());
                }
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Campos de chave primária da tabela
        /// </summary>
        /// <param name="montagem"></param>
        /// <param name="toCliente"></param>
        private void MontarCamposChave(MontarCampo montagem, TOCliente toCliente)
        {
            montagem.Invoke("TIPO_PESSOA", toCliente.TipoPessoa);
            montagem.Invoke("COD_CLIENTE", toCliente.CodCliente);
        }

        /// <summary>
        /// Campos não chave primária na tabela
        /// </summary>
        /// <param name="montagem">Método que será acionado na execução</param>
        /// <param name="toCliente">Campos da tabela</param>
        private void MontarCampos(MontarCampo montagem, TOCliente toCliente)
        {
            montagem.Invoke("DATA_ATU_RATING", toCliente.DataAtuRating);
            montagem.Invoke("DATA_CADASTRO", toCliente.DataCadastro);
            montagem.Invoke("DATA_NASC", toCliente.DataNasc);
            montagem.Invoke("NOME_CLIENTE", toCliente.NomeCliente);
            montagem.Invoke("RATING_CLIENTE", toCliente.RatingCliente);
            montagem.Invoke("RENDA_FAMILIAR", toCliente.RendaFamiliar);
            montagem.Invoke("TELEFONE", toCliente.Telefone);
        }
        #endregion
    }
}
