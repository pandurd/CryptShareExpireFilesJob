using System;
using System.IO;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CryptShare
{
    public static class TimerTrigger1
    {
        [FunctionName("TimerTrigger1")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://20.81.110.66/api/Files/Expired");
            httpWebRequest.ContentType = "application/json";
            //httpWebRequest.Headers["Authorization"] = "Bearer **token**";

            httpWebRequest.Method = "DELETE";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 |
            SecurityProtocolType.Tls;

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                log.LogInformation(result);
            }
            
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
