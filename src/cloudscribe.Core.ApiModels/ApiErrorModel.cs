using System.Collections.Generic;

namespace cloudscribe.Core.ApiModels
{
    public class ApiErrorModel
    {
        public List<ApiError> Errors { get; set; }
    }

    public class ApiError
    {
        public string Message { get; set; }
    }
}
