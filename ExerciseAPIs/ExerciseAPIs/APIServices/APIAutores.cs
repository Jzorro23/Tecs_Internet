using Models;
using TECIFBIntegration.APIServices;

namespace ExerciseAPIs.APIServices
{
    public class APIAutores
    {
        private readonly APIService _service;
        private readonly string _urlGetAutores;
        private readonly string _urlGetAutoresByName;

        public APIAutores()
        {
            _service = new APIService();
            _urlGetAutores = "https://localhost:7279/Autores/GetAutoresFromFile";
            _urlGetAutoresByName = "https://localhost:7279/Autores/GetAutoresByName";
        }

        public async Task<List<Autores>> getAutores()
        {
            var parameter = new List<string> { };
            var res = await _service.GetRequest<List<Autores>>(_urlGetAutores, parameter);
            return res;
        }

        public async Task<List<Autores>> getAutoresByName(string name)
        {
            string Url = $"{_urlGetAutoresByName}?name={name}";
            var parameter = new List<string>();
            var res = await _service.GetRequest<List<Autores>>( Url, parameter);
            return res;
        }
    }
}
