using FluentValidation;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Permissions.Commands.Create;

public class CreatePermissionCommandValidation : AbstractValidator<CreatePermissionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreatePermissionCommandValidation(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MustAsync(NamesAlreadyExists).WithMessage("Permission name already exists");
        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Description is too long, max 255 characters");
    }

    private async Task<bool> NamesAlreadyExists(string? name, CancellationToken cancellationToken)
    {
        return !await _unitOfWork.GetRepositoryAsync<Permission>().AnyAsync(x => x.Name == name);    
    }
}