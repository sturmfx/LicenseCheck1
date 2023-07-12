using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseCheck1
{
    public class Client
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string functionAppUrl = "юрл функции";

        public async Task SendEmail(string email)
        {
            try
            {
                var parameters = new Dictionary<string, string>
            {
                { "email", email }
            };

                var content = new FormUrlEncodedContent(parameters);
                var response = await httpClient.PostAsync(functionAppUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var numberOfCons = await response.Content.ReadAsStringAsync();
                    //переменная содержит в себе количество коннектов для емейла за час (так как каждый час ресетится) 0 - 13 является 100% норм
                }
                else
                {
                    //при фейле коннекта
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
