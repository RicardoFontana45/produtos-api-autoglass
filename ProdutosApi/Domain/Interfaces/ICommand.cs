using FluentValidation.Results;

namespace ProdutosApi.Domain.Interfaces
{
    public interface ICommand
    {
        public ValidationResult Validate();
    }
}
