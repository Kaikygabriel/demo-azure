using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Shopping.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class StripeController : ControllerBase
{
    // private readonly ISender _sender;
    //
    // public StripeController(ISender sender)
    // {
    //     _sender = sender;
    // }

    [HttpPost("/web-hook")]
    public async Task<ActionResult> WebHook()
    { 
        try
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                "whsec_2f724b9098e584144cc212289accd31c1f7337a7540ec9afb82f9e892b53feac"
            );

            switch (stripeEvent.Type)
            {
                case "invoice.payment_succeeded":
                    var invoice = stripeEvent.Data.Object as Invoice;
                    // O pagamento da assinatura deu certo! 
                    // Renove o acesso do usuário até a próxima data de vencimento.
                    break;

                case "customer.subscription.deleted":
                    var subscription = stripeEvent.Data.Object as Subscription;
                    // A assinatura foi cancelada/encerrada de vez.
                    // Bloqueie o acesso do usuário ao sistema.
                    break;

                case "charge.refunded":
                    var charge = stripeEvent.Data.Object as Charge;
                    // Um reembolso foi processado.
                    // Identifique o usuário pelo 'charge.CustomerId' ou metadados e atualize a compra.
                    break;
            }
            
            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}