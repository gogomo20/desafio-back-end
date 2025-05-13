using FluentValidation;

namespace StockManager.Aplication.UseCases.Wallets.Commands.AddBalance;

public class AddBalanceCommandValidation : AbstractValidator<AddBalanceCommand>
{
    public AddBalanceCommandValidation()
    {
        RuleFor(x => x.Value)
            .NotNull().WithMessage("Value is required")
            .GreaterThan(0).WithMessage("Value must be greater than 0");
    }
}