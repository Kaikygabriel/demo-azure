using Shopping.Application.Dto.Stripe;
using Shopping.Application.Interfaces.Repositories;
using Shopping.Application.Interfaces.Services;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Domain.BackOffice.Entities;
using Stripe;
using Stripe.Checkout;

namespace Shopping.Infra.Services;

internal sealed class PayService : IPayService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PayService(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultValue<string>> CreateSession(Order order, CancellationToken cancellationToken = default)
    {
        
        var options = new SessionCreateOptions
        {
            LineItems =
            [
                new SessionLineItemOptions()
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions()
                    {
                        Currency = "BRL",
                        ProductData = new SessionLineItemPriceDataProductDataOptions()
                        {
                          
                            Name = order.Product.Title,
                            Description = order.Product.Summary
                        },
                        UnitAmount = (int)(order.Total * 100)
                    }
                }
            ],
            CustomerEmail = order.User.Email.Address,
            Currency = "BRL",
            CancelUrl = "https://localhost:7208/cancel",
            SuccessUrl = "https://localhost:7208/success",
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            PaymentIntentData = new SessionPaymentIntentDataOptions()
            {
                Metadata =new Dictionary<string, string>
                {
                    {"order",order.Id.ToString()}
                }
            },
            PaymentMethodTypes = ["boleto","card"],
            Mode = "payment"
        };
        var service = new SessionService();
        var result = await service.CreateAsync(options, cancellationToken: cancellationToken);
        return  result.Url;
    }

    public async Task<ResultValue<List<StripeTransactionResponse>>> GetTransactions(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new ChargeSearchOptions()
        {
            Query = $"metadata['order']:'{id}'"
        };
        var service = new ChargeService();
        var result = await service.SearchAsync(query, cancellationToken: cancellationToken);

        if (!result.Data.Any())
            return new Error("Transactions Not Found!");

        return result.Data.Select(x =>
            new StripeTransactionResponse( x.Id, x.Amount, x.AmountCaptured, x.Status, x.Customer.Email, x.Paid,
                x.Refunded)).ToList();  
    }
}