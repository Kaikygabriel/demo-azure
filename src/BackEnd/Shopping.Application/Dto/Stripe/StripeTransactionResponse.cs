namespace Shopping.Application.Dto.Stripe;

public record StripeTransactionResponse(string Id,long Amount,long AmountCaptured,string Status,string Email,bool Paid,bool Refound);