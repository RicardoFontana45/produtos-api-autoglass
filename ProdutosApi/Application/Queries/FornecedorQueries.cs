using AutoMapper;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Helpers;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Queries
{
    public class FornecedorQueries : IFornecedorQueries
    {
        public readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorQueries(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<PaginationHelper<FornecedorDTO>> GetAllFornecedores(int pagina = 1, int tamanhoPagina = 10, string descricaoFornecedor = null, string CNPJ = null)
        {
            var fornecedores = await _fornecedorRepository.GetAllFornecedores(pagina, tamanhoPagina, descricaoFornecedor, CNPJ, includeProducts: false);
            return new()
            {
                Items = fornecedores.Select(f => _mapper.Map<Fornecedor, FornecedorDTO>(f)),
                TotalItems = await _fornecedorRepository.GetTotalFornecedoresAsync()
            };
        }


        public async Task<FornecedorDTO> GetFornecedorById(int codigoFornecedor)
        {
            var fornecedor = await _fornecedorRepository.GetFornecedorById(codigoFornecedor);
            return _mapper.Map<Fornecedor, FornecedorDTO>(fornecedor);
        }
    }
}
