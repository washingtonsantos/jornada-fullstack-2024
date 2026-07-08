using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Bancos;

public class CriarBancoRequest : Request
{
    [Required(ErrorMessage = "Nome inválido")]
    [StringLength(80, ErrorMessage = "O nome deve ter entre {2} e {0} caracteres.", MinimumLength = 3)]
    public string Nome { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
