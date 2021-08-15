using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn.Interface;
using Bergs.Pxc.Pxcbtoxn;
using Bergs.Pxc.Pxcoiexn;
using Bergs.Pxc.Pxcsfixn;
using NUnit.Framework;

namespace Bergs.Pxc.PxcuFIxn_TestaFinanciamento.Teste
{
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class TestaFinanciamento : AplicacaoTela
    {
        Pxcoiexn.RN.EscopoTransacional escopo = null;

        public TestaFinanciamento() :
            this(@"C:\soft\pxc\data\Pxcz01da.mdb")
        {
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="caminho"></param>
        private TestaFinanciamento(String caminho)
            : base(caminho)
        { }

        [SetUp]
        public void TestInit()
        {
            escopo = this.Infra.CriarEscopoTransacional();
        }

        [TearDown]
        public void TestCleanUp()
        {
            escopo.Dispose();
        }

        /*
         * Sucesso
         * 
         * IncluirPF12 parcelas
         * IncluirPF48 parcelas
         * IncluirPJ12 parcelas
         * IncluirPJ48 parcelas
         * 
         * 
         * 
         * 
         * Falha
         * RN1 - IncluirPFInexistente
         * RN1 - IncluirPJInexistente
         * RN2 - IncluirPF com parcela -12
         * RN2 - IncluirPF com parcela 0
         * RN2 - IncluirPF com parcela 11
         * RN2 - IncluirPF com parcela -48
         * RN2 - IncluirPF com parcela 49
         * 
         */

        [Test(Description = "IncluirPF")]
        public void IncluirPF([Values(12, 36, 48)]int parcelas)
        {
            RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
            TOFinanciamento toFinanciamento = new TOFinanciamento();
            toFinanciamento.CodCliente = 191;
            toFinanciamento.TipoPessoa = "F";
            toFinanciamento.CodFinanciamento = 1;
            toFinanciamento.NumeroParcelas = parcelas;
            toFinanciamento.TaxaJuro = 1;
            toFinanciamento.ValorPresente = 10;
            toFinanciamento.Situacao = "P";
            Retorno<Int32> retIncluir = rnFinanciamento.Incluir(toFinanciamento);
            Assert.IsTrue(retIncluir.Ok, "Incluir - erro, retornou: {0}", retIncluir.Mensagem.ParaOperador);

            double VF = 10 + ((10 * 1.0 / 100) * parcelas);

            toFinanciamento = new TOFinanciamento();
            toFinanciamento.CodFinanciamento = 1;
            Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamento);
            Assert.IsTrue(retListar.Ok, "Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);
            Assert.AreEqual(1, retListar.Dados.Count, "Esperava receber 1 e recebi {0}.", retListar.Dados.Count);

            toFinanciamento = retListar.Dados[0];
            Assert.AreEqual(191, toFinanciamento.CodCliente.LerConteudoOuPadrao(), "Esperava receber 191 e recebi {0}.", toFinanciamento.CodCliente.LerConteudoOuPadrao());
            Assert.AreEqual("F", toFinanciamento.TipoPessoa.LerConteudoOuPadrao(), "Esperava receber F e recebi {0}.", toFinanciamento.TipoPessoa.LerConteudoOuPadrao());
            Assert.AreEqual("P", toFinanciamento.Situacao.LerConteudoOuPadrao(), "Esperava receber P e recebi {0}.", toFinanciamento.Situacao.LerConteudoOuPadrao());
            Assert.AreEqual(1, toFinanciamento.CodFinanciamento.LerConteudoOuPadrao(), "Esperava receber 1 e recebi {0}.", toFinanciamento.CodFinanciamento.LerConteudoOuPadrao());
            Assert.AreEqual(parcelas, toFinanciamento.NumeroParcelas.LerConteudoOuPadrao(), "Esperava receber {0} e recebi {1}.",parcelas, toFinanciamento.NumeroParcelas.LerConteudoOuPadrao());
            Assert.AreEqual(1, toFinanciamento.TaxaJuro.LerConteudoOuPadrao(), "Esperava receber 1 e recebi {0}.", toFinanciamento.TaxaJuro.LerConteudoOuPadrao());
            Assert.AreEqual(10, toFinanciamento.ValorPresente.LerConteudoOuPadrao(), "Esperava receber 10 e recebi {0}.", toFinanciamento.ValorPresente.LerConteudoOuPadrao());
            Assert.AreEqual(VF, toFinanciamento.ValorFinanciamento.LerConteudoOuPadrao(), "Esperava receber {0} e recebi {1}.", VF, toFinanciamento.ValorFinanciamento.LerConteudoOuPadrao());
        }

        [Test(Description = "IncluirPJ")]
        public void IncluirPJ()
        {
            RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
            TOFinanciamento toFinanciamento = new TOFinanciamento();
            toFinanciamento.CodCliente = 191;
            toFinanciamento.TipoPessoa = "J";
            toFinanciamento.CodFinanciamento = 1;
            toFinanciamento.NumeroParcelas = 12;
            toFinanciamento.TaxaJuro = 1;
            toFinanciamento.ValorPresente = 10;
            toFinanciamento.Situacao = "P";
            Retorno<Int32> retIncluir = rnFinanciamento.Incluir(toFinanciamento);
            Assert.IsTrue(retIncluir.Ok, "Incluir - erro, retornou: {0}", retIncluir.Mensagem.ParaOperador);

            toFinanciamento = new TOFinanciamento();
            toFinanciamento.CodFinanciamento = 1;
            Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamento);
            Assert.IsTrue(retListar.Ok, "Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);
            Assert.AreEqual(1, retListar.Dados.Count, "Esperava receber 1 e recebi {0}.", retListar.Dados.Count);

            toFinanciamento = retListar.Dados[0];
            Assert.AreEqual(191, toFinanciamento.CodCliente.LerConteudoOuPadrao(), "Esperava receber 191 e recebi {0}.", toFinanciamento.CodCliente.LerConteudoOuPadrao());
            Assert.AreEqual("J", toFinanciamento.TipoPessoa.LerConteudoOuPadrao(), "Esperava receber J e recebi {0}.", toFinanciamento.TipoPessoa.LerConteudoOuPadrao());
            Assert.AreEqual("P", toFinanciamento.Situacao.LerConteudoOuPadrao(), "Esperava receber P e recebi {0}.", toFinanciamento.Situacao.LerConteudoOuPadrao());
            Assert.AreEqual(1, toFinanciamento.CodFinanciamento.LerConteudoOuPadrao(), "Esperava receber 1 e recebi {0}.", toFinanciamento.CodFinanciamento.LerConteudoOuPadrao());
            Assert.AreEqual(12, toFinanciamento.NumeroParcelas.LerConteudoOuPadrao(), "Esperava receber 12 e recebi {0}.", toFinanciamento.NumeroParcelas.LerConteudoOuPadrao());
            Assert.AreEqual(1, toFinanciamento.TaxaJuro.LerConteudoOuPadrao(), "Esperava receber 1 e recebi {0}.", toFinanciamento.TaxaJuro.LerConteudoOuPadrao());
            Assert.AreEqual(10, toFinanciamento.ValorPresente.LerConteudoOuPadrao(), "Esperava receber 10 e recebi {0}.", toFinanciamento.ValorPresente.LerConteudoOuPadrao());
            Assert.AreEqual(11.2, toFinanciamento.ValorFinanciamento.LerConteudoOuPadrao(), "Esperava receber 11,2 e recebi {0}.", toFinanciamento.ValorFinanciamento.LerConteudoOuPadrao());
        }

        [Test(Description = "IncluirPF")]
        public void IncluirPFSituacaoA()
        {
            RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
            TOFinanciamento toFinanciamento = new TOFinanciamento();
            toFinanciamento.CodCliente = 191;
            toFinanciamento.TipoPessoa = "F";
            toFinanciamento.CodFinanciamento = 1;
            toFinanciamento.NumeroParcelas = 12;
            toFinanciamento.TaxaJuro = 1;
            toFinanciamento.ValorPresente = 10;
            toFinanciamento.Situacao = "A";
            Retorno<Int32> retIncluir = rnFinanciamento.Incluir(toFinanciamento);
            Assert.IsTrue(retIncluir.Ok, "Incluir - erro, retornou: {0}", retIncluir.Mensagem.ParaOperador);

            toFinanciamento = new TOFinanciamento();
            toFinanciamento.CodFinanciamento = 1;
            Retorno<List<TOFinanciamento>> retListar = rnFinanciamento.Listar(toFinanciamento);
            Assert.IsTrue(retListar.Ok, "Listar - erro, retornou: {0}", retListar.Mensagem.ParaOperador);
            Assert.AreEqual(1, retListar.Dados.Count, "Esperava receber 1 e recebi {0}.", retListar.Dados.Count);

            toFinanciamento = retListar.Dados[0];
            Assert.AreEqual(191, toFinanciamento.CodCliente.LerConteudoOuPadrao(), "Esperava receber 191 e recebi {0}.", toFinanciamento.CodCliente.LerConteudoOuPadrao());
            Assert.AreEqual("F", toFinanciamento.TipoPessoa.LerConteudoOuPadrao(), "Esperava receber F e recebi {0}.", toFinanciamento.TipoPessoa.LerConteudoOuPadrao());
            Assert.AreEqual("P", toFinanciamento.Situacao.LerConteudoOuPadrao(), "Esperava receber P e recebi {0}.", toFinanciamento.Situacao.LerConteudoOuPadrao());
            Assert.AreEqual(1, toFinanciamento.CodFinanciamento.LerConteudoOuPadrao(), "Esperava receber 1 e recebi {0}.", toFinanciamento.CodFinanciamento.LerConteudoOuPadrao());
            Assert.AreEqual(12, toFinanciamento.NumeroParcelas.LerConteudoOuPadrao(), "Esperava receber 12 e recebi {0}.", toFinanciamento.NumeroParcelas.LerConteudoOuPadrao());
            Assert.AreEqual(1, toFinanciamento.TaxaJuro.LerConteudoOuPadrao(), "Esperava receber 1 e recebi {0}.", toFinanciamento.TaxaJuro.LerConteudoOuPadrao());
            Assert.AreEqual(10, toFinanciamento.ValorPresente.LerConteudoOuPadrao(), "Esperava receber 10 e recebi {0}.", toFinanciamento.ValorPresente.LerConteudoOuPadrao());
            Assert.AreEqual(11.2, toFinanciamento.ValorFinanciamento.LerConteudoOuPadrao(), "Esperava receber 11,2 e recebi {0}.", toFinanciamento.ValorFinanciamento.LerConteudoOuPadrao());
        }

        [Test(Description = "IncluirPF com parcela incorreta")]
        public void RN02IncluirPFParcelaIncorreta([Values(-12, 0 , 11, -48, 49)]int parcelas)
        {
            RNFinanciamento rnFinanciamento = this.Infra.InstanciarRN<RNFinanciamento>();
            TOFinanciamento toFinanciamento = new TOFinanciamento();
            toFinanciamento.CodCliente = 191;
            toFinanciamento.TipoPessoa = "F";
            toFinanciamento.CodFinanciamento = 1;
            toFinanciamento.NumeroParcelas = parcelas;
            toFinanciamento.TaxaJuro = 1;
            toFinanciamento.ValorPresente = 10;
            toFinanciamento.Situacao = "P";
            Retorno<Int32> retIncluir = rnFinanciamento.Incluir(toFinanciamento);
            Assert.IsFalse(retIncluir.Ok, "Incluir - erro, retornou: {0}", retIncluir.Mensagem.ParaOperador);
            Assert.AreEqual("Número de parcelas do financiamento deve estar compreendido entre 12 e 48 parcelas, inclusive.",
                        retIncluir.Mensagem.ParaOperador,
                        "Esperava mensagem {0}", retIncluir.Mensagem.ParaOperador);
        }
    }
}
