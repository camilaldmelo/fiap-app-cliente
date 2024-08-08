using nuget_fiap_app_cliente.Services;
using nuget_fiap_app_cliente_common.Interfaces.Services;
using nuget_fiap_app_cliente_common.Models;
using nuget_fiap_app_cliente_repository.DB;
using nuget_fiap_app_cliente_repository.Interface;
using nuget_fiap_app_cliente_repository;
using Xunit;
using FluentAssertions;

namespace nuget_fiap_app_cliente_test.Service
{
    public class ClienteServiceIT
    {
        private ClienteService _clienteService;
        private RepositoryDB _repositoryDB;
        private IUnitOfWork _unitOfWork;

        public ClienteServiceIT()
        {
            _repositoryDB = new RepositoryDB();
            _unitOfWork = new UnitOfWork(_repositoryDB);
            _clienteService = new ClienteService(new ClienteRepository(_repositoryDB), _unitOfWork);
        }

    }
}
