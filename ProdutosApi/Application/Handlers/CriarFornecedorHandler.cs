using AutoMapper;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Commands;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Handlers
{
    public class CriarFornecedorHandler : IRequestHandler<CriarFornecedorCommand, FornecedorDTO>
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public CriarFornecedorHandler(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }


        public async Task<FornecedorDTO> Handle(CriarFornecedorCommand request, CancellationToken cancellationToken)
        {
            var cnpjExiste = await _fornecedorRepository.GetFornecedorByCNPJAsync(request.CNPJ);

            if (cnpjExiste != null)
            {
                throw new Exception(message: "O CNPJ já existe!");
            }

            var fornecedor = _mapper.Map<CriarFornecedorCommand, Fornecedor>(request);
            await _fornecedorRepository.AddFornecedorAsync(fornecedor);
            return _mapper.Map<Fornecedor, FornecedorDTO>(fornecedor);
        }
    }
}
