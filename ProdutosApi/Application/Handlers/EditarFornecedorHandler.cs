using AutoMapper;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Commands;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Handlers
{
    public class EditarFornecedorHandler : IRequestHandler<EditarFornecedorCommand, FornecedorDTO>
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public EditarFornecedorHandler(IFornecedorRepository fornecedorRepository, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        public async Task<FornecedorDTO> Handle(EditarFornecedorCommand request, CancellationToken cancellationToken)
        {
            var fornecedor = _mapper.Map<EditarFornecedorCommand, Fornecedor>(request);
            var fornecedorOriginal = await _fornecedorRepository.GetFornecedorById(request.CodigoFornecedor);

            fornecedorOriginal.DescricaoFornecedor = fornecedor.DescricaoFornecedor;
            fornecedorOriginal.Produtos = fornecedor.Produtos;

            await _fornecedorRepository.UpdateFornecedor(fornecedorOriginal);
            return _mapper.Map<Fornecedor, FornecedorDTO>(fornecedorOriginal);
        }
    }
}
