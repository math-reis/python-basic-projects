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
        ValorTransacaoMenorIgualZero,
        CamposProibidosSacarDepositar,
        ContaInexistente,
        SaldoInsuficiente,
        RN1_AgenciaInvalida,
        RN2_ContaInvalida,
        RN3_EspecieInvalida,
        TipoPessoaInvalido,
        RN4_LimiteInvalido,
        RN5_SaldoInicialInvalido
    }

    /// <summary>Classe de mensagens para a RN de Conta</summary>
    public class MensagemConta : Mensagem
    {
        public MensagemConta(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.RN5_SaldoInicialInvalido:
                    this.mensagem = "Saldo inicial deve ser zero.";
                    break;
                case TipoFalha.RN4_LimiteInvalido:
                    this.mensagem = "Limite deve ser maior que zero.";
                    break;
                case TipoFalha.TipoPessoaInvalido:
                    this.mensagem = "Tipo de pessoa deve ser F ou J";
                    break;
                case TipoFalha.RN3_EspecieInvalida:
                    this.mensagem = "Espécie inválida. Deve ser 35 para pessoa física e 06 para pessoa jurídica.";
                    break;
                case TipoFalha.RN2_ContaInvalida:
                    this.mensagem = "Conta inválida, deve ser maior que zero.";
                    break;
                case TipoFalha.RN1_AgenciaInvalida:
                    this.mensagem = "Agência inválida, deve ser maior que zero.";
                    break;
                case TipoFalha.SaldoInsuficiente:
                    this.mensagem = string.Format("Saldo insuficiente. Total disponível para saque é {0:C}.", parametro[0]);
                    break;
                case TipoFalha.ContaInexistente:
                    this.mensagem = "Conta inexistente!";
                    break;
                case TipoFalha.CamposProibidosSacarDepositar:
                    this.mensagem = "Não é permitido alterar informações da conta nos métodos Sacar/Depositar.";
                    break;
                case TipoFalha.ValorTransacaoMenorIgualZero:
                    this.mensagem = "Valor da transação deve ser maior que zero.";
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
