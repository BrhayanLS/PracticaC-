using GestionReservas.Dtos;
using GestionReservas.Repositories;

namespace GestionReservas.CasosDeUso
{
    public interface IUpdateHabitacionUseCase
    {
        Task<HabitacionDto> Execute(HabitacionDto habitacion);
    }
    public class UpdateHabitacionUseCase : IUpdateHabitacionUseCase
    {
        public readonly HabitacionRepository _HabitacionRepository;

        public UpdateHabitacionUseCase(HabitacionRepository habitacionRepository)
        {
            _HabitacionRepository = habitacionRepository;
        }

        public async Task<HabitacionDto> Execute(HabitacionDto habitacion)
        {
            var entity = await _HabitacionRepository.Get(habitacion.idHabitacion);
            if (entity == null)
            {
                return null;
            }
            entity.NombreHabitacion = habitacion.nombreHabitacion;
            entity.IdTipoHabitacion = habitacion.idTipoHabitacion;
            entity.Capacidad = habitacion.capacidad;

            await _HabitacionRepository.Actualizar(entity);
            return entity.ToDto();
        }
    }
}
