using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace DirectPrintFromWebUsingDesktopCli
{
    public static class ApiService
    {
        private static string ApiUrl { get; set; } = string.Empty;

        public static bool GetConfiguration()
        {
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                ApiUrl = config["ApiUrl"] ?? "";

                return !string.IsNullOrEmpty(ApiUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public static async Task<ReceiptModel> GetData(string invoiceId)
        {
            try
            {
                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(ApiUrl + invoiceId);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(json))
                    {
                        return JsonSerializer.Deserialize<ReceiptModel>(json, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? new ReceiptModel();
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to fetch data. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new ReceiptModel();
        }
    }
}