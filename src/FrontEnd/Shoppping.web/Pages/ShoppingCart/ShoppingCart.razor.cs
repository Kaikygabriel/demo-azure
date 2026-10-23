using Microsoft.AspNetCore.Components;
using Shopping.Application.Dto.ShoppingCart;
using Shopping.Domain.BackOffice.Commum;
using Shoppping.web.Interfaces;
using Shoppping.web.Services;

namespace Shoppping.web.Pages.ShoppingCart;

public partial class ShoppingCart : ComponentBase
{
    [Inject] public IShoppingCartService ShoppingCartService { get; set; } = null!;
    [Inject] public LocalStorage LocalStorage { get; set; } = null!;

    private ShoppingCartDto? _shoppingCartDto;
    private Error? _error;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ShoppingCartService.GetByUserId(LocalStorage.Get()!.Id);
            if (!result.IsSuccess)
                _error = result.Error;
            else
                _shoppingCartDto = result.Value;
            
            StateHasChanged();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}