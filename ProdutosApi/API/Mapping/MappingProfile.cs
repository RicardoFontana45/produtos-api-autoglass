using AutoMapper;
using ProdutosApi.API.DTOs;
using ProdutosApi.Application.Commands;
using ProdutosApi.Domain.Entitites;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Produto, ProdutoDTO>();
        CreateMap<ProdutoDTO, Produto>();
        CreateMap<CriarProdutoCommand, Produto>();
        CreateMap<EditarProdutoCommand, Produto>();

        CreateMap<Fornecedor, FornecedorDTO>();
        CreateMap<FornecedorDTO, Fornecedor>();
        CreateMap<CriarFornecedorCommand, Fornecedor>();
        CreateMap<EditarFornecedorCommand, Fornecedor>();
    }
}
