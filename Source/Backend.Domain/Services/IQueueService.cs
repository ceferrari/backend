using System.Threading.Tasks;
using Amazon.SQS.Model;

namespace Backend.Domain.Services
{
    public interface IQueueService
    {
        Task<SendMessageResponse> SendToSqsAsync(object messageBody, bool log = true);
    }
}
