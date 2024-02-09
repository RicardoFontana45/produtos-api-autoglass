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
    public class DeletarProdutoHandler : IRequestHandler<DeletarProdutoCommand, ProdutoDTO>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public DeletarProdutoHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }


        public async Task<ProdutoDTO> Handle(DeletarProdutoCommand request, CancellationToken cancellationToken)
        {

            var produtoOriginal = await _produtoRepository.GetProdutoById(request.CodigoProduto);
            produtoOriginal.SituacaoProduto = false;

            await _produtoRepository.UpdateProduto(produtoOriginal);

            return _mapper.Map<Produto, ProdutoDTO>(produtoOriginal);
        }
    }
}
