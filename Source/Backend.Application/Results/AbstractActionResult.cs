using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Backend.Shared.Utilities;

namespace Backend.Application.Results
{
    public abstract class AbstractActionResult : IActionResult
    {
        protected const string Success = "success";
        protected const string Failure = "failure";

        public HttpStatusCode Code { internal get; set; }
        public string Status { internal get; set; }
        public object Data { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)Code;
            context.HttpContext.Response.ContentType = "application/json";
            await new ObjectResult(Data).ExecuteResultAsync(context);
        }

        public override string ToString() => JsonUtilities.Serialize(this);
    }
}
