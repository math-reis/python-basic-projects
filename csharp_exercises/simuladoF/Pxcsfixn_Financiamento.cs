using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqfixn;
using Bergs.Pxc.Pxcsclxn;

namespace Bergs.Pxc.Pxcsfixn
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
                //if (!toFinanciamento.ValorFinanciamento.TemConteudo)
                //{
                //    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("VALOR_FINANCIAMENTO"));
                //}
                if (!toFinanciamento.ValorPresente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("VALOR_PRESENTE"));
                }
                #endregion
                //regras de negócio

                //RRN1
                //1. listar os clientes da base de dados
                //2. filtrar somente o cliente informado
                //3. se retornar erro ou ninguém na lista->erro, 
                // -então retornar mensagem
                // -senão, segue o baile
                RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                TOCliente toClienteFiltro = new TOCliente();
                toClienteFiltro.CodCliente = toFinanciamento.CodCliente;
                toClienteFiltro.TipoPessoa = toFinanciamento.TipoPessoa;
                Retorno<List<TOCliente>> retListar = rnCliente.Listar(toClienteFiltro);
                if (!retListar.Ok)
                {   //se deu erro, repasse o erro pra quem chamou
                    return this.Infra.RetornarFalha<Int32>(retListar.Mensagem);
                }
                if (retListar.Dados.Count == 0)
                {   //não encontrou ninguém na lista
                    return this.Infra.RetornarFalha<Int32>(
                        new MensagemFinanciamento(TipoFalha.FalhaClienteNaoEncontrado)
                        );
                }
                //fim-RRN1
                //RRN2
                //Não é permitido incluir financiamento em que o número de parcelas 
                //seja menor que 12 e maior que 48
                //1. Se o número de parcelas for menor que 12 ou maior que 48, 
                // - então: Sistema retorna a mensagem 
                //    "Número de parcelas do financiamento deve estar compreendido entre 12 e 48 parcelas, inclusive.”"
                if (toFinanciamento.NumeroParcelas.LerConteudoOuPadrao() < 12 ||
                    toFinanciamento.NumeroParcelas.LerConteudoOuPadrao() > 48)
                {
                    return this.Infra.RetornarFalha<Int32>(
                        new MensagemFinanciamento(TipoFalha.FalhaNumeroParcelas)
                        );
                }
                //fim-RRN2
                //RRN3
                //P = VALOR_PRESENTE(valor do empréstimo)
                //J = TAXA_JURO
                //T = NUMERO_PARCELAS
                //======================
                //VF = P + ((P * J / 100) * T)
                //VP = VF / T
                //Armazenar o VF no campo VALOR_FINANCIAMENTO.
                //O valor da parcela é o resultado de VP.
                Double p = toFinanciamento.ValorPresente.LerConteudoOuPadrao();
                Double j = toFinanciamento.TaxaJuro.LerConteudoOuPadrao();
                Double t = toFinanciamento.NumeroParcelas.LerConteudoOuPadrao();
                Double vf = p + ((p * j / 100) * t);
                toFinanciamento.ValorFinanciamento = vf;
                //fim-RRN3
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
        private Retorno<Int32> Alterar(TOFinanciamento toFinanciamento)
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
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toFinanciamento">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> AlterarFinanciamento(TOFinanciamento toFinanciamento)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toFinanciamento.CodFinanciamento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_FINANCIAMENTO"));
                }
                if (!toFinanciamento.Situacao.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(
                        new MensagemFinanciamento(TipoFalha.FalhaSituacao)
                        );
                }
                #endregion
                //regras de negócio
                BDFinanciamento bdFinanciamento = this.Infra.InstanciarBD<BDFinanciamento>();
                Retorno<Int32> retAlterar;
                //RRN4
                TOFinanciamento toFinanciamentoAlterar = new TOFinanciamento();
                toFinanciamentoAlterar.CodFinanciamento = toFinanciamento.CodFinanciamento;
                toFinanciamentoAlterar.Situacao = toFinanciamento.Situacao;
                //fim-RRN4
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdFinanciamento.Alterar(toFinanciamentoAlterar);
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
        /// Executa o comando de atualização na tabela
        /// </summary>
        /// <param name="toFinanciamento">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Aprovar(TOFinanciamento toFinanciamento)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toFinanciamento.CodFinanciamento.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_FINANCIAMENTO"));
                }
                #endregion
                //regras de negócio
                //buscar os dados da base antes,
                // pois recebemos somente o código do financiamento
                TOFinanciamento toFinanciamentoFiltro = new TOFinanciamento();
                toFinanciamentoFiltro.CodFinanciamento = toFinanciamento.CodFinanciamento;
                Retorno<List<TOFinanciamento>> retListar = this.Listar(toFinanciamentoFiltro);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(retListar.Mensagem);
                }
                //retListar.Dados[0] => é o meu financiamento guardado na base
                if (retListar.Dados.Count == 0)
                {
                    //não encontrou a PK (chave primária) na base de dados
                    return this.Infra.RetornarFalha<Int32>(new RegistroInexistenteMensagem());
                }
                TOFinanciamento toFinanciamentoBase = retListar.Dados[0];
                //RRN3
                //----------------------
                //P = VALOR_PRESENTE(valor do empréstimo)
                //J = TAXA_JURO
                //T = NUMERO_PARCELAS
                //======================
                //VF = P + ((P * J / 100) * T)
                //VP = VF / T
                //Armazenar o VF no campo VALOR_FINANCIAMENTO.
                //O valor da parcela é o resultado de VP.
                Double p = toFinanciamentoBase.ValorPresente.LerConteudoOuPadrao();
                Double j = toFinanciamentoBase.TaxaJuro.LerConteudoOuPadrao();
                Double t = toFinanciamentoBase.NumeroParcelas.LerConteudoOuPadrao();
                Double vf = p + ((p * j / 100) * t);
                Double vp = vf / t;
                Double vpAtual = vp;
                toFinanciamentoBase.ValorFinanciamento = vf;
                //fim-RRN3
                //RRN5
                if (toFinanciamentoBase.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    //pessoa física -> verificar renda
                    //como verificar a renda da pessoa:
                    //1. listar o cliente -> filtrando por cod_cliente+tipo_pessoa
                    //2. pegar o campo RendaFamiliar
                    RNCliente rnCliente = this.Infra.InstanciarRN<RNCliente>();
                    TOCliente toClienteFiltro = new TOCliente();
                    toClienteFiltro.CodCliente = toFinanciamentoBase.CodCliente;
                    toClienteFiltro.TipoPessoa = toFinanciamentoBase.TipoPessoa;
                    Retorno<List<TOCliente>> retListarCliente = rnCliente.Listar(toClienteFiltro);
                    if (!retListarCliente.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retListarCliente.Mensagem);
                    }
                    //retListarCliente.Dados[0] => o cliente da base de dados
                    Double renda30 =
                        retListarCliente.Dados[0].RendaFamiliar.LerConteudoOuPadrao() * 0.3;
                    if (vp > renda30)
                    {
                        return this.Infra.RetornarFalha<Int32>(
                        new MensagemFinanciamento(TipoFalha.FalhaRenda30)
                        );
                    }
                    //RRN6
                    //1. buscar todos financiamentos APROVADOS do CLIENTE
                    //2. varrer a lista de financiamentos, somando o valor da parcela de cada financiamento
                    //3. verificar se a soma é maior que 30% da renda (renda30)
                    toFinanciamentoFiltro = new TOFinanciamento();
                    toFinanciamentoFiltro.CodCliente = toFinanciamentoBase.CodCliente;
                    toFinanciamentoFiltro.TipoPessoa = toFinanciamentoBase.TipoPessoa;
                    toFinanciamentoFiltro.Situacao = "A";
                    Retorno<List<TOFinanciamento>> retListarAprovados = this.Listar(toFinanciamentoFiltro);
                    if (!retListarAprovados.Ok)
                    {
                        return this.Infra.RetornarFalha<Int32>(retListarAprovados.Mensagem);
                    }
                    //varrer a lista e somar o valor das parcelas
                    Double somavp = 0;
                    foreach (TOFinanciamento financiamento in retListarAprovados.Dados)
                    {
                        //VP = VF / T
                        vf = financiamento.ValorFinanciamento.LerConteudoOuPadrao();
                        t = financiamento.NumeroParcelas.LerConteudoOuPadrao();
                        vp = vf / t;
                        somavp += vp;
                    }
                    if (somavp + vpAtual > renda30)
                    {
                        return this.Infra.RetornarFalha<Int32>(
                        new MensagemFinanciamento(TipoFalha.FalhaRenda30Somatorio)
                        );
                    }
                    //fim-RRN6
                    toFinanciamentoBase.Situacao = "A";
                }
                else
                {
                    //pessoa jurídica -> aprovar
                    toFinanciamentoBase.Situacao = "A";
                }
                //fim-RRN5
                BDFinanciamento bdFinanciamento = this.Infra.InstanciarBD<BDFinanciamento>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdFinanciamento.Alterar(toFinanciamentoBase);
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
