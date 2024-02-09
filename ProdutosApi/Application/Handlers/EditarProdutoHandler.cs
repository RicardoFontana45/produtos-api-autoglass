using AutoMapper;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Commands;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Handlers
{
    public class EditarProdutoHandler : IRequestHandler<EditarProdutoCommand, ProdutoDTO>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public EditarProdutoHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }


        public async Task<ProdutoDTO> Handle(EditarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _mapper.Map<EditarProdutoCommand, Produto>(request);
            var produtoOriginal = await _produtoRepository.GetProdutoById(request.CodigoProduto);

            produtoOriginal.CodigoFornecedor = produto.CodigoFornecedor;
            produtoOriginal.DataFabricacao = produto.DataFabricacao;
            produtoOriginal.DataValidade = produto.DataValidade;
            produtoOriginal.DescricaoProduto = produto.DescricaoProduto;

            await _produtoRepository.UpdateProduto(produtoOriginal);
            return _mapper.Map<Produto, ProdutoDTO>(produtoOriginal);
        }
    }
}
