using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json;

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
            var currentPlanet = await _planetRepository.GetById(planet.results[0].Id);

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

        [HttpGet]
        [Route("insertall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertAll()
        {
            Planet planetsList = new Planet();
            HttpClient client = new HttpClient();
            string responseBody = "";

            try
            {
                var response = await client.GetAsync(Constants.Planets);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody != null)
                    planetsList = JsonSerializer.Deserialize<Planet>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (planetsList.results.Length <= 0)
                return BadRequest("Number of planets cannot be less than zero");

            _ = await _planetRepository.InsertAll(planetsList.results);
            return Ok();
        }

        [HttpGet]
        [Route("updateall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Actualiza la información de todos los Planetas", Description = "Endpoint encargado de actualizar la información de una lista de Planetas")]
        public async Task<IActionResult> UpdateAll()
        {
            Planet planetsList = new Planet();
            HttpClient client = new HttpClient();
            string responseBody = "";

            try
            {
                var response = await client.GetAsync(Constants.Planets);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                    responseBody = await response.Content.ReadAsStringAsync();

                if (responseBody != null)
                    planetsList = JsonSerializer.Deserialize<Planet>(responseBody);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (planetsList.results.Length <= 0)
                return BadRequest("Number of planets cannot be less than zero");

            _ = await _planetRepository.UpdateAll(planetsList.results);
            return Ok();
        }
    }
}