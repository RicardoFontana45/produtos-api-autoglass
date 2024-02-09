using System;

namespace ProdutosApi.API.DTOs
{
    public class ProdutoDTO
    {
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public bool SituacaoProduto { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int CodigoFornecedor { get; set; }
    }
}
