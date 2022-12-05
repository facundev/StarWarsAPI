using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace StarWarsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmController : ControllerBase
    {
        private readonly ILogger<FilmController> _logger;
        private readonly IFilmRepository _filmRepository;

        public FilmController(ILogger<FilmController> logger, IFilmRepository filmRepository)
        {
            _logger = logger;
            _filmRepository = filmRepository;
        }

        /// <summary>
        /// /// Endpoint encargado de consultar la información de todos los Films
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var film = await _filmRepository.GetAll();
            return Ok(film);
        }

        /// <summary>
        /// Endpoint encargado de consultar la información de un Film mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var film = await _filmRepository.GetById(id);
            return Ok(film);
        }

        /// <summary>
        /// Endpoint encargado de insertar la información de un Film
        /// </summary>
        /// <param name="film"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Film film)
        {
            _ = await _filmRepository.Create(film);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de actualizar la información de un Film
        /// </summary>
        /// <param name="film"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Film film)
        {
            var currentFilm = await _filmRepository.GetById(film.Id);

            if (currentFilm == null)
                return BadRequest("Film to update not found");

            _ = await _filmRepository.Update(film);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de eliminar la información de un Film mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var currentFilm = await _filmRepository.GetById(id);

            if (currentFilm == null)
                return BadRequest("Film to delete not found");

            _ = await _filmRepository.Delete(id);
            return Ok();
        }
    }
}