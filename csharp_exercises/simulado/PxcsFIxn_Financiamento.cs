using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.PxcqFIxn;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.PxcsFIxn
{
    /// <summary>
    /// Classe de acesso a tabela FINANCIAMENTO
    /// </summary>
    public class RNFinanciamento : AplicacaoRegraNegocio
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
                //TODO: regras de negócio
                BDFinanciamento bdFinanciamento = this.Infra.InstanciarBD<BDFinanciamento>();
                Retorno<List<TOFinanciamento>> retListar = bdFinanciamento.Listar(toFinanciamento);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOFinanciamento>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOFinanciamento>>(retListar.Dados, new OperacaoRealizadaMensagem());
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
                #region Validação de campos obrigatórios
                if (!toFinanciamento.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toFinanciamento.CodFinanciamento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_FINANCIAMENTO"));
                }
                if (!toFinanciamento.NumeroParcelas.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NUMERO_PARCELAS"));
                }
                if (!toFinanciamento.Situacao.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("SITUACAO"));
                }
                if (!toFinanciamento.TaxaJuro.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TAXA_JURO"));
                }
                if (!toFinanciamento.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toFinanciamento.ValorFinanciamento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("VALOR_FINANCIAMENTO"));
                }
                if (!toFinanciamento.ValorPresente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("VALOR_PRESENTE"));
                }
                #endregion

                #region Regras de negócio
                //RN1:
                TOCliente toCliente = new TOCliente();
                toFinanciamento.TipoPessoa = toCliente.TipoPessoa;
                if (toFinanciamento.TipoPessoa != null)
                {
                    MensagemFinanciamento m = new MensagemFinanciamento(TipoFalha.RN1);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                //RN2: Não é permitido incluir financiamento em que o número de parcelas seja menor que 12 e maior que 48.
                if (toFinanciamento.NumeroParcelas < 12 ||
                    toFinanciamento.NumeroParcelas > 48)
                {
                    MensagemFinanciamento m = new MensagemFinanciamento(TipoFalha.RN2);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                //RN3: Deve ser calculado o valor final do financiamento e o valor das parcelas que serão pagas.
                Double P = toFinanciamento.ValorPresente;
                Double J = toFinanciamento.TaxaJuro;
                Double T = toFinanciamento.NumeroParcelas;
                Double VF = P + ((P * J / 100) * T);
                Double VP = VF / T;
                toFinanciamento.ValorParcela = VP;
                toFinanciamento.ValorFinanciamento = VF;
                #endregion

                BDFinanciamento bdFinanciamento = this.Infra.InstanciarBD<BDFinanciamento>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdFinanciamento.Incluir(toFinanciamento);
                    if (!retIncluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retIncluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retIncluir.Dados, new OperacaoRealizadaMensagem("Inclusão"));
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

        /// <summary>
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toFinanciamento">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOFinanciamento toFinanciamento)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toFinanciamento.CodFinanciamento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_FINANCIAMENTO"));
                }
                #endregion

                #region Regras de negócio
                //RN4: A alteração só permite modificar o valor do campo situação.
                if (toFinanciamento.Situacao != "A" && 
                    toFinanciamento.Situacao != "P")
                {
                    MensagemFinanciamento m = new MensagemFinanciamento(TipoFalha.RN4);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                #endregion

                BDFinanciamento bdFinanciamento = this.Infra.InstanciarBD<BDFinanciamento>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdFinanciamento.Alterar(toFinanciamento);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retAlterar.Dados, new OperacaoRealizadaMensagem("Alteração"));
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
                #region Validação dos campos da chave primária
                if (!toFinanciamento.CodFinanciamento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_FINANCIAMENTO"));
                }
                #endregion
                //TODO: regras de negócio
                BDFinanciamento bdFinanciamento = this.Infra.InstanciarBD<BDFinanciamento>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdFinanciamento.Excluir(toFinanciamento);
                    if (!retExcluir.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retExcluir.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }
                return this.Infra.RetornarSucesso<Int32>(retExcluir.Dados, new OperacaoRealizadaMensagem("Exclusão"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<Int32>(new Mensagem(e));
            }
        }
        #endregion
    }
}
