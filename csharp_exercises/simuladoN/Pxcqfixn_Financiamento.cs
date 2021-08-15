using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcqfixn
{
    /// <summary>
    /// Classe de acesso a tabela FINANCIAMENTO
    /// </summary>
    public class BDFinanciamento : AplicacaoDados
    {
        #region Métodos
        /// <summary>
        /// Executa o comando de consulta na tabela
        /// </summary>
        /// <param name="toFinanciamento">Campos para pesquisa na tabela</param>
        /// <returns>Retorna a lista consultada</returns>
        public Retorno<List<TOFinanciamento>> Listar(TOFinanciamento toFinanciamento)
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
                this.Sql.Comando.Append("FIN.COD_CLIENTE, ");
                this.Sql.Comando.Append("FIN.COD_FINANCIAMENTO, ");
                this.Sql.Comando.Append("FIN.NUMERO_PARCELAS, ");
                this.Sql.Comando.Append("FIN.SITUACAO, ");
                this.Sql.Comando.Append("FIN.TAXA_JURO, ");
                this.Sql.Comando.Append("FIN.TIPO_PESSOA, ");
                this.Sql.Comando.Append("FIN.VALOR_FINANCIAMENTO, ");
                this.Sql.Comando.Append("FIN.VALOR_PRESENTE, ");
                this.Sql.Comando.Append("CLI.NOME_CLIENTE ");
                this.Sql.Comando.Append("FROM FINANCIAMENTO FIN INNER JOIN CLIENTE CLI ");
                this.Sql.Comando.Append("ON CLI.COD_CLIENTE = FIN.COD_CLIENTE AND ");
                this.Sql.Comando.Append("CLI.TIPO_PESSOA = FIN.TIPO_PESSOA");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toFinanciamento, "FIN.");
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toFinanciamento, "FIN.");
                //Executa a consulta na tabela
                Retorno<List<Linha>> retListar = this.Consultar();
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOFinanciamento>>(retListar.Mensagem);
                }
                List<TOFinanciamento> lista = new List<TOFinanciamento>();
                foreach (Linha linha in retListar.Dados)
                {
                    TOFinanciamento toFinanciamentoLinha = new TOFinanciamento();
                    toFinanciamentoLinha.PopularRetorno(linha);
                    lista.Add(toFinanciamentoLinha);
                }
                return this.Infra.RetornarSucesso<List<TOFinanciamento>>(lista, new OperacaoRealizadaMensagem());
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<List<TOFinanciamento>>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de inclusão na tabela
        /// </summary>
        /// <param name="toFinanciamento">Campos para inclusão</param>
        /// <returns>Retorna a quantidade de registros incluídos</returns>
        public Retorno<Int32> Incluir(TOFinanciamento toFinanciamento)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("INSERT INTO FINANCIAMENTO (");
                this.MontarCamposChave(this.Sql.MontarCampoInsert, toFinanciamento, String.Empty);
                this.MontarCampos(this.Sql.MontarCampoInsert, toFinanciamento, String.Empty);
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
        /// <param name="toFinanciamento">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOFinanciamento toFinanciamento)
        {
            try
            {
                //Limpa o comando SQL
                this.Sql.Comando.Length = 0;
                //Limpa o comando SQL temporário
                this.Sql.Temporario.Length = 0;
                //Limpa os parâmatros do comando
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("UPDATE FINANCIAMENTO SET ");
                this.MontarCampos(this.Sql.MontarCampoSet, toFinanciamento, String.Empty);
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toFinanciamento, String.Empty);
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
        /// <param name="toFinanciamento">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOFinanciamento toFinanciamento)
        {
            try
            {
                this.Sql.Comando.Length = 0;
                this.Sql.Temporario.Length = 0;
                this.Sql.Parametros.Clear();
                this.Sql.Comando.Append("DELETE FROM FINANCIAMENTO");
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toFinanciamento, String.Empty);

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
        /// <param name="toFinanciamento"></param>
        /// <param name="alias">Álias da tabela</param>
        private void MontarCamposChave(MontarCampo montagem, TOFinanciamento toFinanciamento, String alias)
        {
            montagem.Invoke(alias + "COD_FINANCIAMENTO", toFinanciamento.CodFinanciamento);
        }

        /// <summary>
        /// Campos não chave primária na tabela
        /// </summary>
        /// <param name="montagem">Método que será acionado na execução</param>
        /// <param name="toFinanciamento">Campos da tabela</param>
        /// <param name="alias">Álias da tabela</param>
        private void MontarCampos(MontarCampo montagem, TOFinanciamento toFinanciamento, String alias)
        {
            montagem.Invoke(alias + "COD_CLIENTE", toFinanciamento.CodCliente);
            montagem.Invoke(alias + "NUMERO_PARCELAS", toFinanciamento.NumeroParcelas);
            montagem.Invoke(alias + "SITUACAO", toFinanciamento.Situacao);
            montagem.Invoke(alias + "TAXA_JURO", toFinanciamento.TaxaJuro);
            montagem.Invoke(alias + "TIPO_PESSOA", toFinanciamento.TipoPessoa);
            montagem.Invoke(alias + "VALOR_FINANCIAMENTO", toFinanciamento.ValorFinanciamento);
            montagem.Invoke(alias + "VALOR_PRESENTE", toFinanciamento.ValorPresente);
            if (montagem == this.Sql.MontarCampoWhere)
            {
                if (toFinanciamento.NomeCliente.TemConteudo)
                {
                    toFinanciamento.NomeCliente += "%";
                }
                this.Sql.MontarCampoWhere("CLI.NOME_CLIENTE", toFinanciamento.NomeCliente, ConstrutorSql.OperadorUnario.Like);
            }
        }
        #endregion
    }
}
