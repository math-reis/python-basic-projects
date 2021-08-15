using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.PxcsFIxn
{
    /// <summary>Tipos de falha para a RN de Financiamento</summary>
    public enum TipoFalha
    {
        CampoInvalido,
        RN1,
        RN2,
        RN3,
        RN4,
        RN5,
        RN6,
        RN7
        //TODO: incluir demais erros previstos
    }

    /// <summary>Classe de mensagens para a RN de Financiamento</summary>
    public class MensagemFinanciamento : Mensagem
    {
        public MensagemFinanciamento(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.RN6:
                    this.mensagem = string.Format("O financiamento atual não pode ser aprovado porque o cliente estará comprometendo em mais de 30% a sua renda.");
                    break;
                case TipoFalha.RN5:
                    this.mensagem = string.Format("Valor da parcela maior que o limite mensal de endividamento.");
                    break;
                case TipoFalha.RN4:
                    this.mensagem = string.Format("Informe a situação do financiamento.");
                    break;
                case TipoFalha.RN2:
                    this.mensagem = string.Format("Número de parcelas do financiamento deve estar compreendido entre 12 e 48 parcelas, inclusive.");
                    break;
                case TipoFalha.RN1:
                    this.mensagem = string.Format("Cliente não encontrado.");
                    break;
                case TipoFalha.CampoInvalido:
                    this.mensagem = string.Format("Campo {0} inválido.", parametro[0]);
                    break;
                default:
                    break;
            }
        }
    }
}
