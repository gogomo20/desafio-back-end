using FluentValidation;
using StockManager.Domain.Entities;
using StockManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Aplication.UseCases.PermissionGroups.Commands.Update
{
    public class UpdatePermissionGroupValidation : AbstractValidator<UpdatePermissionGroupCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePermissionGroupValidation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name not can be empty or null.")
                .MustAsync(NameExists).WithMessage("Name already exists.");
            var statusOptions = new[] { "A", "I" };
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status not can be empty or null.")
                .Must(x => statusOptions.Contains(x)).WithMessage("Status must be A - Active or I - Inactive.");
        }
        private async Task<bool> NameExists(UpdatePermissionGroupCommand command, string? name, CancellationToken cancellationToken)
        {            
            return !(await _unitOfWork.GetRepositoryAsync<PermissionGroup>().AnyAsync(x => x.Name == name && x.Id != command.Id));
        }
    }
}
