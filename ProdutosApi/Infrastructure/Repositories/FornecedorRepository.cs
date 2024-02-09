using Microsoft.EntityFrameworkCore;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Infrastructure.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly ProdutosApiDbContext _ctx;

        public FornecedorRepository(ProdutosApiDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddFornecedorAsync(Fornecedor fornecedor)
        {
            await _ctx.Fornecedores.AddAsync(fornecedor);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteFornecedorAsync(int id)
        {
            var fornecedor = await _ctx.Fornecedores.FirstOrDefaultAsync(x => x.CodigoFornecedor == id);
            _ctx.Fornecedores.Remove(fornecedor);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Fornecedor>> GetAllFornecedores(
            int? pagina = null,
            int? tamanhoPagina = null,
            string descricaoFornecedor = null,
            string CNPJ = null,
            bool includeProducts = false)
        {
            var query = _ctx.Fornecedores.AsQueryable();

            if (!string.IsNullOrEmpty(descricaoFornecedor))
            {
                query = query.Where(x => x.DescricaoFornecedor.ToLower().Contains(descricaoFornecedor.ToLower()));
            }

            if (!string.IsNullOrEmpty(CNPJ))
            {
                query = query.Where(x => x.CNPJ == CNPJ);
            }

            if (includeProducts)
            {
                query = query.Include(x => x.Produtos);
            }

            if (pagina.HasValue && tamanhoPagina.HasValue)
            {
                query = query.Skip((pagina.Value - 1) * tamanhoPagina.Value).Take(tamanhoPagina.Value);
            }

            return await query
                .ToListAsync();
        }

        public Task<Fornecedor> GetFornecedorById(int id) => _ctx.Fornecedores
            .Include(x => x.Produtos.Where(x => x.SituacaoProduto))
            .FirstOrDefaultAsync(x => x.CodigoFornecedor == id);

        public async Task UpdateFornecedor(Fornecedor fornecedor)
        {
            _ctx.Entry(fornecedor).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        public Task<Fornecedor> GetFornecedorByCNPJAsync(string cnpj) => _ctx.Fornecedores.FirstOrDefaultAsync(x => x.CNPJ == cnpj);

        public Task<int> GetTotalFornecedoresAsync() => _ctx.Fornecedores.CountAsync();
    }
}
