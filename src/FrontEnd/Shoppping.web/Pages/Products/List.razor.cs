using Microsoft.AspNetCore.Components;
using Shopping.Application.Dto.Products;
using Shopping.Domain.BackOffice.Commum;
using Shoppping.web.Interfaces;

namespace Shoppping.web.Pages.Products;

public partial class ListProductPage : ComponentBase
{
    protected int TotalPage;
    protected int PageCurrent;
    private const int PageSize = 25;

    protected List<ProductDto>? Products;
    protected Error? Error;

    [Inject] public NavigationManager Nav { get; set; } = null!;
    [Inject] public IProductService ProductService { get; set; } = null!;

    protected override void OnInitialized()
    {
        PageCurrent = 1;
        GetNewPage();
    }

    protected async void GetNewPage()
    {
        var result = await ProductService.GetAllAsync(PageCurrent, PageSize);
        if (!result.IsSuccess)
        {
            Error = result.Error;
            StateHasChanged();
            return;
        }

        Products = result.Value.Value;
        TotalPage = result.Value.TotalPage;
        PageCurrent = result.Value.Page;
        
        StateHasChanged();
    }
}