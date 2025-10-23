using AutorBusiness;
using AutorModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoresAPI.Controllers
{
    [ApiController]

    [Route("api/usuarios")]

    public class UserController : ControllerBase

    {

        public UserController()

        {

        }

        // con modelos Usuarios

        // metodo para listar los Autores (get)

        [HttpGet(Name = "GetUsuarios")]

        public ActionResult<List<Usuario>> GetUsuarios()

        {

            return new ABusiness().getUsuarios();

        }

        // metodo para registrar un usuario (POST)

        [HttpPost("RegistrarUsuario", Name = "RegistrarUsuario")]

        public ActionResult RegistrarUsuario(UsuarioDTOC nuUsuarioDTOC)

        {

            try

            {

                new ABusiness().RegisterUsuario(nuUsuarioDTOC);

                return Ok("Registro Exitoso");

            }

            catch (Exception ex)

            {

                return BadRequest("Error en el registro: " + ex.Message);

            }

        }

        // metodo para logear un usuario (POST)

        [HttpPost("Login/Usuario", Name = "LoguearUsuario")]

        public ActionResult LoguearUsuario(string email, string Password)

        {

            try

            {

                new ABusiness().LoginUsuario(email, Password);

                return Ok("Ha iniciado sesión correctamente");

            }

            catch (Exception ex)

            {

                return BadRequest("Error en el inicio de sesión: " + ex.Message);

            }

        }

        // metodo para Actualizar la informacion de un usuario (Put)

        [HttpPut("UpdateUsuario/{id}", Name = "UpdateUsuario")]

        public ActionResult UpdateUsuario(Guid id, UsuarioDTOC UsuarioActualizado)

        {

            try

            {

                // Busca el recurso existente por el Id proporcionado

                var UsuarioExistente = new ABusiness().getUsuarios().FirstOrDefault(r => r.Id == id);

                if (UsuarioExistente == null)

                {

                    return NotFound("Usuario no encontrado");

                }

                // Actualiza solo los campos necesarios

                UsuarioExistente.Name = UsuarioActualizado.Name;

                UsuarioExistente.Password = UsuarioActualizado.Password;

                UsuarioExistente.Email = UsuarioActualizado.Email;

                // Pasa el id y el recurso actualizado

                new ABusiness().UpdateUsuario(id, UsuarioExistente);

                return Ok("Usuario actualizado correctamente");

            }

            catch (Exception ex)

            {

                return BadRequest("Error al actualizar el Usuario: " + ex.Message);

            }

        }

        // metodo para eliminar un usuario (Delete)

        [HttpDelete("DeleteUsuario/{id}", Name = "DeleteUsuario")]

        public ActionResult DeleteResource(Guid id)

        {

            try

            {

                new ABusiness().DeleteUsuario(id);

                return Ok("Recurso eliminado correctamente");

            }

            catch (Exception ex)

            {

                return BadRequest("Error al eliminar el Recurso: " + ex.Message);

            }

        }

    }

}
