namespace Base.Contracts.Validation;

public interface IValidator
{
    public void SetError(string errorName, string errorMessage);
    public IErrorResponse GetResponse();
}