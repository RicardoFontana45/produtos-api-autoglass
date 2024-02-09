using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutosApi.Domain.Entitites
{
    public class Fornecedor
    {
        public Fornecedor(string descricaoFornecedor, string cnpj)
        {
            DescricaoFornecedor = descricaoFornecedor;
            CNPJ = cnpj;
        }

        public Fornecedor()
        {
        }


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoFornecedor { get; set; }
        public string DescricaoFornecedor { get; set; }
        public string CNPJ { get; set; }
        public HashSet<Produto> Produtos { get; set; }
    }
}
