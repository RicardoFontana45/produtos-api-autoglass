using AutoMapper;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Helpers;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Queries
{
    public class ProdutoQueries : IProdutoQueries
    {
        public readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoQueries(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<PaginationHelper<ProdutoDTO>> GetAllProdutos(int pagina = 1, int tamanhoPagina = 10, DateTime? dataValidadeMin = null, DateTime? dataValidadeMax = null, DateTime? dataFabricacaoMin = null, DateTime? dataFabricacaoMax = null, int? codigoFornecedor = null)
        {
            var produtos = await _produtoRepository.GetAllProdutos(true, pagina, tamanhoPagina, dataValidadeMin, dataValidadeMax, dataFabricacaoMin, dataFabricacaoMax, codigoFornecedor);
            return new()
            {
                Items = produtos.Select(p => _mapper.Map<Produto, ProdutoDTO>(p)),
                TotalItems = await _produtoRepository.GetTotalProdutosAsync()
            };
        }


        public async Task<ProdutoDTO> GetProdutoById(int codigoProduto)
        {
            var produto = await _produtoRepository.GetProdutoById(codigoProduto);
            return _mapper.Map<Produto, ProdutoDTO>(produto);
        }
    }
}
