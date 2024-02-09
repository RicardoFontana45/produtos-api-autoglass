using ProdutosApi.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProdutosApi.Infrastructure.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllProdutos(
            bool? situacaoProduto = null,
            int? pagina = null,
            int? tamanhoPagina = null,
            DateTime? dataValidadeMin = null,
            DateTime? dataValidadeMax = null,
            DateTime? dataFabricacaoMin = null,
            DateTime? dataFabricacaoMax = null,
            int? codigoFornecedor = null);
        Task<Produto> GetProdutoById(int id);
        Task AddProdutoAsync(Produto produto);
        Task UpdateProduto(Produto produto);
        Task DeleteProdutoAsync(int id);
        Task<int> GetTotalProdutosAsync(bool ignorarSituacao = false);
    }
}
