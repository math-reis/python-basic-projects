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
        /// Metodo para realizar depositos em conta corrente
        /// </summary>
        /// <param name="toConta">conta + valor a ser depositado</param>
        /// <returns>dados da conta alterados</returns>
        public Retorno<TOConta> Depositar(TOConta toConta)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion

                //validar valor a ser depositado
                if (!toConta.ValorTransacao.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("VALOR_TRANSACAO"));
                }
                if (toConta.ValorTransacao.LerConteudoOuPadrao() <= 0)
                {
                    Mensagem m = new MensagemConta(TipoFalha.ValorTransacaoMenorIgualZero);
                    return this.Infra.RetornarFalha<TOConta>(m);
                }

                //----* solucao 1
                //toConta.CodCliente = new Pxcoiexn.BD.CampoTabela<double>();
                //toConta.Limite = new Pxcoiexn.BD.CampoTabela<double>();
                //toConta.Saldo = new Pxcoiexn.BD.CampoTabela<double>();
                //toConta.TipoPessoa = new Pxcoiexn.BD.CampoTabela<string>();

                //----- solucao 2
                //if (toConta.CodCliente.FoiSetado ||
                //    toConta.Limite.FoiSetado ||
                //    toConta.Saldo.FoiSetado ||
                //    toConta.TipoPessoa.FoiSetado)
                //{
                //    //erro = contrabando
                //    //----- nao posso alterar outras infos
                //    Mensagem m = new MensagemConta(TipoFalha.CamposProibidosSacarDepositar);
                //    return this.Infra.RetornarFalha<TOConta>(m);
                //}

                //------ solucao 3
                TOConta toConsultaSaldo = new TOConta();
                toConsultaSaldo.CodAgencia = toConta.CodAgencia;
                toConsultaSaldo.CodEspecie = toConta.CodEspecie;
                toConsultaSaldo.CodConta = toConta.CodConta;

                Retorno<List<TOConta>> retornoConsultaSaldo = this.Listar(toConsultaSaldo);
                /* 1 -> dados da conta
                 * 2 -> conta inexistente
                 * 3 -> Erro
                 * */
                if (!retornoConsultaSaldo.Ok)
                {
                    //retornar o erro
                    return this.Infra.RetornarFalha<TOConta>(retornoConsultaSaldo.Mensagem);
                }
                if (retornoConsultaSaldo.Dados.Count == 0)
                {
                    //---- conta inexistente
                    Mensagem m = new MensagemConta(TipoFalha.ContaInexistente);
                    return this.Infra.RetornarFalha<TOConta>(m);
                }

                //****** Atualiza o saldo com referencia pelo q tem na base de dados
                double novoSaldo = retornoConsultaSaldo.Dados[0].Saldo;
                novoSaldo += toConta.ValorTransacao;

                //****** Garantindo que só será alterado o SALDO!!!!!
                TOConta toDeposito = new TOConta();
                toDeposito.CodAgencia = toConta.CodAgencia;
                toDeposito.CodEspecie = toConta.CodEspecie;
                toDeposito.CodConta = toConta.CodConta;
                toDeposito.Saldo = novoSaldo;

                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdConta.Alterar(toDeposito);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<TOConta>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }

                //***** Pegar os dados do banco de dados...
                TOConta toAtualizado = retornoConsultaSaldo.Dados[0];
                //**** atualizar com o novo saldo alterado
                toAtualizado.Saldo = novoSaldo;

                //******* retorno o TO consultado com o novo saldo
                return this.Infra.RetornarSucesso<TOConta>(toAtualizado, new OperacaoRealizadaMensagem("Alteração"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<TOConta>(new Mensagem(e));
            }
        }

        /// <summary>
        /// Metodo para realizar saques em conta corrente
        /// </summary>
        /// <param name="toConta">conta + valor a ser sacado</param>
        /// <returns>dados da conta alterados</returns>
        public Retorno<TOConta> Sacar(TOConta toConta)
        {
            try
            {
                #region Validação dos campos da chave primária
                if (!toConta.CodConta.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_CONTA"));
                }
                if (!toConta.CodEspecie.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_ESPECIE"));
                }
                if (!toConta.CodAgencia.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("COD_AGENCIA"));
                }
                #endregion

                //validar valor a ser depositado
                if (!toConta.ValorTransacao.TemConteudo)
                {
                    return this.Infra.RetornarFalha<TOConta>(new CampoObrigatorioMensagem("VALOR_TRANSACAO"));
                }
                if (toConta.ValorTransacao.LerConteudoOuPadrao() <= 0)
                {
                    Mensagem m = new MensagemConta(TipoFalha.ValorTransacaoMenorIgualZero);
                    return this.Infra.RetornarFalha<TOConta>(m);
                }

                //----* solucao 1
                //toConta.CodCliente = new Pxcoiexn.BD.CampoTabela<double>();
                //toConta.Limite = new Pxcoiexn.BD.CampoTabela<double>();
                //toConta.Saldo = new Pxcoiexn.BD.CampoTabela<double>();
                //toConta.TipoPessoa = new Pxcoiexn.BD.CampoTabela<string>();

                //----- solucao 2
                //if (toConta.CodCliente.FoiSetado ||
                //    toConta.Limite.FoiSetado ||
                //    toConta.Saldo.FoiSetado ||
                //    toConta.TipoPessoa.FoiSetado)
                //{
                //    //erro = contrabando
                //    //----- nao posso alterar outras infos
                //    Mensagem m = new MensagemConta(TipoFalha.CamposProibidosSacarDepositar);
                //    return this.Infra.RetornarFalha<TOConta>(m);
                //}

                //------ solucao 3
                TOConta toConsultaSaldo = new TOConta();
                toConsultaSaldo.CodAgencia = toConta.CodAgencia;
                toConsultaSaldo.CodEspecie = toConta.CodEspecie;
                toConsultaSaldo.CodConta = toConta.CodConta;

                Retorno<List<TOConta>> retornoConsultaSaldo = this.Listar(toConsultaSaldo);
                /* 1 -> dados da conta
                 * 2 -> conta inexistente
                 * 3 -> Erro
                 * */
                if (!retornoConsultaSaldo.Ok)
                {
                    //retornar o erro
                    return this.Infra.RetornarFalha<TOConta>(retornoConsultaSaldo.Mensagem);
                }
                if (retornoConsultaSaldo.Dados.Count == 0)
                {
                    //---- conta inexistente
                    Mensagem m = new MensagemConta(TipoFalha.ContaInexistente);
                    return this.Infra.RetornarFalha<TOConta>(m);
                }

                double novoSaldo = retornoConsultaSaldo.Dados[0].Saldo;
                double saldoConsiderado = novoSaldo;
                if (retornoConsultaSaldo.Dados[0].Limite.TemConteudo)
                {
                    saldoConsiderado += retornoConsultaSaldo.Dados[0].Limite;
                }

                //**** validar se o valor a ser sacado é menor ou igual ao saldo
                if (saldoConsiderado < toConta.ValorTransacao)
                {
                    //--- Erro: saldo insuficiente
                    Mensagem m = new MensagemConta(TipoFalha.SaldoInsuficiente,
                                                   retornoConsultaSaldo.Dados[0].Saldo.ToString());
                    return this.Infra.RetornarFalha<TOConta>(m);
                }

                //****** Atualiza o saldo com referencia pelo q tem na base de dados
                novoSaldo -= toConta.ValorTransacao;

                //****** Garantindo que só será alterado o SALDO!!!!!
                TOConta toSaque = new TOConta();
                toSaque.CodAgencia = toConta.CodAgencia;
                toSaque.CodEspecie = toConta.CodEspecie;
                toSaque.CodConta = toConta.CodConta;
                toSaque.Saldo = novoSaldo;

                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdConta.Alterar(toSaque);
                    if (!retAlterar.Ok)
                    {
                        return this.Infra.RetornarFalha<TOConta>(retAlterar.Mensagem);
                    }
                    escopo.EfetivarTransacao();
                }

                //***** Pegar os dados do banco de dados...
                TOConta toAtualizado = retornoConsultaSaldo.Dados[0];
                //**** atualizar com o novo saldo alterado
                toAtualizado.Saldo = novoSaldo;

                //******* retorno o TO consultado com o novo saldo
                return this.Infra.RetornarSucesso<TOConta>(toAtualizado, new OperacaoRealizadaMensagem("Alteração"));
            }
            catch (Exception e)
            {
                return this.Infra.RetornarFalha<TOConta>(new Mensagem(e));
            }
        }

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

                #region Regra de Negócio
                //RN1
                if (toConta.CodAgencia.LerConteudoOuPadrao() <= 0)
                {
                    Mensagem m = new MensagemConta(TipoFalha.RN1_AgenciaInvalida);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                //RN2
                if (toConta.CodConta.LerConteudoOuPadrao() <= 0)
                {
                    Mensagem m = new MensagemConta(TipoFalha.RN2_ContaInvalida);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                //RN3
                if (toConta.TipoPessoa.LerConteudoOuPadrao() == "F")
                {
                    if (toConta.CodEspecie.LerConteudoOuPadrao() != 35)
                    {
                        Mensagem m = new MensagemConta(TipoFalha.RN3_EspecieInvalida);
                        return this.Infra.RetornarFalha<Int32>(m);
                    }
                }
                else if (toConta.TipoPessoa.LerConteudoOuPadrao() == "J")
                {
                    if (toConta.CodEspecie.LerConteudoOuPadrao() != 6)
                    {
                        Mensagem m = new MensagemConta(TipoFalha.RN3_EspecieInvalida);
                        return this.Infra.RetornarFalha<Int32>(m);
                    }
                }
                else
                {
                    Mensagem m = new MensagemConta(TipoFalha.TipoPessoaInvalido);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                //RN4
                if (toConta.Limite.TemConteudo && toConta.Limite.LerConteudoOuPadrao() <= 0)
                {
                    Mensagem m = new MensagemConta(TipoFalha.RN4_LimiteInvalido);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                //RN5
                if (toConta.Saldo.LerConteudoOuPadrao() != 0)
                {
                    Mensagem m = new MensagemConta(TipoFalha.RN5_SaldoInicialInvalido);
                    return this.Infra.RetornarFalha<Int32>(m);
                }
                #endregion

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

                //RN4
                if (toConta.Limite.TemConteudo && toConta.Limite.LerConteudoOuPadrao() <= 0)
                {
                    Mensagem m = new MensagemConta(TipoFalha.RN4_LimiteInvalido);
                    return this.Infra.RetornarFalha<Int32>(m);
                }

                //RN8
                //if (toConta.CodCliente.FoiSetado ||
                //    toConta.Saldo.FoiSetado ||
                //    toConta.TipoPessoa.FoiSetado ||
                //    toConta.ValorTransacao.FoiSetado)
                //{
                //    //criar msg e retornar falha
                //}
                TOConta toAlteracao = new TOConta();
                toAlteracao.CodAgencia = toConta.CodAgencia;
                toAlteracao.CodEspecie = toConta.CodEspecie;
                toAlteracao.CodConta = toConta.CodConta;
                toAlteracao.Limite = toConta.Limite;

                BDConta bdConta = this.Infra.InstanciarBD<BDConta>();
                Retorno<Int32> retAlterar;
                using (EscopoTransacional escopo = this.Infra.CriarEscopoTransacional())
                {
                    retAlterar = bdConta.Alterar(toAlteracao);
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
