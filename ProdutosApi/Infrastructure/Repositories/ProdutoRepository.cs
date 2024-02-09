using Microsoft.EntityFrameworkCore;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutosApiDbContext _ctx;

        public ProdutoRepository(ProdutosApiDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task AddProdutoAsync(Produto produto)
        {
            await _ctx.Produtos.AddAsync(produto);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteProdutoAsync(int id)
        {
            var produto = await _ctx.Produtos.FirstOrDefaultAsync(x => x.CodigoProduto == id);
            _ctx.Produtos.Remove(produto);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetAllProdutos(
            bool? situacaoProduto = null,
            int? pagina = null,
            int? tamanhoPagina = null,
            DateTime? dataValidadeMin = null,
            DateTime? dataValidadeMax = null,
            DateTime? dataFabricacaoMin = null,
            DateTime? dataFabricacaoMax = null,
            int? codigoFornecedor = null)
        {
            var query = _ctx.Produtos.AsQueryable();

            if (situacaoProduto.HasValue)
            {
                query = query.Where(x => x.SituacaoProduto == situacaoProduto);
            }

            if (codigoFornecedor.HasValue)
            {
                query = query.Where(x => x.CodigoFornecedor == codigoFornecedor);
            }

            if (dataValidadeMin.HasValue)
            {
                query = query.Where(x => x.DataValidade >= dataValidadeMin.Value);
            }

            if (dataValidadeMax.HasValue)
            {
                query = query.Where(x => x.DataValidade <= dataValidadeMax.Value);
            }

            if (dataFabricacaoMin.HasValue)
            {
                query = query.Where(x => x.DataFabricacao >= dataFabricacaoMin.Value);
            }

            if (dataFabricacaoMax.HasValue)
            {
                query = query.Where(x => x.DataFabricacao >= dataFabricacaoMax.Value);
            }

            if (pagina.HasValue && tamanhoPagina.HasValue)
            {
                query = query.Skip((pagina.Value - 1) * tamanhoPagina.Value).Take(tamanhoPagina.Value);
            }

            return await query
                .ToListAsync();
        }

        public Task<Produto> GetProdutoById(int id) => _ctx.Produtos.FirstOrDefaultAsync(x => x.CodigoProduto == id);

        public Task<int> GetTotalProdutosAsync(bool todosOsItems = false) => _ctx.Produtos
            .Where(x => x.SituacaoProduto || todosOsItems)
            .CountAsync();

        public async Task UpdateProduto(Produto produto)
        {
            _ctx.Entry(produto).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }
    }
}
