using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StarWarsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarshipController : ControllerBase
    {
        private readonly ILogger<StarshipController> _logger;
        private readonly IStarshipRepository _starshipRepository;

        public StarshipController(ILogger<StarshipController> logger, IStarshipRepository starshipRepository)
        {
            _logger = logger;
            _starshipRepository = starshipRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de Naves", Description = "Endpoint encargado de consultar la información de todas las Naves")]
        public async Task<IActionResult> Get()
        {
            var starship = await _starshipRepository.GetAll();
            return Ok(starship);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de Naves mediante Id", Description = "Endpoint encargado de consultar la información de una Nave mediante su Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var starship = await _starshipRepository.GetById(id);
            return Ok(starship);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Inserta la información de una Nave", Description = "Endpoint encargado de insertar la información de una Nave")]
        public async Task<IActionResult> Create(Starship starship)
        {
            _ = await _starshipRepository.Create(starship);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Actualiza la información de una Nave", Description = "Endpoint encargado de actualizar la información de una Nave")]
        public async Task<IActionResult> Update(Starship starship)
        {
            var currentStarship = await _starshipRepository.GetById(starship.Id);

            if (currentStarship == null)
                return BadRequest("Starship to update not found");

            _ = await _starshipRepository.Update(starship);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Elimina la información de una Nave", Description = "Endpoint encargado de eliminar la información de una Nave")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentStarship = await _starshipRepository.GetById(id);

            if (currentStarship == null)
                return BadRequest("Starship to delete not found");

            _ = await _starshipRepository.Delete(id);
            return Ok();
        }
    }
}