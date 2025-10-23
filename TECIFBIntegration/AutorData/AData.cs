using AutorModels;
using System.Text.Json;

namespace AutorData
{
    public class AData
    {
        private string filePath = Environment.CurrentDirectory + "/Files/authors.txt";
        private string fileTxt = Environment.CurrentDirectory + "/Files/Usuarios.txt";
        public AData()
        {
        }
        // modelo autores 
        // obtenemos la lista de los Autores en TXT para luego buscar en ella
        public List<Autor> getAuthorsFromFile()
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Archivo no Existente");
            }
            var ContDatos = File.ReadAllText(filePath);
            var authors = JsonSerializer.Deserialize<List<Autor>>(ContDatos);

            return authors ?? new List<Autor>();
        }
        // modelo Usuario
        // obtenemos la lista de los Usuarios en TXT para luego buscar en ella
        public List<Usuario> getUsuariosFromFile()
        {
            if (!File.Exists(fileTxt))
            {
                throw new FileNotFoundException("Archivo no Existente");
            }
            var ContDatos = File.ReadAllText(fileTxt);
            var Usuarios = JsonSerializer.Deserialize<List<Usuario>>(ContDatos);

            return Usuarios ?? new List<Usuario>();
        }


        // funcion registrar usuario con dtoc
        public void RegistrarUsuario(UsuarioDTOC nuUsuarioDTOC)
        {


            if (!File.Exists(fileTxt))
            {
                throw new FileNotFoundException("Archivo no Existente");
            }

            var ContDatos = File.ReadAllText(fileTxt);
            var Usuarios = JsonSerializer.Deserialize<List<Usuario>>(ContDatos) ?? new List<Usuario>();

            Usuario nuU = new Usuario();
            nuU.Name = nuUsuarioDTOC.Name;
            nuU.Email = nuUsuarioDTOC.Email;
            nuU.Password = nuUsuarioDTOC.Password;
            nuU.Id = Guid.NewGuid();

            Usuarios.Add(nuU);

            var jsonActualizado = JsonSerializer.Serialize(Usuarios, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileTxt, jsonActualizado);
        }
        //funcion para validad login de usuario 
        public void LoginUsuario(string email, string password)
        {


            if (!File.Exists(fileTxt))
            {
                throw new FileNotFoundException("Archivo no Existente");
            }

            var contDatos = File.ReadAllText(fileTxt);
            var usuarios = JsonSerializer.Deserialize<List<Usuario>>(contDatos) ?? new List<Usuario>();

            // Verifica si el correo existe en la lista de usuarios
            var usuario = usuarios.FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
            if (usuario == null)
            {
                throw new Exception("El correo no existe en el sistema.");
            }

            // Verifica si la contraseña coincide con el correo encontrado
            if (usuario.Password != password)
            {
                throw new Exception("Contraseña incorrecta.");
            }
        }

        public void UpdateUsuario(Guid id, Usuario UsuarioActualizado)
        {
            if (!File.Exists(fileTxt))
            {
                throw new FileNotFoundException("Archivo no existente");
            }

            var ContDatos = File.ReadAllText(fileTxt);
            var Usuarios = JsonSerializer.Deserialize<List<Usuario>>(ContDatos) ?? new List<Usuario>();

            // Busca el recurso por el Id
            var UsuarioExistente = Usuarios.Find(r => r.Id == id);
            if (UsuarioExistente == null)
            {
                throw new Exception("El recurso no existe.");
            }

            // Actualiza las propiedades necesarias
            UsuarioExistente.Name = UsuarioActualizado.Name ?? UsuarioExistente.Name;
            UsuarioExistente.Password = UsuarioActualizado.Password ?? UsuarioExistente.Password;
            UsuarioExistente.Email = UsuarioActualizado.Email ?? UsuarioExistente.Email;

            try
            {
                var jsonActualizado = JsonSerializer.Serialize(Usuarios, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileTxt, jsonActualizado);
            }
            catch (Exception ex)
            {
                throw new IOException("Error al escribir el archivo actualizado: " + ex.Message);
            }
        }

        // Método para eliminar un Usuario (ID)
        public void DeleteUsuario(Guid id)
        {
            if (!File.Exists(fileTxt))
            {
                throw new FileNotFoundException("Archivo no Existente");
            }

            var ContDatos = File.ReadAllText(fileTxt);
            var Resources = JsonSerializer.Deserialize<List<Usuario>>(ContDatos) ?? new List<Usuario>();

            var UsuarioExistente = Resources.Find(r => r.Id == id);
            if (UsuarioExistente == null)
            {
                throw new Exception("El recurso no existe.");
            }

            Resources.Remove(UsuarioExistente);

            var jsonActualizado = JsonSerializer.Serialize(Resources, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(fileTxt, jsonActualizado);
        }


    }
}
