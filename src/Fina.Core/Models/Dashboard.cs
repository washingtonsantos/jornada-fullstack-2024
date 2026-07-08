namespace Fina.Core.Models;

public class Dashboard
{
    public string Titulo { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public decimal PercentualVariacao { get; set; }
    public string Icon { get; set; } = string.Empty;
    public bool PositivoOk { get; set; }
    public string CorCss { get; set; } = string.Empty;
}
