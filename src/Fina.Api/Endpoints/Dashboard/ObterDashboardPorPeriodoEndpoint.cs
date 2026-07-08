using Fina.Api.Common.Api;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Dashboard;

public class ObterDashboardPorPeriodoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
        .WithName("Dashboard: Obter por dashboard período")
        .WithSummary("Dashboard: Retorna dados da dashboard período")
        .WithDescription("Dashboard: Retorna dados da dashboard período")
        .WithOrder(1)
        .Produces<Response<Core.Models.Dashboard?>>();

    private static async Task<IResult> HandleAsync()
    {
        var dashboards = new List<Core.Models.Dashboard> 
        {
           new Core.Models.Dashboard { Titulo = "Saldo Geral", Valor = 48275.42m, PercentualVariacao = 8.4m, Icon = "Wallet", CorCss = "from-emerald-500/20 to-cyan-500/5 text-emerald-400", PositivoOk = true },
           new Core.Models.Dashboard { Titulo = "Receitas do mês", Valor = 500.00m, PercentualVariacao  = 1.2m, Icon = "TrendingUp", CorCss = "from-emerald-500/20 to-emerald-500/0 text-emerald-400", PositivoOk  = true },
           new Core.Models.Dashboard { Titulo = "Despesas do mês", Valor = 4020.89m, PercentualVariacao = 3.2m, Icon = "TrendingDown", CorCss = "from-rose-500/20 to-rose-500/0 text-rose-400", PositivoOk = false },
        };

        return TypedResults.Ok(dashboards);
    }
}
