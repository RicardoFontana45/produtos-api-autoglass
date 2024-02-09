using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Commands;
using ProdutosApi.Application.Handlers;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProdutosApiTests.Tests.Unit.Handlers
{
    public class EditarFornecedorHandlerTests
    {
        private readonly Faker _faker;
        public EditarFornecedorHandlerTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnFornecedorDTO()
        {
            //arrange
            var mockRepository = new Mock<IFornecedorRepository>();
            var mockMapper = new Mock<IMapper>();

            var fornecedorExistente = new Fornecedor()
            {
                CodigoFornecedor = 1,
                DescricaoFornecedor = _faker.Company.CompanyName(),
                CNPJ = _faker.Company.Cnpj()
            };

            var cmd = new EditarFornecedorCommand
            {
                CodigoFornecedor = 1,
                DescricaoFornecedor = _faker.Company.CompanyName(),
            };

            var fornecedorAtualizado = new Fornecedor()
            {
                CodigoFornecedor = fornecedorExistente.CodigoFornecedor,
                DescricaoFornecedor = cmd.DescricaoFornecedor,
                CNPJ = fornecedorExistente.CNPJ
            };


            var dto = new FornecedorDTO
            {
                CodigoFornecedor = 1,
                CNPJ = fornecedorAtualizado.CNPJ,
                DescricaoFornecedor = fornecedorAtualizado.DescricaoFornecedor,
            };

            mockMapper.Setup(mapper => mapper.Map<EditarFornecedorCommand, Fornecedor>(cmd)).Returns(fornecedorAtualizado);
            mockRepository.Setup(repo => repo.GetFornecedorById(cmd.CodigoFornecedor)).ReturnsAsync(fornecedorExistente);
            mockMapper.Setup(mapper => mapper.Map<Fornecedor, FornecedorDTO>(fornecedorExistente)).Returns(dto);
            mockRepository.Setup(repo => repo.UpdateFornecedor(fornecedorExistente));


            var handler = new EditarFornecedorHandler(mockRepository.Object, mockMapper.Object);
            var cancellationToken = new CancellationToken();

            //act
            var result = await handler.Handle(cmd, cancellationToken);

            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FornecedorDTO>();
            result.Should().BeEquivalentTo(dto);
        }
    }
}
