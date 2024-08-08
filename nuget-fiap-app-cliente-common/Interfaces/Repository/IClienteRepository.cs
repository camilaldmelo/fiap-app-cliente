using nuget_fiap_app_cliente_common.Models;

namespace nuget_fiap_app_cliente_common.Interfaces.Repository
{
    public interface IClienteRepository
    {
        public Task<IEnumerable<Cliente>> GetAll();
        public Task<int> Create(Cliente cliente);
        public Task<bool> Delete(int id);
        public Task<Cliente> GetById(int id);
        public Task<bool> Update(Cliente cliente);
    }
}
