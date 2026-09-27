using Shopping.Application.Dto.Stripe;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;

namespace Shopping.Application.Interfaces.Services;

public interface IPayService
{
    Task<ResultValue<string>> CreateSession(Order order,CancellationToken cancellationToken = default);
    Task<ResultValue<List<StripeTransactionResponse>>> GetTransactions(Guid id,CancellationToken cancellationToken = default);
}