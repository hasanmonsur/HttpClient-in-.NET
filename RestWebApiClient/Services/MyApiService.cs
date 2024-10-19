namespace RestWebApiClient.Services
{
    public class MyApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");

            var response = await client.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                // Handle error (you can implement a retry or fallback mechanism here)
                throw new HttpRequestException("Error fetching data.");
            }
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");
            var response = await client.PostAsJsonAsync(endpoint, data);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                // Handle error (retry logic can be applied here too)
                throw new HttpRequestException("Error posting data.");
            }
        }
    }
}
