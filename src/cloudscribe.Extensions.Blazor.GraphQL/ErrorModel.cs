using System.Collections.Generic;

namespace cloudscribe.Extensions.Blazor.GraphQL
{
    public class ErrorModel
    {
        public List<ApiError> Errors { get; set; }
    }

    public class ApiError
    {
        public string Message { get; set; }
    }
}
