using FluentValidation;
using MediatR;
using StockManager.Aplication.JWTRepository;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Wallets.Commands.RemoveBallance;

public class RemoveBalanceValidation : AbstractValidator<RemoveBalanceCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserContext _userContext;
    public RemoveBalanceValidation(IUnitOfWork unitOfWork, UserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
        RuleFor(x => x.Value)
            .NotNull().WithMessage("Value is required")
            .GreaterThan(0).WithMessage("Value must be greater than 0")
            .MustAsync(HaveBalanceEnoughValidation).WithMessage("You don't have enough balance");
    }
    public async Task<bool> HaveBalanceEnoughValidation(decimal value, CancellationToken cancellationToken)
    {
        var userId = _userContext.Id;
        var wallet = await _unitOfWork.GetRepositoryAsync<Wallet>()
            .SingleOrDefaultAsync(x => x.UserId == userId);
        if (wallet == null)
        {
            return false;
        }

        return wallet.Balance >= value;
    }
}