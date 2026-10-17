using Microsoft.AspNetCore.Components;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Domain.BackOffice.Commum;
using Shoppping.web.Interfaces;
using Shoppping.web.Security;
using Shoppping.web.Services;

namespace Shoppping.web.Pages.User;

public partial class RegisterPage : ComponentBase
{

    protected bool Loading =false;
    protected RegisterUserRequest? Request { get; set; }= null;
    protected  Error? Error = null;

    [Inject]
    public NavigationManager Nav { get; set; } = null!;
    [Inject]
    public LocalStorage LocalStorage { get; set; } = null!;
    [Inject]
    public IAuthService Service { get; set; } = null!;
    [Inject]
    public JwtAuthProvider JwtAuthProvider { get; set; } = null!;


    protected override async Task OnInitializedAsync()
    {
        if (await JwtAuthProvider.IsAuthenticated())
        {
            Nav.NavigateTo("/");
            return;
        }

        Request = new RegisterUserRequest();
    }

    protected async Task OnValidSubmitAsync()
    {
        try
        {
            Console.WriteLine("Não deu erro 0");

            Loading = true;
            var result = await Service.RegisterAsync(Request, CancellationToken.None);
            Console.WriteLine("Não deu erro");
            if (!result.IsSuccess)
            {
                Error = result.Error;
                StateHasChanged();
                return;
            }
            Console.WriteLine("Não deu erro 2");
            LocalStorage.Insert(result.Value);
            Console.WriteLine("Não deu erro 3");
            await JwtAuthProvider.UpdateAuthenticationStateAsync();
            Console.WriteLine("Não deu erro 4");
            StateHasChanged();
            Nav.NavigateTo("/");
        }
        finally
        {
            Loading = false;
        }
    }
}