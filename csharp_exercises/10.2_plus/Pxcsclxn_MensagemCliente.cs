using System;
using System.Collections.Generic;
using System.Text;
using Bergs.Pxc.Pxcoiexn;

namespace Bergs.Pxc.Pxcsclxn
{
    /// <summary>Tipos de falha para a RN de Cliente</summary>
    public enum TipoFalha
    {
        CampoInvalido,
        RN1,
        RN2,
        RN3,
        RN4,
        PessoaInexistente
        //TODO: incluir demais erros previstos
    }

    /// <summary>Classe de mensagens para a RN de Cliente</summary>
    public class MensagemCliente : Mensagem
    {
        public MensagemCliente(TipoFalha tipoFalha, params string[] parametro)
        {
            switch (tipoFalha)
            {
                case TipoFalha.PessoaInexistente:
                    this.mensagem = string.Format("CPF ou CPNJ inexistente.");
                    break;
                case TipoFalha.RN4:
                    this.mensagem = string.Format("Nome deve ter 2 (dois) nomes e no mínimo 2 (duas) letras no primeiro nome.");
                    break;
                case TipoFalha.RN3:
                    this.mensagem = string.Format("CNPJ inválido.");
                    break;
                case TipoFalha.RN2:
                    this.mensagem = string.Format("CPF inválido.");
                    break;
                case TipoFalha.RN1:
                    this.mensagem = string.Format("Tipo pessoa deve ser 'F' ou 'J'.");
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
