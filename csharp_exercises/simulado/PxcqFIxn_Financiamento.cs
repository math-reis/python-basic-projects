using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.PxcqFIxn
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
                this.Sql.Comando.Append("COD_CLIENTE, ");
                this.Sql.Comando.Append("COD_FINANCIAMENTO, ");
                this.Sql.Comando.Append("NUMERO_PARCELAS, ");
                this.Sql.Comando.Append("SITUACAO, ");
                this.Sql.Comando.Append("TAXA_JURO, ");
                this.Sql.Comando.Append("TIPO_PESSOA, ");
                this.Sql.Comando.Append("VALOR_FINANCIAMENTO, ");
                this.Sql.Comando.Append("VALOR_PRESENTE ");
                this.Sql.Comando.Append("FROM FINANCIAMENTO");
                //Monta os campos de chave primária
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toFinanciamento);
                //Monta os demais campos da tabela
                this.MontarCampos(this.Sql.MontarCampoWhere, toFinanciamento);
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
                this.MontarCamposChave(this.Sql.MontarCampoInsert, toFinanciamento);
                this.MontarCampos(this.Sql.MontarCampoInsert, toFinanciamento);
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
                this.MontarCampos(this.Sql.MontarCampoSet, toFinanciamento);
                //montar campos da PK
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toFinanciamento);
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
                this.MontarCamposChave(this.Sql.MontarCampoWhere, toFinanciamento);

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
        private void MontarCamposChave(MontarCampo montagem, TOFinanciamento toFinanciamento)
        {
            montagem.Invoke("COD_FINANCIAMENTO", toFinanciamento.CodFinanciamento);
        }

        /// <summary>
        /// Campos não chave primária na tabela
        /// </summary>
        /// <param name="montagem">Método que será acionado na execução</param>
        /// <param name="toFinanciamento">Campos da tabela</param>
        private void MontarCampos(MontarCampo montagem, TOFinanciamento toFinanciamento)
        {
            montagem.Invoke("COD_CLIENTE", toFinanciamento.CodCliente);
            montagem.Invoke("NUMERO_PARCELAS", toFinanciamento.NumeroParcelas);
            montagem.Invoke("SITUACAO", toFinanciamento.Situacao);
            montagem.Invoke("TAXA_JURO", toFinanciamento.TaxaJuro);
            montagem.Invoke("TIPO_PESSOA", toFinanciamento.TipoPessoa);
            montagem.Invoke("VALOR_FINANCIAMENTO", toFinanciamento.ValorFinanciamento);
            montagem.Invoke("VALOR_PRESENTE", toFinanciamento.ValorPresente);
        }
        #endregion
    }
}
