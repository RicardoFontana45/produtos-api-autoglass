using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ProdutosApi.API.DTOs;
using ProdutosApi.Domain.Interfaces;

namespace ProdutosApi.Application.Commands
{
    public class CriarFornecedorCommand : IRequest<FornecedorDTO>, ICommand
    {
        public string DescricaoFornecedor { get; set; }
        public string CNPJ { get; set; }

        public ValidationResult Validate() => new CriarFornecedorCommandValidator().Validate(this);

        public class CriarFornecedorCommandValidator : AbstractValidator<CriarFornecedorCommand>
        {
            public CriarFornecedorCommandValidator()
            {
                RuleFor(x => x.DescricaoFornecedor).NotEmpty();
                RuleFor(x => x.CNPJ).NotEmpty().IsValidCNPJ();
            }
        }
    }
}
