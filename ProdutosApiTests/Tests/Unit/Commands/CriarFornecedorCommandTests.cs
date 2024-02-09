using ProdutosApi.Application.Commands;
using Xunit;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace ProdutosApiTests.Tests.Unit.Commands
{
    public class CriarFornecedorCommandTests
    {
        private readonly Faker _faker;

        public CriarFornecedorCommandTests()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact]
        public void Validate_ValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrage
            var cmd = new CriarFornecedorCommand
            {
                CNPJ = _faker.Company.Cnpj(),
                DescricaoFornecedor = _faker.Company.CompanyName()
            };

            var validator = new CriarFornecedorCommand.CriarFornecedorCommandValidator();


            //act
            var result = validator.Validate(cmd);

            //assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validade_InvalidCommand_ShouldFailWhenThereIsAnyError()
        {
            //arrange
            var cmd = new CriarFornecedorCommand
            {
                CNPJ = "12345678912345",
            };

            var validator = new CriarFornecedorCommand.CriarFornecedorCommandValidator();


            //act
            var result = validator.TestValidate(cmd);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.CNPJ);
            result.ShouldHaveValidationErrorFor(x => x.DescricaoFornecedor);
        }
    }
}
