using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;

namespace TECIFBIntegration.APIServices
{
    public class APIService
    {
        private System.Net.Http.HttpClient _httpClient;
        public APIService() { }

        // apiServiceGet
        public async Task<TResponse> GetRequest<TResponse>(string url, List<string> parameters) where TResponse : class
        {
            try
            {
                var queryParams = "";
                foreach (var parameter in parameters)
                {
                    queryParams = queryParams + parameter + "/";
                }
                _httpClient = new HttpClient();

                var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, url + queryParams) {
                });

                response.EnsureSuccessStatusCode();
                var stringData = await response.Content.ReadAsStringAsync();
                TResponse data = JsonConvert.DeserializeObject<TResponse>(stringData);

                return data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // ApiServicePost
        public async Task<TResponse> PostRequest<TResponse, TRequest>(string url, TRequest requestObject) where TResponse : class where TRequest : class
        {
            try
            {
                String result = string.Empty;

                _httpClient = new HttpClient();

                var request = JsonConvert.SerializeObject(requestObject);

                using (_httpClient = new HttpClient())
                {

                    string jsonString = JsonConvert.SerializeObject(requestObject, Formatting.Indented);
                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await _httpClient.PostAsync(url, content);
                    result = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return null;
                    }
                    else
                    {
                        TResponse data = JsonConvert.DeserializeObject<TResponse>(result);
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        //apiServicePut
        public async Task<TResponse> PutRequest<TResponse, TRequest>(string url, string id, TRequest requestObject) where TResponse : class where TRequest : class
        {
            try
            {
                String result = string.Empty;

                _httpClient = new HttpClient();

                var request = JsonConvert.SerializeObject(requestObject);

                using (_httpClient = new HttpClient())
                {

                    string jsonString = JsonConvert.SerializeObject(requestObject, Newtonsoft.Json.Formatting.Indented);
                    StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await _httpClient.PutAsync(url + "/" + id, content);
                    result = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return null;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new Exception("Id no existente, verifique porfavor");
                    }
                    else
                    {
                        TResponse data = JsonConvert.DeserializeObject<TResponse>(result);
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        


        // apiServicedelete
        public async Task<TResponse> DeleteRequest<TResponse>(string url, string id) where TResponse : class
        {
            try
            {
                if (!Guid.TryParse(id, out Guid guidId))
                {
                    throw new ArgumentException("El ID no es un Guid válido.");
                }

                string guidString = guidId.ToString("D"); // Convertir al formato estándar
                _httpClient = new HttpClient();

                var response = await _httpClient.DeleteAsync(url + "/" + guidString);
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (response.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        TResponse data = JsonConvert.DeserializeObject<TResponse>(result);
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }



                else
                {
                    throw new Exception("La respuesta no es JSON: " + result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //post login 
        public async Task<TResponse> PostRequestWithParameter<TResponse, TRequest>(
             string url, Dictionary<string, string> parameters, TRequest requestObject)
             where TResponse : class
             where TRequest : class
        {
            try
            {
                String result = string.Empty;
                // Construir la cadena de consulta con los parámetros
                string queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));

                // Serializar el objeto de solicitud (si es necesario, aunque puede no serlo si no tienes que enviar nada en el cuerpo)
                string jsonString = JsonConvert.SerializeObject(requestObject, Formatting.Indented);
                StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    // Configurar encabezados
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Concatenar la URL con la cadena de consulta
                    var fullUrl = $"{url}?{queryString}";

                    // Enviar solicitud (puede que no necesites enviar contenido si solo son parámetros de consulta)
                    var response = await httpClient.PostAsync(fullUrl, content);
                    result = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return null;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        throw new Exception("Usuario inexistente");
                    }
                    else
                    {
                        TResponse data = JsonConvert.DeserializeObject<TResponse>(result);
                        return data;
                    }

                }
            }
            catch (HttpRequestException httpEx)
            {
                // Manejo específico para errores HTTP
                throw new Exception("Error en la solicitud HTTP: " + httpEx.Message);
            }
            catch (Exception ex)
            {
                // Manejo general de errores
                throw new Exception("Error inesperado: " + ex.Message);
            }
        }

        internal Task<T> GetRequest<T>(string urlGetAutores, T parameter)
        {
            throw new NotImplementedException();
        }
    }
}
