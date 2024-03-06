using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
namespace Project.PaymentService.MomoPayment.MomoPaymentConfiguration
{
    public class VNPayPaymentMethod
    {
        public static async Task<string> SendPaymentRequest(string endpoint, object postJsonObject)
        {
            try
            {
                using HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(postJsonObject);
                var jsonRequest = JsonNode.Parse(json);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = client.PostAsJsonAsync(endpoint, jsonRequest).Result;
                var streamRead = await result.Content.ReadAsStringAsync();
                var jsonResult = JObject.Parse(streamRead);
                return jsonResult.ToString();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());
                return null;
            }

        }

    }
}
