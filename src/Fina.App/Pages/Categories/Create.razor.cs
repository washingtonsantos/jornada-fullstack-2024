using Fina.Core.Handlers;
using Fina.Core.Requests.Categorias;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.App.Pages.Categorias;

public partial class CreateCategoryPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public CriarCategoriaRequest InputModel { get; set; } = new();
    #endregion

    #region Services
    [Inject]
    public ICategoriaHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Methods
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.CriarAsync(InputModel);

            if (result.Sucesso)
            {
                Snackbar.Add(result.Mensagem, Severity.Success);
                NavigationManager.NavigateTo("/categorias");
            }
            else
                Snackbar.Add(result.Mensagem, Severity.Error);
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
}
