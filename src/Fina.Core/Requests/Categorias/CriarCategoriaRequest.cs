using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categorias;

public class CriarCategoriaRequest : Request
{
    [Required(ErrorMessage = "Título inválido")]
    [StringLength(80, ErrorMessage = "O título deve ter entre {2} e {0} caracteres.", MinimumLength = 3)]
    public string Titulo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;
}
