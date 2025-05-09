using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManager.Aplication.Responses;
using StockManager.Aplication.UseCases.Permissions.Commands.Create;
using StockManager.Aplication.UseCases.Permissions.Commands.Update;
using StockManager.Aplication.UseCases.Permissions.Queries.Get;
using StockManager.Aplication.UseCases.Permissions.Responses;

namespace StockManager.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<long>))]
    public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.Success ? Created("", response) : BadRequest(response);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<long>))]
    public async Task<IActionResult> UpdatePermission([FromRoute] long id, [FromBody] UpdatePermissionCommand request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await _mediator.Send(request, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<PermissionResponse>))]
    public async Task<IActionResult> GetPermission([FromRoute] long id, CancellationToken cancellationToken)
    {
        var query = new GetPermission { Id = id };
        var response = await _mediator.Send(query, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}