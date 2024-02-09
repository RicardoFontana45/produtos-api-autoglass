using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Helpers;
using System;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Queries
{
    public interface IProdutoQueries
    {
        Task<PaginationHelper<ProdutoDTO>> GetAllProdutos(
            int pagina = 1,
            int tamanhoPagina = 10,
            DateTime? dataValidadeMin = null,
            DateTime? dataValidadeMax = null,
            DateTime? dataFabricacaoMin = null,
            DateTime? dataFabricacaoMax = null,
            int? codigoFornecedor = null);

        Task<ProdutoDTO> GetProdutoById(int codigoProduto);
    }
}
