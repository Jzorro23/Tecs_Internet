using AutorModels;

namespace TECIFBIntegration.APIServices
{
    public class UserAPIService
    {
        private readonly APIService _service;
        private readonly string _urlGetUser;
        private readonly string _urlRegisterUser;
        private readonly string _urlLoginUser;
        private readonly string _urlUpdateUser;
        private readonly string _urlDeleteUser;

        public UserAPIService()
        {
            _service = new APIService();
            _urlGetUser = "https://localhost:7173/api/usuarios";
            _urlRegisterUser = "https://localhost:7173/api/usuarios/RegistrarUsuario";
            _urlLoginUser = "https://localhost:7173/api/usuarios/Login/";
            _urlUpdateUser = "https://localhost:7173/api/usuarios/UpdateUsuario";
            _urlDeleteUser = "https://localhost:7173/api/usuarios/DeleteUsuario";

        }
        public async Task<List<Usuario>> GetUsers()
        {
            var parameters = new List<string> { };
            var res = await _service.GetRequest<List<Usuario>>(_urlGetUser, parameters);
            return res;
        }
        public async Task<List<UsuarioDTOC>> RegisterUser(UsuarioDTOC usuario)
        {
            var res = await _service.PostRequest<List<UsuarioDTOC>, UsuarioDTOC>(_urlRegisterUser, usuario);
            return res;
        }

        public async Task<UsuarioDTOC> LoginUser(string email, string password)
        {

            var parameters = new Dictionary<string, string>
        {
            { "email", email },
            { "Password", password }
        };


            var user = await _service.PostRequestWithParameter<UsuarioDTOC, object>(_urlLoginUser, parameters, null);

            return user;
        }


        public async Task<Usuario> DeleteUsuario(string id)
        {
            var res = await _service.DeleteRequest<Usuario>(_urlDeleteUser, id);
            return res;
        }


        public async Task<Usuario> UpdateUsuario(string id, UsuarioDTOC updatedUsuario)
        {
            var res = await _service.PutRequest<Usuario, UsuarioDTOC>(_urlUpdateUser, id, updatedUsuario);
            return res;
        }
    }
}
