using FluentValidation;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;
using StockManager.Domain.Entities;
using StockManager.Repositories;

namespace StockManager.Aplication.UseCases.PermissionGroups.Commands.Create
{
    public class CreatePermissionGroupValidation : AbstractValidator<CreatePermissionGroupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreatePermissionGroupValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The name is required.")
                .MustAsync(PermissionGroupExists).WithMessage("The permission group already exists.");
            var acceptedStatus = new[] { "A", "I" };
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("The status is required.")
                .Must(x => acceptedStatus.Contains(x)).WithMessage("The status is invalid.");
        }

        private async Task<bool> PermissionGroupExists(string? name, CancellationToken cancellationToken)
        {
            return !(await _unitOfWork.GetRepositoryAsync<PermissionGroup>().AnyAsync(x => x.Name == name));
        }
    }
}
