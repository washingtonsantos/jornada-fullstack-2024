using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Bancos;

public class AtualizarBancoRequest : Request
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Nome inválido")]
    [StringLength(80, ErrorMessage = "O nome deve ter entre {2} e {0} caracteres.", MinimumLength = 3)]
    public string Nome { get; set; } = string.Empty;

    public decimal Valor { get; set; }
    public string Descricao { get; set; } = string.Empty;

}
