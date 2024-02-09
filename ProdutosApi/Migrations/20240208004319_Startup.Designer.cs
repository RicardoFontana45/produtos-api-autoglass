﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProdutosApi.Infrastructure.Data;

namespace ProdutosApi.Migrations
{
    [DbContext(typeof(ProdutosApiDbContext))]
    [Migration("20240208004319_Startup")]
    partial class Startup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("ProdutosApi.Domain.Entitites.Fornecedor", b =>
                {
                    b.Property<int>("CodigoFornecedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CNPJ")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescricaoFornecedor")
                        .HasColumnType("TEXT");

                    b.HasKey("CodigoFornecedor");

                    b.ToTable("Fornecedores");
                });

            modelBuilder.Entity("ProdutosApi.Domain.Entitites.Produto", b =>
                {
                    b.Property<int>("CodigoProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataFabricacao")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescricaoProduto")
                        .HasColumnType("TEXT");

                    b.Property<int?>("FornecedorCodigoFornecedor")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SituacaoProduto")
                        .HasColumnType("INTEGER");

                    b.HasKey("CodigoProduto");

                    b.HasIndex("FornecedorCodigoFornecedor");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("ProdutosApi.Domain.Entitites.Produto", b =>
                {
                    b.HasOne("ProdutosApi.Domain.Entitites.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorCodigoFornecedor");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("ProdutosApi.Domain.Entitites.Fornecedor", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
