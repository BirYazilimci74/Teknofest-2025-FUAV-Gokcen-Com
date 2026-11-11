using System.Text;
using System.Text.Json;

namespace GokcenCom.Repository.Server
{
    public abstract class BaseServerRepositoryPOST<T> where T : class
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5259")
        };

        public virtual async Task<bool> PostAsync(string endPoint, T data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(endPoint,content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"POST Response Error: {response.StatusCode}");
                    return false;
                }
                
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Post successfully: {responseBody}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"POST Exception: {ex.Message}");
                return false;
            }
        }
    }
}