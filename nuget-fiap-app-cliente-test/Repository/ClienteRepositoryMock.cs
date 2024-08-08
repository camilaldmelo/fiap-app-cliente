using nuget_fiap_app_cliente_common.Interfaces.Repository;
using nuget_fiap_app_cliente_common.Models;


namespace nuget_fiap_app_cliente_test.Repository
{
    public class ClienteRepositoryMock : IClienteRepository
    {
        private List<Cliente> dados;

        public ClienteRepositoryMock()
        {
            dados = new List<Cliente>() {
                new Cliente() { Id = 1, Nome = "teste1", Cpf="00000000000" },
                new Cliente() { Id = 2, Nome = "teste2" , Cpf="00000000001"}
            };
        }

        public async Task<int> Create(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }

            var nextId = dados.Any() ? dados.Max(c => c.Id) + 1 : 1;
            cliente.Id = nextId;
            dados.Add(cliente);

            return await Task.FromResult(nextId);
        }

        public async Task<bool> Delete(int id)
        {
            var cliente = dados.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return await Task.FromResult(false);
            }

            dados.Remove(cliente);
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await Task.FromResult(dados);
        }

        public async Task<Cliente> GetById(int id)
        {
            var cliente = dados.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(cliente);
        }

        public async Task<bool> Update(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente));
            }

            var existingClient = dados.FirstOrDefault(c => c.Id == cliente.Id);
            if (existingClient == null)
            {
                return await Task.FromResult(false);
            }

            // Atualiza os dados da cliente existente
            existingClient.Nome = cliente.Nome;
            existingClient.Cpf = cliente.Cpf;
            existingClient.Email = cliente.Email;
            existingClient.Endereço = cliente.Endereço;
            existingClient.Telefone = cliente.Telefone;


            return await Task.FromResult(true);
        }
    }
}
