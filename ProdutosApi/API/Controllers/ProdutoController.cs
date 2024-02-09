using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Commands;
using ProdutosApi.Application.Helpers;
using ProdutosApi.Application.Queries;
using System;
using System.Threading.Tasks;

namespace ProdutosApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProdutoController : ControllerBase
    {
        [HttpPost]
        public Task<ProdutoDTO> CriarProdutoAsync([FromBody] CriarProdutoCommand command, [FromServices] IMediator mediator) => mediator.Send(command);


        [HttpPut("id")]
        public Task<ProdutoDTO> EditarProdutoAsync([FromBody] EditarProdutoCommand command, [FromServices] IMediator mediator, int id) {
            command.CodigoProduto = id;
            return mediator.Send(command);
        }

        [HttpDelete("id")]
        public Task<ProdutoDTO> DeleteProdutoAsync([FromServices] IMediator mediator, int id)
        {
            var command = new DeletarProdutoCommand() { CodigoProduto = id };
            return mediator.Send(command);
        }

        [HttpGet]
        public Task<PaginationHelper<ProdutoDTO>> GetAllProdutosAsync(
            [FromServices] IProdutoQueries query,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 10,
            [FromQuery] DateTime? dataValidadeMin = null,
            [FromQuery] DateTime? dataValidadeMax = null,
            [FromQuery] DateTime? dataFabricacaoMin = null,
            [FromQuery] DateTime? dataFabricacaoMax = null,
            [FromQuery] int? codigoFornecedor = null) => query.GetAllProdutos(pagina, tamanhoPagina, dataValidadeMin, dataValidadeMax, dataFabricacaoMin, dataFabricacaoMax, codigoFornecedor);

        [HttpGet("id")]
        public Task<ProdutoDTO> GetAsync(int id, [FromServices] IProdutoQueries query) => query.GetProdutoById(id);
    }
}
