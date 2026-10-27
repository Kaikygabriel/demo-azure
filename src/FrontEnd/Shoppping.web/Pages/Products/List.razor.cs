using Microsoft.AspNetCore.Components;
using Shopping.Application.Dto.Products;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Shoppping.web.Interfaces;

namespace Shoppping.web.Pages.Products;

public partial class ListProductPage : ComponentBase
{
    protected int TotalPage;
    protected int PageCurrent;
    private const int PageSize = 25;

    
    protected List<Category>? Categories;
    protected Error? CategoryError;
    
    protected List<ProductDto>? Products;
    protected Error? ProductError;

    [Inject] public NavigationManager Nav { get; set; } = null!;
    [Inject] public IProductService ProductService { get; set; } = null!;
    [Inject] public ICategoryService CategoryService { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        PageCurrent = 1;
        var result = Task.Run(GetCategories);
        GetNewPage();
        await result;
    }

    protected async void GetNewPage()
    {
        try
        {
            var result = await ProductService.GetAllAsync(PageCurrent, PageSize);
            if (!result.IsSuccess)
            {
                ProductError = result.Error;
                StateHasChanged();
                return;
            }

            Products = result.Value.Value;
            TotalPage = result.Value.TotalPage;
            PageCurrent = result.Value.Page;

            StateHasChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            StateHasChanged();

        }
    }

    private async void GetCategories()
    {
        try
        {
            var result = await CategoryService.GetAllAsync();
            if (!result.IsSuccess)
            {
                CategoryError = result.Error;
            }

            Categories = result.Value.Value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            StateHasChanged();
        }
    }
}