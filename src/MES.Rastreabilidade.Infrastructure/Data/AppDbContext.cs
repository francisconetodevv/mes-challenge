using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES.Rastreabilidade.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MES.Rastreabilidade.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<EtapaDoProcesso> EtapasDoProcessos { get; set; }
        public DbSet<RegistroDeEtapa> RegistroDeEtapas { get; set; }

        // --- ADICIONE ESTE MÉTODO ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // É uma boa prática chamar a implementação base

            modelBuilder.Entity<ProductionOrder>(order =>
            {
                // Configura a relação: Uma Ordem de Produção (ProductionOrder) TEM UM Produto.
                order.HasOne(o => o.Produto)
                     // Um Produto TEM MUITAS Ordens de Produção.
                     .WithMany() // Se a entidade Produto tivesse uma lista de ordens, seria .WithMany(p => p.OrdensDeProducao)
                                 // A chave estrangeira na tabela de ProductionOrder é a propriedade ProdutoId.
                     .HasForeignKey(o => o.ProductId);
            });

            modelBuilder.Entity<Batch>(batch =>
            {
                batch.HasOne(b => b.ProductionOrder)
                     .WithMany()
                     .HasForeignKey(b => b.ProductionOrderId);

                batch.Property(b => b.QtyProduced)
                     .HasColumnType("decimal(18,4)");
            });

            modelBuilder.Entity<RegistroDeEtapa>(registro =>
            {
                registro.HasOne(r => r.Batch)
                        .WithMany()
                        .HasForeignKey(r => r.BatchId);

                registro.HasOne(r => r.EtapaDoProcesso)
                        .WithMany()
                        .HasForeignKey(r => r.EtapaDoProcessoId);
            });
        }
    }
}