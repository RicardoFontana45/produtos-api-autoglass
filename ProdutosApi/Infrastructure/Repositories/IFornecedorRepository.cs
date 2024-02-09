using ProdutosApi.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Infrastructure.Repositories
{
    public interface IFornecedorRepository
    {
        Task<IEnumerable<Fornecedor>> GetAllFornecedores(
            int? pagina = null,
            int? tamanhoPagina = null,
            string descricaoFornecedor = null,
            string CNPJ = null,
            bool includeProducts = false);
        Task<Fornecedor> GetFornecedorById(int id);
        Task AddFornecedorAsync(Fornecedor fornecedor);
        Task UpdateFornecedor(Fornecedor fornecedor);
        Task DeleteFornecedorAsync(int id);
        Task<Fornecedor> GetFornecedorByCNPJAsync(string cnpj);
        Task<int> GetTotalFornecedoresAsync();
    }
}
