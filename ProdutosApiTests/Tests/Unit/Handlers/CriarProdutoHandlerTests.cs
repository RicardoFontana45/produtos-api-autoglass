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
    public class CriarProdutoHandlerTests
    {
        private readonly Faker _faker;
        public CriarProdutoHandlerTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnProdutoDTO()
        {
            //arrange
            var mockRepository = new Mock<IProdutoRepository>();
            var mockMapper = new Mock<IMapper>();

            var cmd = new CriarProdutoCommand
            {
                CodigoFornecedor = 1,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(1),
                DescricaoProduto = _faker.Commerce.ProductDescription()
            };

            var produto = new Produto()
            {
                CodigoProduto = 1,
                DataValidade = cmd.DataFabricacao,
                CodigoFornecedor = cmd.CodigoFornecedor,
                DataFabricacao = cmd.DataFabricacao,
                DescricaoProduto = cmd.DescricaoProduto,
                SituacaoProduto = true
            };


            var dto = new ProdutoDTO
            {
                CodigoProduto = 1,
                DataValidade = produto.DataFabricacao,
                CodigoFornecedor = produto.CodigoFornecedor,
                DataFabricacao = produto.DataFabricacao,
                DescricaoProduto = produto.DescricaoProduto,
                SituacaoProduto = produto.SituacaoProduto
            };
           
            mockMapper.Setup(mapper => mapper.Map<CriarProdutoCommand, Produto>(cmd)).Returns(produto);
            mockRepository.Setup(repo => repo.AddProdutoAsync(produto));
            mockMapper.Setup(mapper => mapper.Map<Produto, ProdutoDTO>(produto)).Returns(dto);


            var handler = new CriarProdutoHandler(mockRepository.Object, mockMapper.Object);
            var cancellationToken = new CancellationToken();

            //act
            var result = await handler.Handle(cmd, cancellationToken);


            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ProdutoDTO>();
            result.Should().BeEquivalentTo(dto);
        }
    }
}
