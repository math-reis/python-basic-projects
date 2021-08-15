using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqclxn;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>
    /// Classe de acesso a tabela CLIENTE
    /// </summary>
    public class RNCliente : AplicacaoRegraNegocio
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
                //TODO: regras de negócio
                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<List<TOCliente>> retListar = bdCliente.Listar(toCliente);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOCliente>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOCliente>>(retListar.Dados, new OperacaoRealizadaMensagem());
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
                #region Validação de campos obrigatórios
                if (!toCliente.NomeCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("NOME_CLIENTE"));
                }
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion

                #region Validação Regras de Negócio
                Retorno retornoValidacao = Validar(toCliente, true);
                if (!retornoValidacao.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(retornoValidacao.Mensagem);
                }
                #endregion

                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdCliente.Incluir(toCliente);
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
        /// <param name="toCliente">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOCliente toCliente)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion

                #region Validação Regras de Negócio
                Retorno retornoValidacao = Validar(toCliente, false);
                if (!retornoValidacao.Ok)
                {
                    return this.Infra.RetornarFalha<Int32>(retornoValidacao.Mensagem);
                }
                #endregion

                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdCliente.Alterar(toCliente);
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
        /// <param name="toCliente">Campos para filtro da exclusão</param>
        /// <returns>Retorna a quantidade de registros excluídos</returns>
        public Retorno<Int32> Excluir(TOCliente toCliente)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toCliente.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toCliente.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                #endregion
                //TODO: regras de negócio
                BDCliente bdCliente = this.Infra.InstanciarBD<BDCliente>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdCliente.Excluir(toCliente);
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

        private Retorno Validar(TOCliente toCliente, bool ehInclusao)
        {
            Mensagem m = null;
            if (ehInclusao)
            {
                if (toCliente.TipoPessoa == "F")
                {
                    /// validar cpf 
                    string cpf = toCliente.CodCliente.ToString().PadLeft(11, '0');
                    if (!Pxcoiexn.Interface.Util.ValidaCpf(cpf))
                    {
                        return this.Infra.RetornarFalha(new MensagemCliente(TipoFalha.CpfInvalido));
                    }
                }
                else if (toCliente.TipoPessoa == "J")
                {
                    /// validar cnpj
                    string cnpj = toCliente.CodCliente.ToString();
                    cnpj = cnpj.PadLeft(14, '0');
                    if (!Pxcoiexn.Interface.Util.ValidaCnpj(cnpj))
                    {
                        m = new MensagemCliente(TipoFalha.CnpjInvalido);
                        return this.Infra.RetornarFalha(m);
                    }
                }
                else
                {
                    m = new MensagemCliente(TipoFalha.TipoPessoaInvalido);
                    return this.Infra.RetornarFalha(m);
                }
            }
            // "fabian la porta"
            //[0]=fabian
            //[1]=la
            //[2]=porta
            string nome = toCliente.NomeCliente.ToString().Trim();
            if (nome.Split(' ').Length < 2)
            {
                ///errro
                m = new MensagemCliente(TipoFalha.NomeInvalido);
                return this.Infra.RetornarFalha(m);
            }
            if (nome.Split(' ')[0].Length < 2)
            {
                ///erro
                m = new MensagemCliente(TipoFalha.NomeInvalido);
                return this.Infra.RetornarFalha(m);
            }
            return this.Infra.RetornarSucesso(new OperacaoRealizadaMensagem());
        }
        #endregion
    }
}
