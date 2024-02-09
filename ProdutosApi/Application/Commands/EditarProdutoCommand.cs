using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Domain.Interfaces;
using System;

namespace ProdutosApi.Application.Commands
{
    public class EditarProdutoCommand : IRequest<ProdutoDTO>, ICommand
    {
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }

        public ValidationResult Validate() => new EditarProdutoCommandValidator().Validate(this);

        public class EditarProdutoCommandValidator : AbstractValidator<EditarProdutoCommand>
        {
            public EditarProdutoCommandValidator()
            {
                RuleFor(x => x.DescricaoProduto).NotEmpty();
                RuleFor(x => x.DataValidade).GreaterThanOrEqualTo(x => x.DataFabricacao);
            }
        }
    }
}
