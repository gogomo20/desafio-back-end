using MediatR;
using StockManager.Aplication.Responses;

namespace StockManager.Aplication.UseCases.Permissions.Commands;

public class PermissionCommandBase : IRequest<GenericResponse<long>>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}