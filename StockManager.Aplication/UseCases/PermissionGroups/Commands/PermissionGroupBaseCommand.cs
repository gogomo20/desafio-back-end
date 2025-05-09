using MediatR;
using StockManager.Aplication.Responses;

namespace StockManager.Aplication.UseCases.PermissionGroups.Commands
{
    public class PermissionGroupBaseCommand : IRequest<GenericResponse<long>>
    {
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
