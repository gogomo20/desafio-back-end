using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockManager.Aplication.Responses;
using StockManager.Aplication.UseCases.PermissionGroups.Commands.Create;
using StockManager.Aplication.UseCases.Permissions.Commands.Update;

namespace StockManager.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PermissionGroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<long>))]
        public async Task<IActionResult> CreateAsync([FromBody] CreatePermissionGroupCommand command, CancellationToken cancellation)
        {
            var response = await _mediator.Send(command, cancellation);
            return response.Success ? new ObjectResult(response) { StatusCode = StatusCodes.Status201Created } : BadRequest(response);
        }
        [HttpPut("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<long>))]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdatePermissionCommand command, CancellationToken cancellation)
        {
            command.Id = id;
            var response = await _mediator.Send(command, cancellation);
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
