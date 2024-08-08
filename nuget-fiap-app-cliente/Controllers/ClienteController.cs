using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nuget_fiap_app_cliente_common.Interfaces.Services;
using nuget_fiap_app_cliente_common.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace nuget_fiap_app_cliente.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        /// <summary>
        /// Exclui um cliente com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser excluído.</param>
        /// <returns>
        /// 204 No Content se o cliente for excluído com sucesso.
        /// 404 Not Found se o cliente não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpDelete("{id}", Name = "ClientePorId")]
        [SwaggerOperation(Summary = "Exclusão de cliente por ID", Description = "Exclui um cliente com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "O cliente foi excluído com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "cliente não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool result = await _clienteService.Delete(id);

                if (!result)
                {
                    return NotFound(); // Retorna 404 Not Found se o cliente não for encontrado.
                }
                return NoContent(); // Retorna 204 No Content se o cliente for excluído com sucesso.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }



        /// <summary>
        /// Obtém uma lista de todos os Clientes.
        /// </summary>
        /// <returns>Uma lista de Clientes.</returns>
        [HttpGet(Name = "Clientes")]
        [SwaggerOperation(Summary = "Listagem de todos os Clientes", Description = "Recupera uma lista de todos os Clientes.")]
        [SwaggerResponse(StatusCodes.Status200OK, "A lista de Clientes foi recuperada com sucesso.", typeof(IEnumerable<Cliente>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum Cliente encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Clientes = await _clienteService.GetAll();
                return Ok(Clientes); // Retorna 200 OK com os Clientes recuperados.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }


        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="cliente">Os dados do novo cliente a ser criado.</param>
        /// <returns>
        /// 201 Created juntamente com o URL do novo recurso se a criação for bem-sucedida.
        /// 500 Internal Server Error em caso de erro inesperado no servidor.
        /// </returns>
        [HttpPost(Name = "Clientes")]
        [SwaggerOperation(Summary = "Criação de um novo cliente", Description = "Cria um novo cliente com base nos dados fornecidos.")]
        [SwaggerResponse(StatusCodes.Status201Created, "O cliente foi criado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            try
            {
                int clienteId = await _clienteService.Create(cliente);
                return CreatedAtRoute("ClientePorId", new { id = clienteId }, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Obtém um cliente com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente desejado.</param>
        /// <returns>
        /// 200 OK com o cliente recuperado.
        /// 404 Not Found se o cliente não for encontrado.
        /// 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpGet("{id}", Name = "ClientePorId")]
        [SwaggerOperation(Summary = "Obtenção de cliente por ID", Description = "Obtém um cliente com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O cliente foi recuperado com sucesso.", typeof(Cliente))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "cliente não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cliente = await _clienteService.GetById(id);

                if (cliente == null)
                {
                    return NotFound(); // Retorna 404 Not Found se o cliente não for encontrado.
                }
                return Ok(cliente); // Retorna 200 OK com o cliente recuperado.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

        /// <summary>
        /// Atualiza um cliente com base no ID especificado.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="cliente">Os dados do cliente a serem atualizados.</param>
        /// <returns>
        /// Retorna um código de status HTTP que indica o resultado da operação:
        /// - 200 OK se a atualização for bem-sucedida.
        /// - 404 Not Found se o cliente não for encontrado com o ID especificado.
        /// - 500 Internal Server Error em caso de erro interno do servidor.
        /// </returns>
        [HttpPut("{id}", Name = "ClientePorId")]
        [SwaggerOperation(Summary = "Atualização de um cliente por ID", Description = "Atualiza um cliente com base no ID especificado.")]
        [SwaggerResponse(StatusCodes.Status200OK, "O cliente foi atualizado com sucesso.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "cliente não encontrado.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno do servidor.")]
        public async Task<IActionResult> Put(int id, Cliente cliente)
        {
            try
            {

                bool updated = await _clienteService.Update(cliente, id);

                if (!updated)
                {
                    return NotFound(); // Retorna 404 Not Found se o cliente não for encontrado.
                }
                return Ok(); // Retorna 200 OK se a atualização for bem-sucedida.
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Retorna 500 Internal Server Error em caso de erro interno do servidor.
            }
        }

    }
}
