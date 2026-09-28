using Shopping.Domain.BackOffice.Commum;

namespace Shopping.Application.UsesCases.Order.Command.Request;

public record AlterStatusOrderRequest(Guid OrderId) : IRequest<ResultValue<Guid>>;