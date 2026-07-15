using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Enums;

public enum TipoPagamentoRecebimento
{
    [Display(Name = "Pix")]
    Pix = 1,

    [Display(Name = "Cartão de Crédito")]
    CartaoCredito = 2,

    [Display(Name = "Cartão de Débito")]
    CartaoDebito = 3,

    [Display(Name = "Dinheiro")]
    Dinheiro = 4,

    [Display(Name = "Depósito Bancario")]
    DepositoBancario = 5,

    [Display(Name = "Débito Automático")]
    DebitoAutomatico = 6,

    [Display(Name = "Débito em Conta")]
    DebitoConta = 7,

    [Display(Name = "Outros")]
    Outros = 9
}
