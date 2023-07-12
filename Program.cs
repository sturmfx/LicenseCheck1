using LicenseCheck1;

public class Program
{
    public static async Task Main()
    {
        var client = new Client();

        
        var startTime = DateTime.UtcNow.Date.AddHours(DateTime.UtcNow.Hour).AddMinutes(DateTime.UtcNow.Minute);
        var interval = TimeSpan.FromMinutes(5);

        while (true)
        {
            var email = "example@example.com"; // емейл для отправки
            await client.SendEmail(email);
            await Task.Delay(interval);

            startTime += interval;
            var delay = startTime - DateTime.UtcNow;
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay);
            }
        }
    }
}