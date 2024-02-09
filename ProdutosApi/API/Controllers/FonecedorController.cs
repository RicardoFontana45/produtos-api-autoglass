using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Commands;
using ProdutosApi.Application.Helpers;
using ProdutosApi.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FonecedorController : ControllerBase
    {
        [HttpPost]
        public Task<FornecedorDTO> CriarFornecedorAsync([FromBody] CriarFornecedorCommand command, [FromServices] IMediator mediator) => mediator.Send(command);

        [HttpPut("id")]
        public Task<FornecedorDTO> EditarFOrnecedorAsync(int id, [FromBody] EditarFornecedorCommand command, [FromServices] IMediator mediator)
        {
            command.CodigoFornecedor = id;
            return mediator.Send(command);
        }

        [HttpGet("id")]
        public Task<FornecedorDTO> GetFornecedorByIdAsync(int id, [FromServices] IFornecedorQueries queries) => queries.GetFornecedorById(id);


        [HttpGet]
        public Task<PaginationHelper<FornecedorDTO>> GetFornecedoresAsync([FromServices] IFornecedorQueries queries,
                int pagina = 1,
                int tamanhoPagina = 10,
                string descricaoFornecedor = null,
                string CNPJ = null
            ) => queries.GetAllFornecedores(pagina, tamanhoPagina, descricaoFornecedor, CNPJ);
    }
}
