using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseCheck1
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.Data;
    using Microsoft.AspNetCore.Mvc;

    public static class LCheck
    {
        private static List<EmailCounter> emailList = new List<EmailCounter>();
        private static int reset_hour = 0;
        [FunctionName("CheckLicense")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string email = req.Form["email"];

            ResetNumberOfCons();

            EmailCounter existingEmail = emailList.FirstOrDefault(e => e.email == email);

            if (existingEmail != null)
            {
                existingEmail.number_of_cons++;
            }
            else
            {
                emailList.Add(new EmailCounter { email = email, number_of_cons = 1 });
            }

            return new OkObjectResult(existingEmail?.number_of_cons ?? 1);
        }

        private static void ResetNumberOfCons()
        {
            int currentHour = DateTime.UtcNow.Hour;
            if (currentHour > reset_hour || (reset_hour == 23 && currentHour != 23))
            {
                foreach (EmailCounter emailObj in emailList)
                {
                    emailObj.number_of_cons = 0;
                }
                reset_hour = currentHour;
            }
        }
    }
}
