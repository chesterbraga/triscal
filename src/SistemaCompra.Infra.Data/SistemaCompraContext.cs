using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaCompra.Domain.Core;
using SistemaCompra.Domain.Core.Model;
using SistemaCompra.Domain.SolicitacaoCompraAggregate;
using SistemaCompra.Infra.Data.Produto;
using SistemaCompra.Infra.Data.SolicitacaoCompra;
using System;
using System.Collections.Generic;
using System.Reflection;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
using SolicitacaoAgg = SistemaCompra.Domain.SolicitacaoCompraAggregate;

namespace SistemaCompra.Infra.Data
{
    public class SistemaCompraContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public SistemaCompraContext(DbContextOptions options) : base(options) { }
        public DbSet<ProdutoAgg.Produto> Produtos { get; set; }
        public DbSet<SolicitacaoAgg.SolicitacaoCompra> SolicitacaoCompras { get; set; }
        //public DbSet<SolicitacaoAgg.Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*var seedProduto = new ProdutoAgg.Produto("Produto01", "Descricao01", "Madeira", 100);
            var seedMoney = new Money(100m);

            modelBuilder.Entity<ProdutoAgg.Produto>()
                .HasData(seedProduto);

            modelBuilder.Entity<ProdutoAgg.Produto>().OwnsOne(x => x.Preco)
                .HasData(new
                {
                    ProdutoId = seedProduto.Id,
                    seedMoney.Value
                });

            //seedProduto.AtualizarPreco(seedMoney.Value);
            var seedCompra = new SolicitacaoAgg.SolicitacaoCompra("UsuarioUsuario", "FornFornForn");
            seedCompra.Id = Guid.NewGuid();            

            var seedItem = new Item(seedProduto, 10);
            seedItem.Id = Guid.NewGuid();
            var seedSubTotal = new Money(1000m);            

            //seedCompra.AdicionarItem(seedProduto, 10);
            //seedCompra.RegistrarCompra(seedCompra.Itens);            

            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>()
                .HasData(seedCompra);

            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>()
                .HasData(seedItem);

            /*modelBuilder.Entity<Item>().OwnsOne(x => x.Subtotal)
                .HasData(new
                {
                    ItemId = seedItem.Id,
                    seedSubTotal.Value
                });

            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>().OwnsOne(x => x.TotalGeral)
                .HasData(new
                {
                    SolicitacaoCompraId = seedCompra.Id,
                    seedSubTotal.Value
                });

            var seedFornecedor = new NomeFornecedor("FornFornForn");
            var seedUsuario = new UsuarioSolicitante("UserUser");

            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>().OwnsOne(x => x.NomeFornecedor)
                .HasData(new
                {
                    SolicitacaoCompraId = seedCompra.Id,
                    seedFornecedor.Nome
                });

            modelBuilder.Entity<SolicitacaoAgg.SolicitacaoCompra>().OwnsOne(x => x.UsuarioSolicitante)
                .HasData(new
                {
                    SolicitacaoCompraId = seedCompra.Id,
                    seedUsuario.Nome
                });*/            

            modelBuilder.Ignore<Event>();

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new SolicitacaoCompraConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(loggerFactory)  
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=SistemaCompraDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}   
