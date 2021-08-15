using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcscoxn
{
    /// <summary>Tipos de falha para a RN de Conta</summary>
    public enum TipoFalha
    {
        CampoInvalido,
        ValorTransacaoInvalido,
        SaldoInsuficiente
    }

    /// <summary>Classe de mensagens para a RN de Conta</summary>
    public class MensagemConta : Mensagem
    {
        public MensagemConta(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.CampoInvalido:
                    this.mensagem = string.Format("Campo {0} inválido.", parametro[0]);
                    break;
                case TipoFalha.ValorTransacaoInvalido:
                    this.mensagem = "Valor da transação deve ser maior que zero.";
                    break;
                case TipoFalha.SaldoInsuficiente:
                    this.mensagem = String.Format(
                        "Saldo insuficiente. Total disponível para saque é {0:N}.",
                        Double.Parse(parametro[0])
                        );
                    break;
                default:
                    break;
            }
        }
    }
}
