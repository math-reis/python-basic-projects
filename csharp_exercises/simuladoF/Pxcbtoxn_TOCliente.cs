using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CLIENTE</summary>
    public class TOCliente : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Double> codCliente;
        private CampoTabela<String> tipoPessoa;
        #endregion

        #region Campos da tabela
        private CampoTabela<DateTime> dataAtuRating;
        private CampoTabela<DateTime> dataCadastro;
        private CampoTabela<DateTime> dataNasc;
        private CampoTabela<String> nomeCliente;
        private CampoTabela<String> ratingCliente;
        private CampoTabela<Double> rendaFamiliar;
        private CampoTabela<Double> telefone;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo DATA_ATU_RATING da tabela CLIENTE</summary>
        [XmlElement("data_atu_rating")]
        public CampoTabela<DateTime> DataAtuRating
        {
            get { return dataAtuRating; }
            set { dataAtuRating = value; }
        }
        /// <summary>Campo DATA_CADASTRO da tabela CLIENTE</summary>
        [XmlElement("data_cadastro")]
        public CampoTabela<DateTime> DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }
        /// <summary>Campo DATA_NASC da tabela CLIENTE</summary>
        [XmlElement("data_nasc")]
        public CampoTabela<DateTime> DataNasc
        {
            get { return dataNasc; }
            set { dataNasc = value; }
        }
        /// <summary>Campo NOME_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("nome_cliente")]
        public CampoTabela<String> NomeCliente
        {
            get { return nomeCliente; }
            set { nomeCliente = value; }
        }
        /// <summary>Campo RATING_CLIENTE da tabela CLIENTE</summary>
        [XmlElement("rating_cliente")]
        public CampoTabela<String> RatingCliente
        {
            get { return ratingCliente; }
            set { ratingCliente = value; }
        }
        /// <summary>Campo RENDA_FAMILIAR da tabela CLIENTE</summary>
        [XmlElement("renda_familiar")]
        public CampoTabela<Double> RendaFamiliar
        {
            get { return rendaFamiliar; }
            set { rendaFamiliar = value; }
        }
        /// <summary>Campo TELEFONE da tabela CLIENTE</summary>
        [XmlElement("telefone")]
        public CampoTabela<Double> Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CLIENTE</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOCliente</summary>
        /// <param name="linha">Linha para popular os campos da TOCliente</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "DATA_ATU_RATING":
                        this.dataAtuRating = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "DATA_CADASTRO":
                        this.dataCadastro = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "DATA_NASC":
                        this.dataNasc = this.LeCampoTabela<DateTime>(campo.Conteudo);
                        break;
                    case "NOME_CLIENTE":
                        this.nomeCliente = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "RATING_CLIENTE":
                        this.ratingCliente = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    case "RENDA_FAMILIAR":
                        this.rendaFamiliar = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "TELEFONE":
                        this.telefone = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "TIPO_PESSOA":
                        this.tipoPessoa = this.LeCampoTabela<String>(campo.Conteudo);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
