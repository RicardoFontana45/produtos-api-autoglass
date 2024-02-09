using System.Collections.Generic;

namespace ProdutosApi.API.DTOs
{
    public class FornecedorDTO
    {
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJ { get; set; }
        public IEnumerable<ProdutoDTO> Produtos { get; set; }
    }
}
