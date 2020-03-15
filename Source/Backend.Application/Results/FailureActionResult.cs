using System.Net;

namespace Backend.Application.Results
{
    public class FailureActionResult : AbstractActionResult
    {
        public FailureActionResult(HttpStatusCode code, object data)
        {
            Code = code;
            Status = Failure;
            Data = data;
        }
    }
}
