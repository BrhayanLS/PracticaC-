using GestionReservas.Dtos;
using GestionReservas.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace GestionReservas.CasosDeUso
{
    public interface IUpdateClienteUseCase
    {
        Task<ClienteDto> Execute(ClienteDto cliente);
    }
    public class UpdateClienteUseCase : IUpdateClienteUseCase
    {
        public readonly ClienteRepository _clienteRepository;

        public UpdateClienteUseCase(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDto> Execute(ClienteDto cliente)
        {
            var entity = await _clienteRepository.Get(cliente.IdCliente);
            if (entity == null)
            {
                return null;
            }
            entity.Nombre = cliente.nombre;
            entity.Correo = cliente.correo;
            entity.Telefono = cliente.telefono;
            entity.Direccion = cliente.direccion;

            await _clienteRepository.Actualizar(entity);
            return entity.ToDto();
        }
    }
}
