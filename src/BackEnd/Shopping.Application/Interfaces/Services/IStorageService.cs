using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.Interfaces.Services;

public interface IStorageService
{
    Task<ResultValue<String>> Insert(string base64,string extendsFile);
}