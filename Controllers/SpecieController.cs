using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace StarWarsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecieController : ControllerBase
    {
        private readonly ILogger<SpecieController> _logger;
        private readonly ISpecieRepository _specieRepository;

        public SpecieController(ILogger<SpecieController> logger, ISpecieRepository specieRepository)
        {
            _logger = logger;
            _specieRepository = specieRepository;
        }

        /// <summary>
        /// /// Endpoint encargado de consultar la información de todos las Especies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var specie = await _specieRepository.GetAll();
            return Ok(specie);
        }

        /// <summary>
        /// Endpoint encargado de consultar la información de una Especie mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var specie = await _specieRepository.GetById(id);
            return Ok(specie);
        }

        /// <summary>
        /// Endpoint encargado de insertar la información de una Especie
        /// </summary>
        /// <param name="specie"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Specie specie)
        {
            _ = await _specieRepository.Create(specie);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de actualizar la información de una Especie
        /// </summary>
        /// <param name="specie"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Specie specie)
        {
            var currentSpecie = await _specieRepository.GetById(specie.Id);

            if (currentSpecie == null)
                return BadRequest("Specie to update not found");

            _ = await _specieRepository.Update(specie);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de eliminar la información de un Especie mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var currentSpecie = await _specieRepository.GetById(id);

            if (currentSpecie == null)
                return BadRequest("Specie to delete not found");

            _ = await _specieRepository.Delete(id);
            return Ok();
        }
    }
}