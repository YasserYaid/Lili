using Newtonsoft.Json;
using ReposteriaLili_Front.Models.Constantes;
using ReposteriaLili_Front.Models.ReqResModels;
using ReposteriaLili_Front.Services.IServices;
using System.Text;

namespace ReposteriaLili_Front.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseMoldel { get; set; }
        public IHttpClientFactory _httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseMoldel = new APIResponse();
            _httpClient = httpClient;
        }

        public async Task<T?> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("ReposteriaLiliAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.ApiUrlDestino);

                if(apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                switch (apiRequest.OperacionHTTP)
                {
                    case OpcionesAPI.OperacionHTTP.POST:
                         message.Method = HttpMethod.Post;
                         break;
                    case OpcionesAPI.OperacionHTTP.PUT:
                         message.Method = HttpMethod.Put;
                         break;
                    case OpcionesAPI.OperacionHTTP.DELETE:
                         message.Method = HttpMethod.Delete;
                         break;
                    case OpcionesAPI.OperacionHTTP.GET: 
                         message.Method = HttpMethod.Get;
                         break;
                    default :
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    isExitoso = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T> (res);
                return APIResponse;
            }
        }
    }
}
