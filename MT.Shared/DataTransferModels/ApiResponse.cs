using System.Collections.Generic;
using System.Net;

namespace MT.Shared.DataTransferModels;

public class ApiResponse<T> where T : class
{
    public T Data { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Messages { get; set; } = new List<string>();
}

public class ApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public List<string> Messages { get; set; } = new List<string>();

    public ApiResponse()
    { }

    public ApiResponse(HttpStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Messages = new List<string>
        {
            message
        };
    }
}
