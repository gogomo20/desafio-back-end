using FluentValidation;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.Permissions.Commands.Update;

public class UpdatePermissionCommandValidation : AbstractValidator<UpdatePermissionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePermissionCommandValidation(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MustAsync(PermissionAlreadyExists).WithMessage("Permission name already exists");
        RuleFor(x => x.Description)
            .MaximumLength(255).WithMessage("Description is too long, max 255 characters");
    }
    private async Task<bool> PermissionAlreadyExists(UpdatePermissionCommand command, string? name,  CancellationToken cancellationToken)
    {
        return !await _unitOfWork.GetRepositoryAsync<Permission>().AnyAsync(x => x.Name == name && x.Id != command.Id);
    }
}