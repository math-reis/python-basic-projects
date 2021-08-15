using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Bergs.Pxc.Pxcoiexn.BD;

namespace Bergs.Pxc.Pxcbtoxn
{
    ///<summary>Classe para manipulação dos campos da tabela CONTA</summary>
    public class TOConta : TOTabela
    {
        #region Campos

        #region Campos chave primária
        private CampoTabela<Int32> codAgencia;
        private CampoTabela<Int32> codConta;
        private CampoTabela<Int32> codEspecie;
        #endregion

        #region Campos da tabela
        private CampoTabela<Double> codCliente;
        private CampoTabela<Double> limite;
        private CampoTabela<Double> saldo;
        private CampoTabela<String> tipoPessoa;
        private CampoTabela<double> valorTransacao;
        #endregion

        #endregion

        #region Propriedades
        /// <summary>Campo COD_AGENCIA da tabela CONTA</summary>
        [XmlElement("cod_agencia")]
        public CampoTabela<Int32> CodAgencia
        {
            get { return codAgencia; }
            set { codAgencia = value; }
        }
        /// <summary>Campo COD_CLIENTE da tabela CONTA</summary>
        [XmlElement("cod_cliente")]
        public CampoTabela<Double> CodCliente
        {
            get { return codCliente; }
            set { codCliente = value; }
        }
        /// <summary>Campo COD_CONTA da tabela CONTA</summary>
        [XmlElement("cod_conta")]
        public CampoTabela<Int32> CodConta
        {
            get { return codConta; }
            set { codConta = value; }
        }
        /// <summary>Campo COD_ESPECIE da tabela CONTA</summary>
        [XmlElement("cod_especie")]
        public CampoTabela<Int32> CodEspecie
        {
            get { return codEspecie; }
            set { codEspecie = value; }
        }
        /// <summary>Campo LIMITE da tabela CONTA</summary>
        [XmlElement("limite")]
        public CampoTabela<Double> Limite
        {
            get { return limite; }
            set { limite = value; }
        }
        /// <summary>Campo SALDO da tabela CONTA</summary>
        [XmlElement("saldo")]
        public CampoTabela<Double> Saldo
        {
            get { return saldo; }
            set { saldo = value; }
        }
        /// <summary>Campo TIPO_PESSOA da tabela CONTA</summary>
        [XmlElement("tipo_pessoa")]
        public CampoTabela<String> TipoPessoa
        {
            get { return tipoPessoa; }
            set { tipoPessoa = value; }
        }
        /// <summary>Campo VALOR DA TRANSACAO para metodos sacar/deposicar</summary>
        [XmlElement("valor_transacao")]
        public CampoTabela<Double> ValorTransacao
        {
            get { return valorTransacao; }
            set { valorTransacao = value; }
        }
        #endregion

        #region Métodos
        /// <summary>Método para popular os campos da TOConta</summary>
        /// <param name="linha">Linha para popular os campos da TOConta</param>
        public override void PopularRetorno(Linha linha)
        {
            foreach (Campo campo in linha.Campos)
            {
                switch (campo.Nome)
                {
                    case "COD_AGENCIA":
                        this.codAgencia = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_CLIENTE":
                        this.codCliente = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "COD_CONTA":
                        this.codConta = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "COD_ESPECIE":
                        this.codEspecie = this.LeCampoTabela<Int32>(campo.Conteudo);
                        break;
                    case "LIMITE":
                        this.limite = this.LeCampoTabela<Double>(campo.Conteudo);
                        break;
                    case "SALDO":
                        this.saldo = this.LeCampoTabela<Double>(campo.Conteudo);
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
