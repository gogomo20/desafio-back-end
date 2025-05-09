using System.Data;
using FluentValidation;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Users.Commands.Create;

public class CreateUserCommandValidation : AbstractValidator<CreateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateUserCommandValidation(IUnitOfWork unitOfWork)
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
            .MustAsync(EmailsExists).WithMessage("Email already exists");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters");
    }

    private async Task<bool> EmailsExists(string email, CancellationToken cancellationToken)
    {
        
        return !await _unitOfWork.GetRepositoryAsync<User>().AnyAsync(x => x.Email == email, cancellationToken);
    }

    private async Task<bool> UsernamesExists(string? userName, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.GetRepositoryAsync<User>().AnyAsync(x => x.UserName == userName, cancellationToken);
    }
}