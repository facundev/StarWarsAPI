using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StarWarsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanetController : ControllerBase
    {
        private readonly ILogger<PlanetController> _logger;
        private readonly IPlanetRepository _planetRepository;

        public PlanetController(ILogger<PlanetController> logger, IPlanetRepository planetRepository)
        {
            _logger = logger;
            _planetRepository = planetRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de los Planetas", Description = "Endpoint encargado de consultar la información de todos los Planetas")]
        public async Task<IActionResult> Get()
        {
            var planet = await _planetRepository.GetAll();
            return Ok(planet);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de los Planetas mediante Id", Description = "Endpoint encargado de consultar la información de un Planeta mediante su Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var planet = await _planetRepository.GetById(id);
            return Ok(planet);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Inserta la información de un Planeta", Description = "Endpoint encargado de insertar la información de un Planeta")]
        public async Task<IActionResult> Create(Planet planet)
        {
            _ = await _planetRepository.Create(planet);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Actualiza la información de un Planeta", Description = "Endpoint encargado de actualizar la información de un Planeta")]
        public async Task<IActionResult> Update(Planet planet)
        {
            var currentPlanet = await _planetRepository.GetById(planet.Id);

            if (currentPlanet == null)
                return BadRequest("Planet to update not found");

            _ = await _planetRepository.Update(planet);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Elimina la información de un Planeta", Description = "Endpoint encargado de eliminar la información de un Planeta")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentPlanet = await _planetRepository.GetById(id);

            if (currentPlanet == null)
                return BadRequest("Planet to delete not found");

            _ = await _planetRepository.Delete(id);
            return Ok();
        }
    }
}