using FluentValidation;
using WebRestApi.Dto;

namespace WebRestApi.Validators
{
    public class PaymentValidator : AbstractValidator<PaymentDto>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.SocketId)
                .NotNull()
                .NotEmpty();
            
            RuleFor(p => p.ProductId)
                .NotNull()
                .NotEmpty();
            
            RuleFor(p => p.Email)
                .EmailAddress()
                .WithMessage("Invalid Email Address");

            RuleFor(p => p.Card.CardNumber)
                .CreditCard()
                .WithMessage("Number card invalid!");

            RuleFor(p => p.Card.CardName)
                .NotNull().NotEmpty();

            RuleFor(p => p.Card.CardExpiry)
                .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$")
                .WithMessage("Date format MM/AA.");

            RuleFor(p => p.Card.CardCvv)
                .Matches(@"^\d{3,4}$")
                .WithMessage("Contains 3 or 4");
        }
    }
}
