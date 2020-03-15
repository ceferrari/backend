using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Backend.Shared.Utilities;
using Serilog;

namespace Backend.Domain.Services
{
    public class QueueService : IQueueService
    {
        private readonly AmazonSQSClient _amazonSqsClient;

        public QueueService(AmazonSQSClient amazonSqsClient)
        {
            _amazonSqsClient = amazonSqsClient;
        }

        public async Task<SendMessageResponse> SendToSqsAsync(object messageBody, bool log = true)
        {
            var request = new SendMessageRequest
            {
                QueueUrl = "https://sqs.us-east-1.amazonaws.com/105029661252/start-checkout", // Leandro
                // QueueUrl = "https://sqs.us-east-1.amazonaws.com/106868270748/start-checkout", // Carlos
                MessageBody = JsonUtilities.Serialize(messageBody)
            };

            var response = await _amazonSqsClient.SendMessageAsync(request);

            if (log && !string.IsNullOrEmpty(response.MessageId))
            {
                Log.Information("Sent to SQS: {data}", messageBody);
            }

            return response;
        }
    }
}
