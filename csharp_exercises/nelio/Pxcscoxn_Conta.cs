using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcoiexn.RN;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcqcoxn;

namespace Bergs.Pxc.Pxcscoxn
{
    /// <summary>
    /// Classe de acesso a tabela CONTA
    /// </summary>
    public class RNConta : AplicacaoRegraNegocio
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
                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<List<TOConta>> retListar = bdConta.Listar(toConta);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<List<TOConta>>(retListar.Mensagem);
                }
                return this.Infra.RetornarSucesso<List<TOConta>>(retListar.Dados, new OperacaoRealizadaMensagem());
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
                #region Validação de campos obrigatórios
                if (!toConta.CodCliente.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CLIENTE"));
                }
                if (!toConta.Saldo.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("SALDO"));
                }
                if (!toConta.TipoPessoa.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("TIPO_PESSOA"));
                }
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion
                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retIncluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retIncluir = bdConta.Incluir(toConta);
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
        /// <param name="toConta">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<Int32> Alterar(TOConta toConta)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion
                //regras de negócio
                TOConta toContaAlterar = new TOConta();
                toContaAlterar.CodAgencia = toConta.CodAgencia;
                toContaAlterar.CodEspecie = toConta.CodEspecie;
                toContaAlterar.CodConta = toConta.CodConta;
                toContaAlterar.Limite = toConta.Limite;
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdConta.Alterar(toContaAlterar);
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
        /// Executa o comando de SAQUE na tabela
        /// </summary>
        /// <param name="toConta">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<TOConta> Sacar(TOConta toConta)
        {
            try
            {
                //identifica a conta(agência,espécie e conta)
                //      ->exigir que seja preenchido
                //exigir que o valor da transação seja informado
                //      e maior que zero
                //consulta se a conta do boneco tem saldo
                //se o boneco tiver saldo considerando o limite,
                //-então retira o valor da conta
                //-senão xinga o infeliz e cai fora
                #region Validação dos campos da chave primária
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.ValorTransacao.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("Valor da transação"));
                }
                if (toConta.ValorTransacao.LerConteudoOuPadrao() <= 0)
                {
                    return this.Infra.RetornarFalha<TOConta>(new MensagemConta(TipoFalha.ValorTransacaoInvalido));
                }
                #endregion
                //regras de negócio

                #region consulta se a conta do boneco tem saldo
                TOConta toContaFiltro = new TOConta();
                toContaFiltro.CodAgencia = toConta.CodAgencia;
                toContaFiltro.CodEspecie = toConta.CodEspecie;
                toContaFiltro.CodConta = toConta.CodConta;
                //consulta a conta
                Retorno<List<TOConta>> retListar =
                    Listar(toContaFiltro);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<TOConta>(retListar.Mensagem);
                }
                //verifica se retornou o registro com informação
                if (retListar.Dados.Count == 0)
                {
                    return this.Infra.RetornarFalha<TOConta>(
                        new RegistroInexistenteMensagem());
                }
                //pega o saldo e o limite
                Double saldo = retListar.Dados[0].Saldo.LerConteudoOuPadrao();
                Double limite = retListar.Dados[0].Limite.LerConteudoOuPadrao();
                //se o boneco tiver saldo considerando o limite,
                if (saldo + limite >= toConta.ValorTransacao.LerConteudoOuPadrao())
                {
                    //saca
                    saldo -= toConta.ValorTransacao.LerConteudoOuPadrao();
                    TOConta toContaAlterar = new TOConta();
                    toContaAlterar.CodAgencia = toConta.CodAgencia;
                    toContaAlterar.CodEspecie = toConta.CodEspecie;
                    toContaAlterar.CodConta = toConta.CodConta;
                    toContaAlterar.Saldo = saldo;
                    BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                    Retorno<Int32> retAlterar;
                    using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                    {
                        retAlterar = bdConta.Alterar(toContaAlterar);
                        if (!retAlterar.Ok)
                        {
                            return this.Infra.RetornarFalha<TOConta>(retAlterar.Mensagem);
                        }
                        escopo.EfetivarTransacao();
                    }
                }
                else
                {
                    //xinga
                    Double disponivel = saldo + limite;
                    return this.Infra.RetornarFalha<TOConta>(
                        new MensagemConta(TipoFalha.SaldoInsuficiente,
                            disponivel.ToString())
                        );
                }
                //-então retira o valor da conta
                //-senão xinga o infeliz e cai fora
                #endregion
                retListar.Dados[0].Saldo = saldo;

                return this.Infra.RetornarSucesso<TOConta>(retListar.Dados[0], new OperacaoRealizadaMensagem("Alteração"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<TOConta>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Executa o comando de SAQUE na tabela
        /// </summary>
        /// <param name="toConta">Campos para alteração</param>
        /// <returns>Retorna a quantidade de registros atualizados</returns>
        public Retorno<TOConta> Depositar(TOConta toConta)
        {
            try
            {
                //identifica a conta(agência,espécie e conta)
                //      ->exigir que seja preenchido
                //exigir que o valor da transação seja informado
                //      e maior que zero
                //adiciona saldo pro cara
                #region Validação dos campos da chave primária
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.ValorTransacao.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("Valor da transação"));
                }
                if (toConta.ValorTransacao.LerConteudoOuPadrao() <= 0)
                {
                    return this.Infra.RetornarFalha<TOConta>(new MensagemConta(TipoFalha.ValorTransacaoInvalido));
                }
                #endregion
                //regras de negócio

                #region consulta se a conta do boneco tem saldo
                TOConta toContaFiltro = new TOConta();
                toContaFiltro.CodAgencia = toConta.CodAgencia;
                toContaFiltro.CodEspecie = toConta.CodEspecie;
                toContaFiltro.CodConta = toConta.CodConta;
                //consulta a conta
                Retorno<List<TOConta>> retListar =
                    Listar(toContaFiltro);
                if (!retListar.Ok)
                {
                    return this.Infra.RetornarFalha<TOConta>(retListar.Mensagem);
                }
                //verifica se retornou o registro com informação
                if (retListar.Dados.Count == 0)
                {
                    return this.Infra.RetornarFalha<TOConta>(
                        new RegistroInexistenteMensagem());
                }
                //pega o saldo e o limite
                Double saldo = retListar.Dados[0].Saldo.LerConteudoOuPadrao();

                saldo += toConta.ValorTransacao.LerConteudoOuPadrao();
                TOConta toContaAlterar = new TOConta();
                toContaAlterar.CodAgencia = toConta.CodAgencia;
                toContaAlterar.CodEspecie = toConta.CodEspecie;
                toContaAlterar.CodConta = toConta.CodConta;
                toContaAlterar.Saldo = saldo;
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdConta.Alterar(toContaAlterar);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<TOConta>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }

                #endregion
                retListar.Dados[0].Saldo = saldo;

                return this.Infra.RetornarSucesso<TOConta>(retListar.Dados[0], new OperacaoRealizadaMensagem("Alteração"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<TOConta>(new Mensagem(e));
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
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<Int32>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion
                //TODO: regras de negócio
                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retExcluir;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retExcluir = bdConta.Excluir(toConta);
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
