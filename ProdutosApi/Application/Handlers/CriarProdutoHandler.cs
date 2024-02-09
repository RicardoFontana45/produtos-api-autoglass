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
    public class CriarProdutoHandler : IRequestHandler<CriarProdutoCommand, ProdutoDTO>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public CriarProdutoHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }


        public async Task<ProdutoDTO> Handle(CriarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _mapper.Map<CriarProdutoCommand, Produto>(request);
            await _produtoRepository.AddProdutoAsync(produto);
            return _mapper.Map<Produto, ProdutoDTO>(produto);
        }
    }
}
