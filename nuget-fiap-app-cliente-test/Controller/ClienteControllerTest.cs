using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Threading.Tasks;
using nuget_fiap_app_cliente.Controllers;
using System;
using nuget_fiap_app_cliente_common.Interfaces.Services;

namespace nuget_fiap_app_cliente_test.Controller
{
    public class ClienteControllerTest
    {
        private readonly Mock<IClienteService> _clienteServiceMock;
        private readonly ClienteController _controller;

        public ClienteControllerTest()
        {
            _clienteServiceMock = new Mock<IClienteService>();
            _controller = new ClienteController(_clienteServiceMock.Object);
        }


    }
}
