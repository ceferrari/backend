using System.Net;

namespace Backend.Application.Results
{
    public class SuccessActionResult : AbstractActionResult
    {
        public SuccessActionResult(HttpStatusCode code, object data)
        {
            Code = code;
            Status = Success;
            Data = data;
        }
    }
}
