using Fina.Core.Common;
using Fina.Core.Dtos.Transacoes;
using Fina.Core.Handlers;
using Fina.Core.Requests.Transacoes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.App.Pages.Transacoes;

public partial class ObterTodasTransacoesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<TransacaoPorPeriodoDto?> Transacoes { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private ITransacaoHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var hoje = DateTime.UtcNow.Date;
            var request = new ObterTransacaoPorPeriodoRequest
            {
                PageSize = 25,
                PageNumber = 1,
                DataInicio = new DateTime(hoje.Year, hoje.Month, 1),
                DataFim = new DateTime(hoje.Year, hoje.Month, hoje.GetLastDay(hoje.Year, hoje.Month).Day)
            };
            var result = await Handler.ObterTransacaoPorPeriodoAsync(request);
            if (result.Sucesso)
                Transacoes = result.Data ?? [];
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

    #region Methods

    public async void OnDeleteButtonClickedAsync(Guid id, string title)
    {
        var result = await DialogService.ShowMessageBoxAsync(
            title: "Atenção",
            message: $"Você confirma a exclusão da categoria '{title}' ?",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteasync(id, title);

        StateHasChanged();
    }

    public async Task OnDeleteasync(Guid id, string title)
    {
        try
        {
            //IsBusy = true;

            //var result = await Handler.RemoverAsync(
            //    new RemoverCategoriaRequest
            //    {
            //        Id = id
            //    });

            //if (result.Sucesso)
            //{
            //    Snackbar.Add(result.Mensagem, Severity.Info);
            //    Transacoes.RemoveAll(x => x.Id == id);
            //}
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Success);
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion
}
