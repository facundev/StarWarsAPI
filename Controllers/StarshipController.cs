using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// /// Endpoint encargado de consultar la información de todos las Naves
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var starship = await _starshipRepository.GetAll();
            return Ok(starship);
        }

        /// <summary>
        /// Endpoint encargado de consultar la información de una Nave mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var starship = await _starshipRepository.GetById(id);
            return Ok(starship);
        }

        /// <summary>
        /// Endpoint encargado de insertar la información de una Nave
        /// </summary>
        /// <param name="starship"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Starship starship)
        {
            _ = await _starshipRepository.Create(starship);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de actualizar la información de una Nave
        /// </summary>
        /// <param name="starship"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Starship starship)
        {
            var currentStarship = await _starshipRepository.GetById(starship.Id);

            if (currentStarship == null)
                return BadRequest("Starship to update not found");

            _ = await _starshipRepository.Update(starship);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de eliminar la información de una Nave mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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