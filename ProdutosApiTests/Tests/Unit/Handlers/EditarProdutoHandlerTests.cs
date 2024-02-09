using AutoMapper;
using Bogus;
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
    public class EditarProdutoHandlerTests
    {
        private readonly Faker _faker;
        public EditarProdutoHandlerTests()
        {
            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnProdutoDTO()
        {
            //arrange
            var mockRepository = new Mock<IProdutoRepository>();
            var mockMapper = new Mock<IMapper>();

            var produtoExistente = new Produto()
            {
                CodigoFornecedor = 1,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(1),
                DescricaoProduto = _faker.Commerce.ProductDescription(),
                CodigoProduto = 1,
                SituacaoProduto = true,
            };

            var cmd = new EditarProdutoCommand
            {
                CodigoProduto = 1,
                CodigoFornecedor = 5,
                DataFabricacao = DateTime.Now.AddDays(5),
                DataValidade = DateTime.Now.AddDays(6),
                DescricaoProduto = _faker.Commerce.ProductDescription()
            };

            var produtoAtualizado = new Produto()
            {
                CodigoProduto = produtoExistente.CodigoProduto,
                DataValidade = cmd.DataFabricacao,
                CodigoFornecedor = cmd.CodigoFornecedor,
                DataFabricacao = cmd.DataFabricacao,
                DescricaoProduto = cmd.DescricaoProduto,
                SituacaoProduto = produtoExistente.SituacaoProduto
            };


            var dto = new ProdutoDTO
            {
                CodigoProduto = 1,
                DataValidade = produtoAtualizado.DataFabricacao,
                CodigoFornecedor = produtoAtualizado.CodigoFornecedor,
                DataFabricacao = produtoAtualizado.DataFabricacao,
                DescricaoProduto = produtoAtualizado.DescricaoProduto,
                SituacaoProduto = produtoAtualizado.SituacaoProduto
            };

            mockMapper.Setup(mapper => mapper.Map<EditarProdutoCommand, Produto>(cmd)).Returns(produtoAtualizado);
            mockRepository.Setup(repo => repo.GetProdutoById(cmd.CodigoProduto)).ReturnsAsync(produtoExistente);
            mockMapper.Setup(mapper => mapper.Map<Produto, ProdutoDTO>(produtoExistente)).Returns(dto);
            mockRepository.Setup(repo => repo.UpdateProduto(produtoExistente));


            var handler = new EditarProdutoHandler(mockRepository.Object, mockMapper.Object);
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
