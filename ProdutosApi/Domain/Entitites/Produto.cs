using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProdutosApi.Domain.Entitites
{
    public class Produto
    {
        public Produto(string descricaoProduto, DateTime dataFabricacao, DateTime dataValidade, Fornecedor fornecedor) : this()
        {
            DescricaoProduto = descricaoProduto;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            Fornecedor = fornecedor;
            SituacaoProduto = true;
        }

        public Produto()
        {
            SituacaoProduto = true;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public bool SituacaoProduto { get; set; }
        public int CodigoFornecedor { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public Fornecedor Fornecedor { get; set; }

    }
}
