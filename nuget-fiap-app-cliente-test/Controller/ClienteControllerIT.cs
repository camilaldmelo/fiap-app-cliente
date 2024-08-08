using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace nuget_fiap_app_cliente_test.Controller
{
    public class ClienteControllerIT : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ClienteControllerIT(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

      
    }
}
