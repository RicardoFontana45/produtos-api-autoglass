using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Helpers;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Queries
{
    public interface IFornecedorQueries
    {
        Task<PaginationHelper<FornecedorDTO>> GetAllFornecedores(
            int pagina = 1,
            int tamanhoPagina = 10,
            string descricaoFornecedor = null,
            string CNPJ = null);

        Task<FornecedorDTO> GetFornecedorById(int condigoFornecedor);
    }
}
