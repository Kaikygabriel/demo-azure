using System.ComponentModel.DataAnnotations;
using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Application.UsesCases.Users.Command.Request;

public record RegisterUserRequest
: IRequest<ResultValue<AuthUserResponse>>
{
    [Required]
    public string Name { get;set; }
    [Required]
    [EmailAddress]
    public string Email { get;set; }
    [Required]
    public string Password { get;set; }
    [Required]
    public int Cep { get;set; }
    [Required]
    public string Address { get;set; }
    
    public ResultValue<User> ToEntity()
    {
        var emailResult = Domain.BackOffice.ValueObjects.Email.Factory.Create(Email);
        if (!emailResult.IsSuccess)
            return emailResult.Error;

        var passwordResult = Domain.BackOffice.ValueObjects.Password.Factory.Create(Password);
        if (!passwordResult.IsSuccess)
            return passwordResult.Error;

        return new User(emailResult.Value, passwordResult.Value, new Localization(Address, Cep), Name);
    }
}