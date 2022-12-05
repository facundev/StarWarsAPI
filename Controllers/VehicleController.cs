using StarWarsAPI.Core.Entities;
using StarWarsAPI.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de los Vehiculos", Description = "Endpoint encargado de consultar la información de todos los Vehiculos")]
        public async Task<IActionResult> Get()
        {
            var vehicle = await _vehicleRepository.GetAll();
            return Ok(vehicle);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Devuelve la información de los Vehiculos mediante Id", Description = "Endpoint encargado de consultar la información de un Vehiculo mediante su Id")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleRepository.GetById(id);
            return Ok(vehicle);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Inserta la información de un Vehiculo", Description = "Endpoint encargado de insertar la información de un Vehiculo")]
        public async Task<IActionResult> Create(Vehicle vehicle)
        {
            _ = await _vehicleRepository.Create(vehicle);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Actualiza la información de un Vehiculo", Description = "Endpoint encargado de actualizar la información de un Vehiculo")]
        public async Task<IActionResult> Update(Vehicle vehicle)
        {
            var currentVehicle = await _vehicleRepository.GetById(vehicle.Id);

            if (currentVehicle == null)
                return BadRequest("Vehicle to update not found");

            _ = await _vehicleRepository.Update(vehicle);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Elimina la información de un Vehiculo", Description = "Endpoint encargado de eliminar la información de un Vehiculo")]
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