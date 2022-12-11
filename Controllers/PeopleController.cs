using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;
using StarWarsAPI.Core.Entities;

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
            var currentPeople = await _peopleRepository.GetById(people.results[0].Id);

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

        [HttpGet]
        [Route("insertall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de todas las Personas y las inserta en la base de datos", Description = "Endpoint encargado de consultar la información de todas las Personas y las inserta en la base de datos")]
        public async Task<IActionResult> InsertAll()
        {
            People peopleList = new People();
            HttpClient client = new HttpClient();
            string PeopleAPI = Constants.People;
            string responseBody = "";

            try
            {
                var response = await client.GetAsync(PeopleAPI);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody != null)
                    peopleList = JsonSerializer.Deserialize<People>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (peopleList.results.Length <= 0)
                return BadRequest("Number of people cannot be less than zero");

            _ = await _peopleRepository.InsertAll(peopleList.results);
            return Ok();
        }

        [HttpGet]
        [Route("updateall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Actualiza la información de todas las Personas", Description = "Endpoint encargado de actualizar la información de una lista de Personas")]
        public async Task<IActionResult> UpdateAll()
        {
            People peopleList = new People();
            HttpClient client = new HttpClient();
            string PeopleAPI = Constants.People;
            string responseBody = "";

            try
            {
                var response = await client.GetAsync(PeopleAPI);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody != null)
                    peopleList = JsonSerializer.Deserialize<People>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (peopleList.results.Length <= 0)
                return BadRequest("Number of people cannot be less than zero");

            _ = await _peopleRepository.UpdateAll(peopleList.results);
            return Ok();
        }
    }
}