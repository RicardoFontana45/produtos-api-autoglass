using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Queries;
using ProdutosApi.Domain.Entitites;
using ProdutosApi.Infrastructure.Repositories;
using Xunit;

namespace ProdutosApi.Tests.Unit.Queries
{
    public class ProdutoQueriesTests
    {
        private readonly Faker _faker;
        public ProdutoQueriesTests()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact]
        public async Task GetAllProdutoes_ReturnsPaginationHelper()
        {
            //arrange
            var produtos = new Faker<Produto>()
                .RuleFor(p => p.CodigoProduto, p => p.Random.Int())
                .RuleFor(p => p.DescricaoProduto, p => p.Commerce.ProductName())
                .RuleFor(p => p.DataFabricacao, p => DateTime.Now)
                .RuleFor(p => p.DataValidade, p => p.Date.Future(1, DateTime.Now))
                .RuleFor(p => p.SituacaoProduto, p => true)
                .RuleFor(p => p.CodigoFornecedor, p => p.Random.Int())
                .Generate(10);

            var mockRepository = new Mock<IProdutoRepository>();
            mockRepository.Setup(repo => repo.GetAllProdutos(true, It.IsAny<int>(), It.IsAny<int>(), null, null, null, null, null))
                .ReturnsAsync(produtos);

            mockRepository.Setup(repo => repo.GetTotalProdutosAsync(false)).ReturnsAsync(produtos.Count);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<ProdutoDTO>>(It.IsAny<IEnumerable<Produto>>()))
                .Returns((IEnumerable<Produto> produtos) =>
                    produtos.Select(p => new ProdutoDTO
                    {
                        CodigoProduto = p.CodigoProduto,
                        DescricaoProduto = p.DescricaoProduto,
                        CodigoFornecedor = p.CodigoFornecedor,
                        DataFabricacao = p.DataFabricacao,
                        DataValidade = p.DataValidade,
                        SituacaoProduto = p.SituacaoProduto
                    }));

            var queries = new ProdutoQueries(mockRepository.Object, mockMapper.Object);

            //act
            var result = await queries.GetAllProdutos();

            //assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Items.Count());
            Assert.Equal(10, result.TotalItems);
        }

        [Fact]
        public async Task GetProdutoById_ReturnsProdutoDTO()
        {
            //arrange
            var produto = new Faker<Produto>()
                .RuleFor(p => p.CodigoProduto, p => p.Random.Int())
                .RuleFor(p => p.DescricaoProduto, p => p.Commerce.ProductName())
                .RuleFor(p => p.DataFabricacao, p => DateTime.Now)
                .RuleFor(p => p.DataValidade, p => p.Date.Future(1, DateTime.Now))
                .RuleFor(p => p.SituacaoProduto, p => true)
                .RuleFor(p => p.CodigoFornecedor, p => p.Random.Int())
                .Generate();

            var mockRepository = new Mock<IProdutoRepository>();
            mockRepository.Setup(repo => repo.GetProdutoById(It.IsAny<int>()))
                .ReturnsAsync(produto);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<Produto, ProdutoDTO>(It.IsAny<Produto>()))
                .Returns((Produto p) => new ProdutoDTO
                {
                    CodigoProduto = p.CodigoProduto,
                    DescricaoProduto = p.DescricaoProduto,
                    CodigoFornecedor = p.CodigoFornecedor,
                    DataFabricacao = p.DataFabricacao,
                    DataValidade = p.DataValidade,
                    SituacaoProduto = p.SituacaoProduto
                });

            var queries = new ProdutoQueries(mockRepository.Object, mockMapper.Object);

            //act
            var result = await queries.GetProdutoById(produto.CodigoProduto);

            //assert
            Assert.NotNull(result);
            Assert.Equal(produto.CodigoProduto, result.CodigoProduto);
            Assert.Equal(produto.DescricaoProduto, result.DescricaoProduto);
            Assert.Equal(produto.CodigoFornecedor, result.CodigoFornecedor);
            Assert.Equal(produto.DataFabricacao, result.DataFabricacao);
            Assert.Equal(produto.DataValidade, result.DataValidade);
            Assert.Equal(produto.SituacaoProduto, result.SituacaoProduto);
        }
    }
}
