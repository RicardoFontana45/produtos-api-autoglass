using ProdutosApi.Application.Commands;
using Xunit;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace ProdutosApiTests.Tests.Unit.Commands
{
    public class EditarFornecedorCommandTests
    {
        private readonly Faker _faker;

        public EditarFornecedorCommandTests()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact]
        public void Validate_ValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrage
            var cmd = new EditarFornecedorCommand
            {
                DescricaoFornecedor = _faker.Company.CompanyName()
            };

            var validator = new EditarFornecedorCommand.EditarFornecedorCommandValidator();


            //act
            var result = validator.Validate(cmd);

            //assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_InvalidCommand_ShouldFailWhenThereIsAnyError()
        {
            //arrange
            var cmd = new EditarFornecedorCommand
            {
            };

            var validator = new EditarFornecedorCommand.EditarFornecedorCommandValidator();


            //act
            var result = validator.TestValidate(cmd);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.DescricaoFornecedor);
        }
    }
}
