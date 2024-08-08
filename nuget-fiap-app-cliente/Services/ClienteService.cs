using nuget_fiap_app_cliente_common.Interfaces.Repository;
using nuget_fiap_app_cliente_common.Interfaces.Services;
using nuget_fiap_app_cliente_common.Models;
using nuget_fiap_app_cliente_repository.Interface;

namespace nuget_fiap_app_cliente.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var result = await _clienteRepository.Delete(id);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<int> Create(Cliente cliente)
        {
            try
            {
                int clienteId = await _clienteRepository.Create(cliente);
                return clienteId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _clienteRepository.GetAll();
        }

        public async Task<Cliente> GetById(int id)
        {
            try
            {
                return await _clienteRepository.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Cliente cliente, int id)
        {

            try
            {
                var existingClient = await _clienteRepository.GetById(id);

                if (existingClient == null)
                {
                    return false; // Cliente não encontrado, portanto, não foi atualizado.
                }

                // Realize as validações necessárias no objeto 'cliente' e manipule exceções, se necessário.

                // Atualize as propriedades do cliente existente com base nos dados de 'cliente'.
                existingClient.Nome = cliente.Nome;
                existingClient.Cpf = cliente.Cpf;
                existingClient.Email = cliente.Email;
                existingClient.Endereço = cliente.Endereço;
                existingClient.Telefone = cliente.Telefone;

                bool updated = await _clienteRepository.Update(existingClient);
                return updated;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

