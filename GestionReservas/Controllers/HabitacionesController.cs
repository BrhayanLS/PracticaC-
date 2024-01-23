using GestionReservas.Dtos;
using GestionReservas.Dtos.Create;
using GestionReservas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservas.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class HabitacionesController : Controller
    {
        public readonly HabitacionRepository _habitacionRepository;

        public HabitacionesController(HabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HabitacionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHabitaciones(int id)
        {
            HabitacionEntity result = await _habitacionRepository.Get(id);
            return new OkObjectResult(result.ToDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetHabitaciones()
        {
            var result = _habitacionRepository.Habitacion.Select(c=>c.ToDto()).ToList();
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteHabitacion(int id)
        {
            var result = await _habitacionRepository.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(HabitacionDto))]
        public async Task<IActionResult> CreateHabitacion(CreateHabitacionDto habitacion)
        {
            HabitacionEntity result = await _habitacionRepository.Add(habitacion);
            return new CreatedResult($"/api/habitacion/{result.IdHabitacion}", null);
        }
    }
}