using System.Net;
using Base.Contracts.Validation;
using Base.Validation;

namespace WebApp.Api.Validation;

public class AuthenticationValidation : BaseValidator
{
    private readonly ErrorResponse _errorResponse;

    public AuthenticationValidation(IErrorResponse errorResponse) : base(errorResponse)
    {
        _errorResponse = new ErrorResponse();
    }
    public void SetResponseBadRequest(string title, string traceId)
    {
        _errorResponse.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
        _errorResponse.Status = HttpStatusCode.BadRequest;
        _errorResponse.Title = title;
        _errorResponse.TraceId = traceId;
    }

    

    
}