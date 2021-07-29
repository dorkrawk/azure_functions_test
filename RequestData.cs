using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace My.Functions
{
    public static class RequestData
    {
        [FunctionName("RequestData")]
        public static async Task Run([TimerTrigger("0 c")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            HttpClient client = new HttpClient();
            // This url is for a small local API I have
            String test_url = "http://localhost:4567/emojify/lizard";
            try
            {
                HttpResponseMessage response = await client.GetAsync(test_url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                log.LogInformation($"response: {responseBody}");
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");	
                Console.WriteLine("Message :{0} ",e.Message);
            }
        }
    }
}
