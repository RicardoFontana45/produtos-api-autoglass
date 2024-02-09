using ProdutosApi.Application.Commands;
using System;
using Xunit;
using FluentValidation.TestHelper;
using FluentAssertions;

namespace ProdutosApi.Tests.Unit.Commands
{
    public class CriarProdutoCommandTests
    {
        [Fact]
        public void Validate_ValidCommand_ShouldNotHaveAnyValiadtionError()
        {
            //arrange
            var cmd = new CriarProdutoCommand
            {
                CodigoFornecedor = 1,
                DataFabricacao = DateTime.Now.AddDays(-1),
                DataValidade = DateTime.Now,
                DescricaoProduto = "Produto Teste"
            };
            var validator = new CriarProdutoCommand.CriarProdutoCommandValidator();

            //act
            var result = validator.TestValidate(cmd);

            //assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_InvalidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var cmd = new CriarProdutoCommand
            {
                CodigoFornecedor = 1,
                DataFabricacao = DateTime.Now.AddDays(1),
                DataValidade = DateTime.Now
            };
            var validator = new CriarProdutoCommand.CriarProdutoCommandValidator();

            //act
            var result = validator.TestValidate(cmd);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.DataValidade);
            result.ShouldHaveValidationErrorFor(x => x.DescricaoProduto);
        }
    }
}
