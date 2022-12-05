using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de Especies", Description = "Endpoint encargado de consultar la información de todas las Especies")]
        public async Task<IActionResult> Get()
        {
            var specie = await _specieRepository.GetAll();
            return Ok(specie);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de Especies mediante Id", Description = "Endpoint encargado de consultar la información de una Especie mediante su Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var specie = await _specieRepository.GetById(id);
            return Ok(specie);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Inserta la información de una Especie", Description = "Endpoint encargado de insertar la información de una Especie")]
        public async Task<IActionResult> Create(Specie specie)
        {
            _ = await _specieRepository.Create(specie);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Actualiza la información de una Especie", Description = "Endpoint encargado de actualizar la información de una Especie")]
        public async Task<IActionResult> Update(Specie specie)
        {
            var currentSpecie = await _specieRepository.GetById(specie.Id);

            if (currentSpecie == null)
                return BadRequest("Specie to update not found");

            _ = await _specieRepository.Update(specie);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Elimina la información de una Especie", Description = "Endpoint encargado de eliminar la información de una Especie")]
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