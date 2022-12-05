using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StarWarsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(ILogger<PeopleController> logger, IPeopleRepository peopleRepository)
        {
            _logger = logger;
            _peopleRepository = peopleRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de Personas", Description = "Endpoint encargado de consultar la información de todas las Personas")]
        public async Task<IActionResult> Get()
        {
            var people = await _peopleRepository.GetAll();
            return Ok(people);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de Personas mediante Id", Description = "Endpoint encargado de consultar la información de una Persona mediante su Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var people = await _peopleRepository.GetById(id);
            return Ok(people);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Inserta la información de una Persona", Description = "Endpoint encargado de insertar la información de una Persona")]
        public async Task<IActionResult> Create(People people)
        {
            _ = await _peopleRepository.Create(people);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Actualiza la información de una Persona", Description = "Endpoint encargado de actualizar la información de una Persona")]
        public async Task<IActionResult> Update(People people)
        {
            var currentPeople = await _peopleRepository.GetById(people.Id);

            if (currentPeople == null)
                return BadRequest("People to update not found");

            _ = await _peopleRepository.Update(people);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Elimina la información de una Persona", Description = "Endpoint encargado de eliminar la información de una Persona")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentPeople = await _peopleRepository.GetById(id);

            if (currentPeople == null)
                return BadRequest("People to delete not found");

            _ = await _peopleRepository.Delete(id);
            return Ok();
        }
    }
}