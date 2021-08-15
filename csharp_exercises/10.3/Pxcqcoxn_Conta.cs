using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcqcoxn
{
    /// <summary>
    /// Classe de acesso a tabela CONTA
    /// </summary>
    public class BDConta : AplicacaoDados
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toConta">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOConta>> Listar(TOConta toConta)
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
                this.Sql.Comando.Append("COD_AGENCIA, ");
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("COD_CONTA, ");
                this.Sql.Comando.Append("COD_ESPECIE, ");
                this.Sql.Comando.Append("LIMITE, ");
                this.Sql.Comando.Append("SALDO, ");
                this.Sql.Comando.Append("TIPO_PESSOA ");
                this.Sql.Comando.Append("FROM CONTA");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toConta);
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toConta);
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOConta>>(retListar.Mensagem);
                }
                List<TOConta> lista = new List<TOConta>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOConta toContaLinha = new TOConta();
                    toContaLinha.PopularRetorno(linha);
                    lista.Add(toContaLinha);
                }
                return this.Infra.RetornarSucesso<List<TOConta>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOConta>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toConta">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOConta toConta)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("INSERT INTO CONTA (");
                this.MontarCamposChave(this.Sql.MontarCampoInsert, toConta);
                this.MontarCampos(this.Sql.MontarCampoInsert, toConta);
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
        /// <param name="toConta">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOConta toConta)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("UPDATE CONTA SET ");
                this.MontarCampos(this.Sql.MontarCampoSet, toConta);
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toConta);
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
        /// <param name="toConta">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOConta toConta)
        {
            try
            {
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("DELETE FROM CONTA");
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toConta);

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
        /// <param name="toConta"></param>
        private void MontarCamposChave(MontarCampo montagem, TOConta toConta)
        {
            montagem.Invoke("COD_CONTA", toConta.CodConta);
            montagem.Invoke("COD_ESPECIE", toConta.CodEspecie);
            montagem.Invoke("COD_AGENCIA", toConta.CodAgencia);
        }

        /// <summary>
        /// Campos não chave primária na tabela
        /// </summary>
        /// <param name="montagem">Método que será acionado na execução</param>
        /// <param name="toConta">Campos da tabela</param>
        private void MontarCampos(MontarCampo montagem, TOConta toConta)
        {
            montagem.Invoke("COD_CLIENTE", toConta.CodCliente);
            montagem.Invoke("LIMITE", toConta.Limite);
            montagem.Invoke("SALDO", toConta.Saldo);
            montagem.Invoke("TIPO_PESSOA", toConta.TipoPessoa);
        }
        #endregion
    }
}
