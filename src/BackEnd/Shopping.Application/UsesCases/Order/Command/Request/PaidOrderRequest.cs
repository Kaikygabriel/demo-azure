using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Command.Request;

public record PaidOrderRequest(Guid OrderId) : IRequest<ResultValue<Guid>>;