using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categorias;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.App.Pages.Categorias;

public partial class GetAllCategoriesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Categoria?> Categorias { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services

    [Inject]
    private ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private ICategoriaHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new ObterTodasCategoriasRequest { PageSize = 25, PageNumber = 1 };
            var result = await Handler.ObterTodosAsync(request);
            if (result.Sucesso)
                Categorias = result.Data ?? [];
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
        var result = await DialogService.ShowMessageBox(
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
            IsBusy = true;

            var result = await Handler.RemoverAsync(
                new RemoverCategoriaRequest
                {
                    Id = id
                });

            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem, Severity.Info);
                Categorias.RemoveAll(x => x.Id == id);
            }
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
