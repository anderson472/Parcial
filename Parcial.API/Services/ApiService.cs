using Newtonsoft.Json;
using Parcial.Shared.Responses;

namespace Parcial.API.Services
{
    public class ApiService : IApiService
    {



        public async Task<Response> GetListAsync<T>(string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new()
                {
                   
                };

       
                string url = $"{servicePrefix}{controller}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                List<T> list = JsonConvert.DeserializeObject<List<T>>(result)!;
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
