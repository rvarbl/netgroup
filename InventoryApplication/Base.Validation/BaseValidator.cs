using Base.Contracts.Validation;

namespace Base.Validation;

public class BaseValidator : IValidator
{
    private readonly IErrorResponse _errorResponse;

    public BaseValidator(IErrorResponse errorResponse)
    {
        _errorResponse = errorResponse;
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

    public IErrorResponse GetResponse()
    {
        return _errorResponse;
    }
}