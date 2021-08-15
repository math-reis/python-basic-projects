using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcsfixn
{
    /// <summary>Tipos de falha para a RN de Financiamento</summary>
    public enum TipoFalha
    {
        CampoInvalido,
        FalhaClienteNaoEncontrado,
        FalhaNumeroParcelas,
        FalhaSituacao,
        FalhaRenda30,
        FalhaRenda30Somatorio
    }

    /// <summary>Classe de mensagens para a RN de Financiamento</summary>
    public class MensagemFinanciamento : Mensagem
    {
        public MensagemFinanciamento(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.CampoInvalido:
                    this.mensagem = string.Format("Campo {0} inválido.", parametro[0]);
                    break;
                case TipoFalha.FalhaClienteNaoEncontrado:
                    this.mensagem = "Cliente não encontrado.";
                    break;
                case TipoFalha.FalhaNumeroParcelas:
                    this.mensagem = "Número de parcelas do financiamento deve estar compreendido entre 12 e 48 parcelas, inclusive.";
                    break;
                case TipoFalha.FalhaSituacao:
                    this.mensagem = "Informe a situação do financiamento.";
                    break;
                case TipoFalha.FalhaRenda30:
                    this.mensagem = "Valor da parcela maior que o limite mensal de endividamento.";
                    break;
                case TipoFalha.FalhaRenda30Somatorio:
                    this.mensagem = "O financiamento atual não pode ser aprovado porque o cliente estará comprometendo em mais de 30% da sua renda.";
                    break;
                default:
                    break;
            }
        }
    }
}
