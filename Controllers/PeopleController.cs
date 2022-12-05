using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// /// Endpoint encargado de consultar la información de todas las Personas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var people = await _peopleRepository.GetAll();
            return Ok(people);
        }

        /// <summary>
        /// Endpoint encargado de consultar la información de una Persona mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var people = await _peopleRepository.GetById(id);
            return Ok(people);
        }

        /// <summary>
        /// Endpoint encargado de insertar la información de una Persona
        /// </summary>
        /// <param name="people"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(People people)
        {
            _ = await _peopleRepository.Create(people);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de actualizar la información de una Persona
        /// </summary>
        /// <param name="people"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(People people)
        {
            var currentPeople = await _peopleRepository.GetById(people.Id);

            if (currentPeople == null)
                return BadRequest("People to update not found");

            _ = await _peopleRepository.Update(people);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de eliminar la información de una Persona mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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