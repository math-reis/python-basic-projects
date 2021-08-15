using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela FINANCIAMENTO</summary>
    public class TOFinanciamento : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codFinanciamento;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> codCliente;
        private CampoTabela<Int32> numeroParcelas;
        private CampoTabela<String> situacao;
        private CampoTabela<Double> taxaJuro;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<Double> valorFinanciamento;
        private CampoTabela<Double> valorPresente;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela FINANCIAMENTO</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_FINANCIAMENTO da tabela FINANCIAMENTO</summary>
        [XmlElement("cod_financiamento")]
        public CampoTabela<Int32> CodFinanciamento
        {
            get { return codFinanciamento; }
            set { codFinanciamento = value; }
        }
        /// <summary>Campo NUMERO_PARCELAS da tabela FINANCIAMENTO</summary>
        [XmlElement("numero_parcelas")]
        public CampoTabela<Int32> NumeroParcelas
        {
            get { return numeroParcelas; }
            set { numeroParcelas = value; }
        }
        /// <summary>Campo SITUACAO da tabela FINANCIAMENTO</summary>
        [XmlElement("situacao")]
        public CampoTabela<String> Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }
        /// <summary>Campo TAXA_JURO da tabela FINANCIAMENTO</summary>
        [XmlElement("taxa_juro")]
        public CampoTabela<Double> TaxaJuro
        {
            get { return taxaJuro; }
            set { taxaJuro = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela FINANCIAMENTO</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR_FINANCIAMENTO da tabela FINANCIAMENTO</summary>
        [XmlElement("valor_financiamento")]
        public CampoTabela<Double> ValorFinanciamento
        {
            get { return valorFinanciamento; }
            set { valorFinanciamento = value; }
        }
        /// <summary>Campo VALOR_PRESENTE da tabela FINANCIAMENTO</summary>
        [XmlElement("valor_presente")]
        public CampoTabela<Double> ValorPresente
        {
            get { return valorPresente; }
            set { valorPresente = value; }
        }

        /// <summary>Campo NOME_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("nome_cliente")]
        public CampoTabela<String> NomeCliente { get; set; }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOFinanciamento</summary>
        /// <param name="linha">Linha para popular os campos da TOFinanciamento</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_FINANCIAMENTO":
                        this.codFinanciamento = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "NUMERO_PARCELAS":
                        this.numeroParcelas = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "SITUACAO":
                        this.situacao = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "TAXA_JURO":
                        this.taxaJuro = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "VALOR_FINANCIAMENTO":
                        this.valorFinanciamento = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "VALOR_PRESENTE":
                        this.valorPresente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "NOME_CLIENTE":
                        this.NomeCliente = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
