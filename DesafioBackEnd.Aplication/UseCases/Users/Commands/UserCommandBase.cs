using MediatR;
using StockManager.Aplication.Responses;

namespace StockManager.UseCases.UseCases.Users.Commands;

public class UserCommandBase : IRequest<GenericResponse<long>>
{
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }

}