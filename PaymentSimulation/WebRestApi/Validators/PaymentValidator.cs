using FluentValidation;
using WebRestApi.Dto;

namespace WebRestApi.Validators
{
    public class PaymentValidator: AbstractValidator<PaymentDto>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.SocketId).NotNull().NotEmpty();
            RuleFor(p => p.ProductId).NotNull().NotEmpty();
            RuleFor(p => p.Card.CardNumber).CreditCard();
            RuleFor(p => p.Card.CardName).NotNull().NotEmpty();
            RuleFor(p => p.Card.CardExpiry).Length(5);
            RuleFor(p => p.Card.CardCvv).Length(4);
        }
    }
}
