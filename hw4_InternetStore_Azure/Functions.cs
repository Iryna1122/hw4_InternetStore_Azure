using Microsoft.Azure.WebJobs;

namespace hw4_InternetStore_Azure
{
    public class Functions
    {
        public static void ProcessQueueMessage([QueueTrigger("mynewqueue")] string message, ILogger log)
        {
            log.LogInformation(message);
        }
    }
}
