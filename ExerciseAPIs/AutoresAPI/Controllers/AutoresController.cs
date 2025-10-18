using AutoresBusiness;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace AutoresAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ILogger<AutoresController> _logger;

        public AutoresController(ILogger<AutoresController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAutoresFromFile")]
        public IEnumerable<Autores> Get()
        {
            return new AutoresB().GetAutoresFromFile();
        }

        [HttpGet("GetAutoresByName")]
        public IEnumerable<Autores> Get(string name)
        {
            return new AutoresB().GetAutoresByName(name);
        }
    }
}
