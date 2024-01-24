using GestionReservas.Dtos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GestionReservas.Repositories
{
    public class ReservaRepository : DbContext
    {
        public ReservaRepository(DbContextOptions<ReservaRepository> options) : base(options) { }

        public DbSet<ReservaEntity> Reserva { get; set; }

        public async Task<ReservaEntity> Get(int id)
        {
            return await Reserva.FirstOrDefaultAsync(x => x.IdReserva == id);
        }

        public async Task<bool> Delete(int id)
        {
            ReservaEntity entity = await Get(id);
            if (entity != null)
            {
                Reserva.Remove(entity);
                SaveChanges();
                return true;
            }
            else { return false; }
        }

        public async Task<ReservaEntity> Add(CreateReservaDto reservaDto)
        {
            ReservaEntity entity = new ReservaEntity()
            {
                IdReserva = null,
                FechaLlegada = reservaDto.FechaLlegada,
                FechaSalida = reservaDto.FechaSalida,
                IdCliente = reservaDto.IdCliente,
                IdHabitacion = reservaDto.IdHabitacion
            };
            EntityEntry<ReservaEntity> response = await Reserva.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.IdReserva ?? throw new Exception ("No se ha podido guardar"));
        }

        public async Task<bool> Actualizar(ReservaEntity reservaEntity)
        {
            Reserva.Update(reservaEntity);
            SaveChangesAsync();
            return true;
        }
    }

    public class ReservaEntity
    {
        [Key]
        [Column("id_reserva")]
        public int? IdReserva { get; set; }
        [Column("fecha_llegada")]
        public DateTime FechaLlegada { get; set; }
        [Column("fecha_salida")]
        public DateTime FechaSalida { get; set; }
        [Column("id_cliente")]
        public int IdCliente { get; set; }
        [Column("id_habitacion")]
        public int IdHabitacion { get; set; }

        public ReservaDto ToDto()
        {
            return new ReservaDto()
            {
                FechaLlegada = FechaLlegada,
                FechaSalida = FechaSalida,
                IdCliente = IdCliente,
                IdHabitacion = IdHabitacion,
                IdReserva = IdReserva ?? throw new Exception("El id no puede ser null")
            };
        }
    }
}

