using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Domain.Interfaces;
using System;

namespace ProdutosApi.Application.Commands
{
    public class CriarProdutoCommand : IRequest<ProdutoDTO>, ICommand
    {
        public string DescricaoProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }

        public ValidationResult Validate() => new CriarProdutoCommandValidator().Validate(this);

        public class CriarProdutoCommandValidator : AbstractValidator<CriarProdutoCommand>
        {
            public CriarProdutoCommandValidator()
            {
                RuleFor(x => x.DescricaoProduto).NotEmpty();
                RuleFor(x => x.DataValidade).GreaterThanOrEqualTo(x => x.DataFabricacao);
            }
        }
    }
}
