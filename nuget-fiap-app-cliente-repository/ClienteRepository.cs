using nuget_fiap_app_cliente_repository.DB;
using Dapper;
using nuget_fiap_app_cliente_common.Interfaces.Repository;
using nuget_fiap_app_cliente_common.Models;

namespace nuget_fiap_app_cliente_repository
{
    public class ClienteRepository : IClienteRepository
    {
        private RepositoryDB _session;
        private const string commandTextGet = @"
            SELECT 
                cli.id AS Id,
                cli.nome AS Nome,
                cli.email AS Email,
                cli.endereco AS Endereco,
                cli.telefone AS Telefone
            FROM 
                public.tbl_cliente cli";

        public ClienteRepository(RepositoryDB session)
        {
            _session = session;
        }

        public async Task<bool> Delete(int id)
        {
            string sql = "DELETE FROM public.tbl_cliente WHERE id = @id";

            int rowsAffected = await _session.Connection.ExecuteAsync(sql, new { id });
            return rowsAffected > 0;
        }


        public async Task<IEnumerable<Cliente>> GetAll()
        {

            var clientes = await _session.Connection.QueryAsync<Cliente>(commandTextGet);

            return clientes;
        }

        public async Task<int> Create(Cliente cliente)
        {
            string sql = "INSERT INTO public.tbl_cliente (nome, cpf, email, endereco, telefone) VALUES (@nome, @cpf, @email, @endereco, @telefone) RETURNING id";

            var parametros = new
            {
                nome = cliente.Nome,
                cpf = cliente.Cpf,
                email = cliente.Email,
                endereco = cliente.Endereço,
                telefone = cliente.Telefone
            };

            int clienteId = await _session.Connection.ExecuteScalarAsync<int>(sql, parametros);
            return clienteId;
        }


        public async Task<Cliente> GetById(int id)
        {
            string sql = commandTextGet + " WHERE pro.id = @id";

            var cliente = await _session.Connection.QueryAsync<Cliente>(sql, new { id });
         
            return cliente.FirstOrDefault(); // Retorna o primeiro cliente correspondente ao ID.
        }

        public async Task<bool> Update(Cliente cliente)
        {
            string sql = @"
                UPDATE public.tbl_cliente
                SET nome = @nome,
                    cpf = @cpf,
                    email = @email,
                    endereco = @endereco,
                    telefone = @telefone
                WHERE id = @id;";

            var parametros = new
            {
                id = cliente.Id,
                nome = cliente.Nome,
                cpf = cliente.Cpf,
                email = cliente.Email,
                endereco = cliente.Endereço,
                telefone = cliente.Telefone
            };

            int rowsAffected = await _session.Connection.ExecuteAsync(sql, parametros);
            return rowsAffected > 0; // Retorna true se alguma linha foi afetada (atualização bem-sucedida).
        }

    }
}
