using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaApi.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // Determinamos el código HTTP según el tipo de excepción
            var (statusCode, title) = exception switch
            {
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
                ArgumentException => (StatusCodes.Status400BadRequest, "Solicitud inválida"),
                _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor")
            };

            // Construimos la respuesta estándar RFC 7807 (ProblemDetails)
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; // Indicamos que la excepción ya fue manejada
        }
    }
}