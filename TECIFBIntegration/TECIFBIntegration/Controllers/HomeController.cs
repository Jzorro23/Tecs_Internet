using AutorModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TECIFBIntegration.APIServices;
using TECIFBIntegration.Models;

namespace TECIFBIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string varibale  = "Hola";
            int numero = 10;
            if (varibale == "Hola")
            {
                numero++;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> AutorAsync()
        {
            AutorAPIService apiService = new AutorAPIService();

            List<Autor> autores = new List<Autor>();
            autores = await apiService.getAutores();

            return View(autores);
        }

        public IActionResult Login()
        {
            return View();
        }
        public async Task<IActionResult> AdminAsync()
        {
            var UsersService = new UserAPIService();

            var viewModel = new GetsViewModel
            {
                Usuario = await UsersService.GetUsers()
            };

            return View(viewModel);
        }

        //Inicio Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                UserAPIService userService = new UserAPIService();
                var usuario = await userService.LoginUser(email, password);

                TempData["Message"] = "Inicio de Sesión Exitoso.";
                return RedirectToAction("Admin");


            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                ModelState.AddModelError("", "No se pudo registrar el usuario.");
                return View("Error", new ErrorViewModel() { RequestId = "016-No se pudo Iniciar Sesión, verifique sus credenciales." });
            }
        }


        // Metodo para registrar usuario 
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UsuarioDTOC User)
        {
            try
            {
                UserAPIService UserAPIService = new UserAPIService();
                var UsuariosExistentes = await UserAPIService.GetUsers();

                if (UsuariosExistentes.Any(u => u.Email == User.Email))
                {
                    ModelState.AddModelError("", "No se pudo registrar el usuario.");
                    return View("Login", new ErrorViewModel() { RequestId = "006-El usuario ya esta registrado" });
                }
                else
                {
                    {
                        List<UsuarioDTOC> newSuscribers = await UserAPIService.RegisterUser(User);

                        TempData["Message"] = "Usuario Registrado correctamente.";
                        return RedirectToAction("Login");
                    }

                }

            }
            catch
            {
                ModelState.AddModelError("", "No se pudo registrar el usuario.");
                return View("Error", new ErrorViewModel() { RequestId = "013-No se pudo registrar el usuario." });
            }
        }

        // metodo para eliminar Usuario
        [HttpPost]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            try
            {
                UserAPIService UserAPIService = new UserAPIService();

                Usuario deletedUsuario = await UserAPIService.DeleteUsuario(id);
                TempData["Message"] = $"Usuario con ID {id} ha sido eliminado correctamente.";
                return RedirectToAction("Admin");

            }
            catch
            {
                ModelState.AddModelError("", "No se pudo eliminar el Usuario.");
                return View("Error", new ErrorViewModel() { RequestId = "001-No se pudo eliminar el suscriptor" });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUsuario(string id, UsuarioDTOC updatedUsuario)
        {
            try
            {
                UserAPIService UsuarioService = new UserAPIService();
                Usuario updated = await UsuarioService.UpdateUsuario(id, updatedUsuario);
                TempData["Message"] = $"Recurso con ID {id} ha sido actualizado correctamente.";
                return RedirectToAction("Admin");
            }
            catch
            {
                ModelState.AddModelError("", "No se pudo Actualizar el Recurso.");
                return View("Error", new ErrorViewModel() { RequestId = $"002-No se pudo Actualizar el Recurso, el Id ingresado no es valido, porfavor verificar" });
            }
        }



        // funcion para buscar autor por aname 
        public async Task<IActionResult> searchAuthorByNameAsync(String aname)
        {
            AutorAPIService apiService = new AutorAPIService();

            List<Autor> autores = new List<Autor>();
            autores = await apiService.getAuthorsByName(aname);

            return View("Autor", autores);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
