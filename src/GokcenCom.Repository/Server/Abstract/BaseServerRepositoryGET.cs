using System.Text.Json;

namespace GokcenCom.Repository.Server
{
    public abstract class BaseServerRepositoryGET<T> where T : class
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5259")
        };

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public virtual async Task<T?> GetAsync(string endPoint)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(endPoint);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"GET Response Error: {response.StatusCode}");
                    return default;
                }

                string responseData = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<T>(responseData, jsonOptions);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GET Exception: {ex.Message}");
                return default(T);
            }
        }
    }
}