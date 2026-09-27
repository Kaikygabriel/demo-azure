using Shopping.Application.UsesCases.Users.Command.Response;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Shopping.Domain.BackOffice.ValueObjects;

namespace Shopping.Application.UsesCases.Users.Command.Request;

public record RegisterUserRequest(string Email, string Password, string Name, string Address, int Cep)
: IRequest<ResultValue<AuthUserResponse>>
{
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