using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockManager.Aplication.Responses;
using StockManager.Attributes;
using StockManager.UseCases.UseCases.Users.Queries.Get;

namespace StockManager.Filters;
/// <summary>
/// This filter verifies if the user has the permission to perform the action
/// pass on [Permission("YOUR_PERMISSION")]
/// </summary>
public class PermissionsFilter : IAsyncActionFilter
{
    private readonly IMediator _mediator;
    public PermissionsFilter(IMediator mediator) => _mediator = mediator;
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = context.HttpContext.User;
        var permissionAttribute = context
                    .ActionDescriptor
                    .EndpointMetadata
                    .OfType<PermissionAttribute>()
                    .Select(x => x.Permission)
                    .FirstOrDefault();
        if (permissionAttribute == null) {
            await next();
            return;
        }
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var roles = (user.Claims.FirstOrDefault(x => x.Type == "permissions")?.Value ?? "").Split(',');

        if (!roles.Contains(permissionAttribute))
        {
            var resultObject = new GenericResponseNoData()
            {
                Errors = new[] { "You do not have permission to perform this action" }
            };
            context.Result = new ObjectResult(resultObject)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
            return;
        }
        await next();
    }
}