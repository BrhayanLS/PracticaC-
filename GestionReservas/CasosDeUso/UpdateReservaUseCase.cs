using GestionReservas.Dtos;
using GestionReservas.Repositories;

namespace GestionReservas.CasosDeUso
{
    public interface IUpdateReservaUseCase
    {
        Task<ReservaDto> Execute(ReservaDto reserva);
    }
    public class UpdateReservaUseCase : IUpdateReservaUseCase
    {
        public readonly ReservaRepository _reservaRepository;

        public UpdateReservaUseCase(ReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public async Task<ReservaDto> Execute(ReservaDto reserva)
        {
            var entity = await _reservaRepository.Get(reserva.IdReserva);
            if (entity == null)
            {
                return null;
            }

            entity.IdCliente = reserva.IdCliente;
            entity.FechaLlegada = reserva.FechaLlegada;
            entity.FechaSalida = reserva.FechaSalida;
            entity.IdHabitacion = reserva.IdHabitacion;

            await _reservaRepository.Actualizar(entity);
            return entity.ToDto();
        }
    }
}
