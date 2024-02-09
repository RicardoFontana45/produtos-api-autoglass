using Microsoft.EntityFrameworkCore;
using ProdutosApi.Domain.Entitites;

namespace ProdutosApi.Infrastructure.Data
{
    public class ProdutosApiDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        public ProdutosApiDbContext(DbContextOptions<ProdutosApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasKey(p => p.CodigoProduto);
            modelBuilder.Entity<Produto>().HasOne(p => p.Fornecedor).WithMany(x => x.Produtos).HasForeignKey(x => x.CodigoFornecedor);
            modelBuilder.Entity<Fornecedor>().HasKey(p => p.CodigoFornecedor);
        }

    }
}
