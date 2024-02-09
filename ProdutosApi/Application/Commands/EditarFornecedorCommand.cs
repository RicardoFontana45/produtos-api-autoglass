using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProdutosApi.Application.Commands
{
    public class EditarFornecedorCommand : IRequest<FornecedorDTO>, ICommand
    {
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }

        public ValidationResult Validate() => new EditarFornecedorCommandValidator().Validate(this);

        public class EditarFornecedorCommandValidator : AbstractValidator<EditarFornecedorCommand>
        {
            public EditarFornecedorCommandValidator()
            {
                RuleFor(x => x.DescricaoFornecedor).NotEmpty();
            }
        }
    }
}
