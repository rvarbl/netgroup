using System.Net;

namespace Base.Contracts.Validation;

public interface IErrorResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public HttpStatusCode Status { get; set; }
    public string TraceId { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }
}