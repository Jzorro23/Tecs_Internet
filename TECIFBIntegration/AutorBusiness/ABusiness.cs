using AutorData;
using AutorModels;

namespace AutorBusiness
{
    public class ABusiness
    {
        public ABusiness()

        {

        }

        // modelo Author

        public List<Autor> getAuthors()

        {

            return new AData().getAuthorsFromFile();

        }

        public List<Autor> GetAuthorsByName(string aname)

        {

            return new AData().getAuthorsFromFile().Where(a => a.FullName.Contains(aname, StringComparison.OrdinalIgnoreCase)).ToList();

        }

        // modelo Usuario

        public List<Usuario> getUsuarios()

        {

            return new AData().getUsuariosFromFile();

        }

        // con modelo usuario

        public void RegisterUsuario(UsuarioDTOC nuUsuarioDTOC)

        {

            new AData().RegistrarUsuario(nuUsuarioDTOC);

        }

        public void LoginUsuario(string email, string password)

        {

            new AData().LoginUsuario(email, password);

        }

        public void UpdateUsuario(Guid id, Usuario UsuarioActualizado)

        {

            new AData().UpdateUsuario(id, UsuarioActualizado);

        }

        public void DeleteUsuario(Guid id)

        {

            new AData().DeleteUsuario(id);

        }

    }

}