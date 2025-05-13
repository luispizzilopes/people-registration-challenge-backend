using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PeopleAPI.Domain.Exception;
using PeopleAPI.Shared.Common;
using System.Net;

namespace PeopleAPI.Presentation.Middlewares; 

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        Result? result = Result.Failure("Ocorreu um erro durante a execução do EndPoint!");

        if (ex is DomainException domainException)
            result = Result.Failure(domainException.Message);

        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        var resultSerializeObject = JsonConvert.SerializeObject(result, settings);

        return context.Response.WriteAsync(resultSerializeObject);
    }
}
