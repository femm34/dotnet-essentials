using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using MiApi.Models.DTOs;
using Microsoft.OpenApi.Extensions;

namespace MiApi.Utils;

public class ApiResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Aquí puedes hacer algo antes de ejecutar la acción si lo necesitas
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            // Obtener el mensaje desde el atributo si existe
            var actionDescriptor = context.ActionDescriptor;
            var messageAttribute = actionDescriptor.EndpointMetadata
                .OfType<ApiResponseMessageAtribute>()
                .FirstOrDefault();

            var message = messageAttribute?.Message ?? "Success";  // Si no hay mensaje, usar "Success"

            var response = new ApiResponse<object>
            {
                Message = message,
                Status = HttpStatusCode.OK.GetDisplayName(),
                Data = objectResult.Value,
                Code = 200
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = 200
            };
        }
        else if (context.Result is NotFoundObjectResult notFoundResult)
        {
            var response = new ApiResponse<object>
            {
                Message = "No users found.",
                Status = HttpStatusCode.NotFound.GetDisplayName(),
                Data = null,
                Code = 404
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = 404
            };
        }
        else if (context.Result is StatusCodeResult statusCodeResult)
        {
            var response = new ApiResponse<object>
            {
                Message = "Internal server error.",
                Status = HttpStatusCode.InternalServerError.GetDisplayName(),
                Data = null,
                Code = statusCodeResult.StatusCode
            };
            context.Result = new ObjectResult(response)
            {
                StatusCode = statusCodeResult.StatusCode
            };
        }
    }
}