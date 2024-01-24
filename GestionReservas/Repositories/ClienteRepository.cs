using GestionReservas.Dtos;
using GestionReservas.Dtos.Create;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionReservas.Repositories
{
    public class ClienteRepository : DbContext
    {
        public ClienteRepository(DbContextOptions<ClienteRepository> options) : base(options) { }

        public DbSet<ClienteEntity> Cliente { get; set; }

        public async Task<ClienteEntity> Get(int id)
        {
            return await Cliente.FirstOrDefaultAsync(x => x.IdCliente == id);
        }

        public async Task<bool> Delete(int id)
        {
            ClienteEntity entity = await Get(id);
            if (entity != null)
            {
                Cliente.Remove(entity);
                SaveChanges();
                return true;
            }
            else { return false; }
        }

        public async Task<ClienteEntity> Add (CreateClienteDto clienteDto)
        {
            ClienteEntity entity = new ClienteEntity()
            {
                IdCliente = null,
                Nombre = clienteDto.nombre,
                Direccion = clienteDto.direccion,
                Telefono = clienteDto.telefono,
                Correo = clienteDto.correo,
                Dni = clienteDto.dni
            };
            EntityEntry<ClienteEntity> response = await Cliente.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.IdCliente ?? throw new Exception("No se ha podido guardar"));
        }

        public async Task<bool> Actualizar(ClienteEntity clienteEntity)
        {
            Cliente.Update(clienteEntity);
            SaveChangesAsync();
            return true;
        }
    }

    public class ClienteEntity
    {
        [Key]
        [Column("id_cliente")]
        public int? IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Dni { get; set; }

        public ClienteDto ToDto()
        {
            return new ClienteDto()
            {
                nombre = Nombre,
                direccion = Direccion,
                telefono = Telefono,
                correo = Correo,
                dni = Dni,
                IdCliente = IdCliente ?? throw new Exception("El id no puede ser null")
            };
        }
    }
}
