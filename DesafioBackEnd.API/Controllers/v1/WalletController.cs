using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql.Internal;
using StockManager.Aplication.Responses;
using StockManager.Aplication.UseCases.Wallets.Commands.AddBalance;
using StockManager.Aplication.UseCases.Wallets.Commands.RemoveBallance;
using StockManager.Aplication.UseCases.Wallets.Commands.TransferValue;
using StockManager.Aplication.UseCases.Wallets.Queries;
using StockManager.Aplication.UseCases.Wallets.Responses;
using StockManager.Attributes;

namespace StockManager.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class WalletController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalletController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("balance")]
    [Permission("GET_BALANCE")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<decimal>))]
    public async Task<IActionResult> GetBalance(CancellationToken cancellationToken)
    {
        var query = new GetBalance();
        var response = await _mediator.Send(query, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }
    [HttpPut("deposit")]
    [Permission("ADD_BALANCE")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<decimal>))]
    public async Task<IActionResult> Deposit([FromBody] AddBalanceCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }
    [HttpPut("withdraw")]
    [Permission("REMOVE_BALANCE")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<decimal>))]
    public async Task<IActionResult> Withdraw([FromBody] RemoveBalanceCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }
    [HttpPost("transfer")]
    [Permission("CREATE_TRANSFERENCE")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<decimal>))]
    public async Task<IActionResult> Transfer([FromBody] TransferValueCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("history-transference")]
    [Permission("LIST_TRANSFERENCE")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(DefaultApiResponse<ICollection<TransferenceResponse>>))]
    public async Task<IActionResult> ListTransference(
        [FromBody] ListTransference query,
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        CancellationToken cancellationToken = default)
    {
        query.Page = page;
        query.Size = size;
        var response = await _mediator.Send(query, cancellationToken);
        return Ok(response);
    }
}