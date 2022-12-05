using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// /// Endpoint encargado de consultar la información de todos las Planetas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var planet = await _planetRepository.GetAll();
            return Ok(planet);
        }

        /// <summary>
        /// Endpoint encargado de consultar la información de un Planeta mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var planet = await _planetRepository.GetById(id);
            return Ok(planet);
        }

        /// <summary>
        /// Endpoint encargado de insertar la información de un Planeta
        /// </summary>
        /// <param name="planet"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Planet planet)
        {
            _ = await _planetRepository.Create(planet);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de actualizar la información de un Planeta
        /// </summary>
        /// <param name="planet"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Planet planet)
        {
            var currentPlanet = await _planetRepository.GetById(planet.Id);

            if (currentPlanet == null)
                return BadRequest("Planet to update not found");

            _ = await _planetRepository.Update(planet);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de eliminar la información de un Planeta mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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