using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManager.Aplication.Responses;
using StockManager.Aplication.UseCases.Users.Commands.Create;
using StockManager.Attributes;
using StockManager.UseCases.UseCases.Users.Commands.Update;
using StockManager.UseCases.UseCases.Users.Queries.Get;
using StockManager.UseCases.UseCases.Users.Responses;

namespace StockManager.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Permission("CREATE_USER")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<long>))]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id:long}")]
    [Permission("UPDATE_USER")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<long>))]
    public async Task<IActionResult> UpdateUser([FromRoute] long id, [FromBody] UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        request.Id = id;
        var response = await _mediator.Send(request, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:long}")]
    [Permission("GET_USER")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<UserResponse>))]
    public async Task<IActionResult> GetUser([FromRoute] long id, CancellationToken cancellationToken)
    {
        var response = new GetUserById() { Id = id };
        var result = await _mediator.Send(response, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    [HttpDelete("{id:long}")]
    [Permission("DELETE_USER")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<long>))]
    public async Task<IActionResult> DeleteUser([FromRoute] long id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}