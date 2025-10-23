using AutorBusiness;
using AutorModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoresAPI.Controllers
{
    [ApiController]

    [Route("api/autors")]

    public class AutorController : ControllerBase

    {

        public AutorController()

        {

        }

        // con modelo Author

        // metodo para listar los Autores (get)

        [HttpGet(Name = "GetAuthors")]

        public ActionResult<List<Autor>> GetAuthors()

        {

            return new ABusiness().getAuthors();

        }

        // metodo para obtener un Autor por nombre (Get)

        [HttpGet("{aname}", Name = "GetAuthorsByName")]

        public async Task<List<Autor>> GetAuthorsByName(string aname)

        {

            return new ABusiness().GetAuthorsByName(aname);

        }

    }

}
