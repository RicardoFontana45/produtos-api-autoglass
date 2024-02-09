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
    public class FornecedorQueriesTests
    {
        private readonly Faker _faker;
        public FornecedorQueriesTests()
        {
            _faker = new Faker("pt_BR");
        }


        [Fact]
        public async Task GetAllFornecedores_ReturnsPaginationHelper()
        {
            //arrange
            var fornecedores = new Faker<Fornecedor>()
                .RuleFor(f => f.CodigoFornecedor, f => f.Random.Int())
                .RuleFor(f => f.DescricaoFornecedor, f => f.Company.CompanyName())
                .RuleFor(f => f.CNPJ, f => f.Company.Cnpj())
                .Generate(10);

            var mockRepository = new Mock<IFornecedorRepository>();
            mockRepository.Setup(repo => repo.GetAllFornecedores(It.IsAny<int>(), It.IsAny<int>(), null, null, true))
                .ReturnsAsync(fornecedores);

            mockRepository.Setup(repo => repo.GetTotalFornecedoresAsync()).ReturnsAsync(fornecedores.Count);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<IEnumerable<FornecedorDTO>>(It.IsAny<IEnumerable<Fornecedor>>()))
                .Returns((IEnumerable<Fornecedor> fornecedores) =>
                    fornecedores.Select(f => new FornecedorDTO { CodigoFornecedor = f.CodigoFornecedor, DescricaoFornecedor = f.DescricaoFornecedor }));

            var queries = new FornecedorQueries(mockRepository.Object, mockMapper.Object);

            //act
            var result = await queries.GetAllFornecedores();

            //assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Items.Count());
            Assert.Equal(10, result.TotalItems);
        }

        [Fact]
        public async Task GetFornecedorById_ReturnsFornecedorDTO()
        {
            //arrange
            var fornecedor = new Faker<Fornecedor>()
                .RuleFor(f => f.CodigoFornecedor, f => f.Random.Int())
                .RuleFor(f => f.DescricaoFornecedor, f => f.Company.CompanyName())
                .RuleFor(f => f.CNPJ, f => f.Company.Cnpj())
                .Generate();

            var mockRepository = new Mock<IFornecedorRepository>();
            mockRepository.Setup(repo => repo.GetFornecedorById(It.IsAny<int>()))
                .ReturnsAsync(fornecedor);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<Fornecedor, FornecedorDTO>(It.IsAny<Fornecedor>()))
                .Returns((Fornecedor f) => new FornecedorDTO { CodigoFornecedor = f.CodigoFornecedor, DescricaoFornecedor = f.DescricaoFornecedor, CNPJ = f.CNPJ });

            var queries = new FornecedorQueries(mockRepository.Object, mockMapper.Object);

            //act
            var result = await queries.GetFornecedorById(fornecedor.CodigoFornecedor);

            //assert
            Assert.NotNull(result);
            Assert.Equal(fornecedor.CodigoFornecedor, result.CodigoFornecedor);
            Assert.Equal(fornecedor.DescricaoFornecedor, result.DescricaoFornecedor);
            Assert.Equal(fornecedor.CNPJ, result.CNPJ);
        }
    }
}
