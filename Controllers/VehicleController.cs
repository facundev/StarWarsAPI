using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace StarWarsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(ILogger<VehicleController> logger, IVehicleRepository vehicleRepository)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
        }

        /// <summary>
        /// /// Endpoint encargado de consultar la información de todos los Vehiculos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var vehicle = await _vehicleRepository.GetAll();
            return Ok(vehicle);
        }

        /// <summary>
        /// Endpoint encargado de consultar la información de un Vehiculo mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleRepository.GetById(id);
            return Ok(vehicle);
        }

        /// <summary>
        /// Endpoint encargado de insertar la información de un Vehiculo
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            _ = await _vehicleRepository.Create(vehicle);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de actualizar la información de un Vehiculo
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Vehicle vehicle)
        {
            var currentVehicle = await _vehicleRepository.GetById(vehicle.Id);

            if (currentVehicle == null)
                return BadRequest("Vehicle to update not found");

            _ = await _vehicleRepository.Update(vehicle);
            return Ok();
        }

        /// <summary>
        /// Endpoint encargado de eliminar la información de un Vehiculo mediante su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var currentVehicle = await _vehicleRepository.GetById(id);

            if (currentVehicle == null)
                return BadRequest("Vehicle to delete not found");

            _ = await _vehicleRepository.Delete(id);
            return Ok();
        }
    }
}