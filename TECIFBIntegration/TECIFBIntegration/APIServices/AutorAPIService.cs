using AutorModels;

namespace TECIFBIntegration.APIServices
{
    public class AutorAPIService

    {

        private readonly APIService _service;

        private readonly string _urlRegistrarAutor;

        private readonly string _urlGetAutorById;

        private readonly string _urlGetAutores;

        public AutorAPIService()

        {

            _service = new APIService();

            _urlGetAutorById = "https://localhost:7173/api/autors/";

            _urlGetAutores = "https://localhost:7173/api/autors/";

        }



        public async Task<List<Autor>> getAuthorsByName(string aname)

        {

            var parameters = new List<string> { { aname } };

            var res = await _service.GetRequest<List<Autor>>(_urlGetAutorById, parameters);

            return res;

        }

        public async Task<List<Autor>> getAutores()

        {

            var parameters = new List<string> { };

            var res = await _service.GetRequest<List<Autor>>(_urlGetAutores, parameters);

            return res;

        }

    }

}
