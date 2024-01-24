using GestionReservas.CasosDeUso;
using GestionReservas.Dtos;
using GestionReservas.Dtos.Create;
using GestionReservas.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionReservas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController
    {
        public readonly ClienteRepository _clienteRepository;
        public readonly IUpdateClienteUseCase _updateClienteUseCase;

        public ClienteController(ClienteRepository clienteRepository, IUpdateClienteUseCase updateClienteUseCase)
        {
            _clienteRepository = clienteRepository;
            _updateClienteUseCase = updateClienteUseCase;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCliente(int id)
        {
            ClienteEntity result = await _clienteRepository.Get(id);
            return new OkObjectResult(result.ToDto());
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ClienteDto))]
        public async Task<IActionResult> GetAllClientes()
        {
            var result = _clienteRepository.Cliente.Select(c => c.ToDto()).ToList();
            return new OkObjectResult(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _clienteRepository.Delete(id);
            return new OkObjectResult(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ClienteDto))]
        public async Task<IActionResult> CreateCliente(CreateClienteDto cliente)
        {
            ClienteEntity result = await _clienteRepository.Add(cliente);
            return new CreatedResult($"/api/cliente/{result.IdCliente}", null);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof (ClienteDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCliente(ClienteDto cliente)
        {
            ClienteDto? result = await _updateClienteUseCase.Execute(cliente);
            if(result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }
    }
}
