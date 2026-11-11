using System.Text;
using System.Text.Json;
using GokcenCom.Domain.Models;

namespace GokcenCom.Repository.Server
{
    public class LoginSR : BaseServerRepositoryPOST<LoginRequest>
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5259")
        };

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<LoginResponse> Login(string endPoint, LoginRequest request)
        {
            try
            {
                string json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(endPoint,content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine();
                    new LoginResponse{
                        response_message = $"Login Response Error: {response.StatusCode}",
                        takimNumarasi = 0
                    };
                }
                
                var responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<LoginResponse>(responseBody,jsonOptions);
                Console.WriteLine($"Login Response: {data?.response_message}, {data?.takimNumarasi}");

                return new LoginResponse{
                    response_message = $"Post Login Successfully: {responseBody}",
                    takimNumarasi = data.takimNumarasi
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                return new LoginResponse{
                    response_message = $"Login Exception: {ex.Message}",
                    takimNumarasi = 0
                };
            }
        }
        
    }
}