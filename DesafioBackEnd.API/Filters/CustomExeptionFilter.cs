using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using StockManager.Aplication.Responses;

namespace StockManager.Filters;

public class CustomExeptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException validationException:
                var errors = validationException.Errors.Select(error => $"{error.ErrorMessage}").ToList();
                var result = new ObjectResult(new GenericResponseNoData()
                {
                    Errors = errors.ToArray()
                })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
                context.Result = result;
                context.ExceptionHandled = true;
                break;
            case KeyNotFoundException keyNotFoundException:
                var notFoundResult = new ObjectResult(new GenericResponseNoData()
                {
                    Message = "Register not Found",
                    Errors = new[] { keyNotFoundException.Message }
                })
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
                context.Result = notFoundResult;
                context.ExceptionHandled = true;
                break;
                
        }
    }
}