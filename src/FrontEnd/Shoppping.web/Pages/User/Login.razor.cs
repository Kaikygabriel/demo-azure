using Microsoft.AspNetCore.Components;
using Shopping.Application.UsesCases.Users.Command.Request;
using Shopping.Domain.BackOffice.Commum;
using Shoppping.web.Interfaces;
using Shoppping.web.Security;
using Shoppping.web.Services;

namespace Shoppping.web.Pages.User;

public partial class LoginPage : ComponentBase
{
    protected bool Loading;
    protected Error? Error;
    protected LoginUserRequest Request { get; set; } = new();

    [Inject]
    public LocalStorage LocalStorage { get; set; }
    [Inject]
    public  IAuthService AuthService{ get; set; }
    [Inject]
    public  NavigationManager Nav{ get; set; }
    [Inject]
    public  JwtAuthProvider JwtAuthProvider{ get; set; }
    
    protected LoginPage()
    {
        
    }

    

    public async Task LoginAsync()
    {
        try
        {
            Loading = true;

            var result = await AuthService.LoginAsync(Request, CancellationToken.None);
            Console.WriteLine("Exceutou");
            if (!result.IsSuccess)
            {
                Console.WriteLine("Passou pelo errado");
                Console.WriteLine(result.Error.Message);
                Error = result.Error;
                return;
            }

            Console.WriteLine("Executou esse");
            LocalStorage.Insert(result.Value);
            StateHasChanged();
            Nav.NavigateTo("/");

        }
        catch (Exception e)
        {
            Console.WriteLine("Exception");
            Console.WriteLine(e);
        }
        finally
        {
            Loading = false;
        }
    }
}