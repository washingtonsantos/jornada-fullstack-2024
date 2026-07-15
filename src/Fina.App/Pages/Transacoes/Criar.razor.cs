using Fina.Core.Enums;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.App.Pages.Transacoes;

public partial class CriarTransacaoPage : ComponentBase
{
    #region Propriedades

    public bool IsBusy { get; set; } = false;
    public CriarTransacaoRequest InputModel { get; set; } = new();
    public List<Categoria> Categorias { get; set; } = [];
    private List<Categoria> CategoriasTemp { get; set; } = [];
    public List<SubCategoria> SubCategorias { get; set; } = [];
    private List<SubCategoria> SubCategoriasTemp { get; set; } = [];
    public List<Conta> Contas { get; set; } = [];
    public TipoPagamentoRecebimento TipoPagamentoRecebimento { get; set; } = TipoPagamentoRecebimento.Pix;

    #endregion

    #region Services
    [Inject]
    public ITransacaoHandler Handler { get; set; } = null!;

    [Inject]
    public ICategoriaHandler CategoriasHandler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    #region Métodos Override
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        InputModel.TipoTransacao = TipoTransacao.Despesa;

        var result = await CategoriasHandler.ObterTodosAsync(new Core.Requests.Categorias.ObterTodasCategoriasRequest { PageNumber = 1, PageSize = 25 });
        if (result.Sucesso && result.Data?.Count > 0)
        {
            CategoriasTemp = result.Data;
            SetCategoria(TipoTransacao.Despesa);
        }

        IsBusy = false;
    }
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
                NavigationManager.NavigateTo("/transacoes");
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

    public void OnTipoTransacaoChanged(TipoTransacao tipoTransacao)
    {
        if (CategoriasTemp is null) return;
        SetCategoria(tipoTransacao);
    }

    public void OnCategoriaChanged(Guid categoriaId)
    {
        
    }

    #endregion

    #region Métodos Privados

    private void SetCategoria(TipoTransacao tipoTransacao)
    {
        Categorias = CategoriasTemp.Where(c => c.TipoCategoria == tipoTransacao).ToList();
        InputModel.CategoriaId = Categorias.FirstOrDefault()!.Id;
    }

    #endregion
}
