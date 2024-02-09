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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProdutosApiTests.Tests.Unit.Handlers
{
    public class CriarFornecedorHandlerTests
    {
        private readonly Faker _faker;
        public CriarFornecedorHandlerTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnFornecedorDTO()
        {
            //arrange
            var mockRepository = new Mock<IFornecedorRepository>();
            var mockMapper = new Mock<IMapper>();

            var cmd = new CriarFornecedorCommand
            {
                CNPJ = _faker.Company.Cnpj(),
                DescricaoFornecedor = _faker.Company.CompanyName()
            };

            var fornecedor = new Fornecedor()
            {
                CNPJ = cmd.CNPJ,
                CodigoFornecedor = 1,
                DescricaoFornecedor = cmd.DescricaoFornecedor,
                Produtos = Enumerable.Empty<Produto>().ToHashSet()
            };


            var dto = new FornecedorDTO
            {
                CNPJ = fornecedor.CNPJ,
                CodigoFornecedor = fornecedor.CodigoFornecedor,
                DescricaoFornecedor = fornecedor.DescricaoFornecedor,
                Produtos = Enumerable.Empty<ProdutoDTO>()
            };

            mockRepository.Setup(repo => repo.GetFornecedorByCNPJAsync(cmd.CNPJ)).ReturnsAsync((Fornecedor)null);
            mockMapper.Setup(mapper => mapper.Map<CriarFornecedorCommand, Fornecedor>(cmd)).Returns(fornecedor);
            mockRepository.Setup(repo => repo.AddFornecedorAsync(fornecedor));
            mockMapper.Setup(mapper => mapper.Map<Fornecedor, FornecedorDTO>(fornecedor)).Returns(dto);


            var handler = new CriarFornecedorHandler(mockRepository.Object, mockMapper.Object);
            var cancellationToken = new CancellationToken();

            //act
            var result = await handler.Handle(cmd, cancellationToken);


            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FornecedorDTO>();
            result.Should().BeEquivalentTo(dto);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldThronwErrorWhenCnpjExists()
        {
            //arrange
            var mockRepository = new Mock<IFornecedorRepository>();
            var mockMapper = new Mock<IMapper>();

            var cmd = new CriarFornecedorCommand
            {
                CNPJ = _faker.Company.Cnpj(),
                DescricaoFornecedor = _faker.Company.CompanyName()
            };

            var fornecedor = new Fornecedor()
            {
                CNPJ = cmd.CNPJ,
                CodigoFornecedor = 1,
                DescricaoFornecedor = cmd.DescricaoFornecedor,
                Produtos = Enumerable.Empty<Produto>().ToHashSet()
            };

            mockRepository.Setup(repo => repo.GetFornecedorByCNPJAsync(cmd.CNPJ)).ReturnsAsync(fornecedor);


            var handler = new CriarFornecedorHandler(mockRepository.Object, mockMapper.Object);
            var cancellationToken = new CancellationToken();

            //act & assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(cmd, cancellationToken));
        }
    }
}
