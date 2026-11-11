using System.Text;
using System.Text.Json;
using GokcenCom.Domain.Models;

namespace GokcenCom.Repository.Server
{
    public class TelemetrySR : BaseServerRepositoryPOST<Telemetry>
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5259")
        };

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<OtherUAVList> GetOtherUAVListAsync(string endPoint, Telemetry telemetry)
        {
            try
            {
                string json = JsonSerializer.Serialize(telemetry);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(endPoint,content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Telemetry Response Error: {response.StatusCode}");
                    return new OtherUAVList{
                        sunucusaati = new ServerTime(),
                        konumBilgileri = new List<UAVLocation>()
                    };
                }
                
                var responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<OtherUAVList>(responseBody,jsonOptions);
                
                Console.WriteLine($"Telemetry Response: {data?.konumBilgileri.Count}");
                return data ?? new OtherUAVList{
                    sunucusaati = new ServerTime(),
                    konumBilgileri = new List<UAVLocation>()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Telemetry Exception: {ex.Message}");
                return new OtherUAVList{
                    sunucusaati = new ServerTime(),
                    konumBilgileri = new List<UAVLocation>()
                };
            }
        }
 
    }
}