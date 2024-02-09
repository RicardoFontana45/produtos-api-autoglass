using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Domain.Interfaces;
using System;

namespace ProdutosApi.Application.Commands
{
    public class DeletarProdutoCommand : IRequest<ProdutoDTO>, ICommand
    {
        public int CodigoProduto { get; set; }

        public ValidationResult Validate() => new DeletarProdutoCommandValidator().Validate(this);

        private class DeletarProdutoCommandValidator : AbstractValidator<DeletarProdutoCommand>
        {
            public DeletarProdutoCommandValidator()
            {
            }
        }
    }
}
