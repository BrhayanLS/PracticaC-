using GestionReservas.CasosDeUso;
using GestionReservas.Dtos;
using GestionReservas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController
    {
        public readonly ReservaRepository _reservaRepository;
        public readonly IUpdateReservaUseCase _updateReservaUseCase;

        public ReservaController(ReservaRepository reservaRepository, IUpdateReservaUseCase updateReservaUseCase)
        {
            _reservaRepository = reservaRepository;
            _updateReservaUseCase = updateReservaUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReserva(int id)
        {
            ReservaEntity result = await _reservaRepository.Get(id);
            return new OkObjectResult(result.ToDto());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservaDto))]
        public async Task<IActionResult> GetReservas()
        {
            var result = _reservaRepository.Reserva.Select(x => x.ToDto()).ToList();
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var result = await _reservaRepository.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReservaDto))]
        public async Task<IActionResult> CreateReserva(CreateReservaDto reserva)
        {
            ReservaEntity result = await _reservaRepository.Add(reserva);
            return new CreatedResult($"/api/reserva/{result.IdCliente}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateReserva(ReservaDto reserva)
        {
            ReservaDto? result = await _updateReservaUseCase.Execute(reserva);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }
    }
}
