using GestionReservas.Dtos;
using GestionReservas.Dtos.Create;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionReservas.Repositories
{
    public class HabitacionRepository : DbContext
    {
        public HabitacionRepository(DbContextOptions<HabitacionRepository> options) : base(options) { }

        public DbSet<HabitacionEntity> Habitacion { get; set; }

        public async Task<HabitacionEntity> Get(int id)
        {
            //return await Habitacion.LastOrDefaultAsync(x => x.idHabitacion == id);
            return await Habitacion.FirstOrDefaultAsync(x => x.IdHabitacion == id);
        }

        public  async Task<bool> Delete(int id)
        {
            HabitacionEntity entity = await Get(id);
            if (entity != null)
            {
                Habitacion.Remove(entity);
                SaveChanges();
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<HabitacionEntity> Add(CreateHabitacionDto habitacionDto)
        {
            HabitacionEntity entity = new HabitacionEntity()
            {
                NombreHabitacion = habitacionDto.NombreHabitacion,
                Capacidad = habitacionDto.Capacidad,
                IdTipoHabitacion = habitacionDto.IdTipoHabitacion
            };
            EntityEntry<HabitacionEntity> response = await Habitacion.AddAsync(entity);
            await SaveChangesAsync();
            entity.IdHabitacion = response.Entity.IdHabitacion;
            return await Get(response.Entity.IdHabitacion ?? throw new Exception("No se ha podido guardar"));
        }
    }

    public class HabitacionEntity
    {
        [Key]
        [Column("id_habitacion")]
        public int? IdHabitacion { get; set; }
        [Column("nombre_habitacion")]
        public string NombreHabitacion { get; set; }
        public int Capacidad { get; set; }
        [Column("id_tipo_habitacion")]
        public int IdTipoHabitacion { get; set; }

        public HabitacionDto ToDto()
        {
            return new HabitacionDto()
            {
                nombreHabitacion = NombreHabitacion,
                capacidad = Capacidad,
                idTipoHabitacion = IdTipoHabitacion,
                idHabitacion = IdHabitacion ?? throw new Exception("El id no puede ser null")
            };
        }
    }
}
