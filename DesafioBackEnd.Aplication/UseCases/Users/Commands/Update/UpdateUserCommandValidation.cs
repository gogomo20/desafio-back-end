using FluentValidation;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.UseCases.UseCases.Users.Commands.Update;

public class UpdateUserCommandValidation : AbstractValidator<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandValidation(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required")
            .MustAsync(UsernamesExists).WithMessage("UserName already exists");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid")
            .MustAsync(EmailExists).WithMessage("Email already exists");
        When(x => x.NewPassword != null, () =>
        {
            RuleFor(x => x.NewPassword)
                .MinimumLength(8).WithMessage("Password must be at least 8 characters");
        });
    }

    private async Task<bool> UsernamesExists(UpdateUserCommand command,string? userName, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.GetRepositoryAsync<User>()
            .AnyAsync(x => x.UserName == userName && x.Id != command.Id, cancellationToken);
    }

    private async Task<bool> EmailExists(UpdateUserCommand command, string? Email, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.GetRepositoryAsync<User>()
            .AnyAsync(x => x.Email == Email && x.Id != command.Id, cancellationToken);
    }
}