using System.Net;

namespace WebApp.Api.Validation;

public class AuthenticationValidation
{
    private readonly ErrorResponse _errorResponse = new();

    public void SetResponseBadRequest(string title, string traceId)
    {
        _errorResponse.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
        _errorResponse.Status = HttpStatusCode.BadRequest;
        _errorResponse.Title = title;
        _errorResponse.TraceId = traceId;
    }

    public void SetError(string errorName, string errorMessage)
    {
        if (_errorResponse.Errors.ContainsKey(errorName))
        {
            _errorResponse.Errors[errorName].Add(errorMessage);
        }
        else
        {
            _errorResponse.Errors[errorName] = new List<string> {errorMessage};
        }
    }
    
    public ErrorResponse GetResponse()
    {
        return _errorResponse;
    }
}