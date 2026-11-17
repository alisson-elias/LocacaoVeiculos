
using LocacaoVeiculos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LocacaoVeiculos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Tabelas do banco de dados
        public DbSet<Veiculo> Veiculos { get; set; }

        public DbSet<CategoriaVeiculo> CategoriasVeiculo { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Categoria 1:N Veiculos
            modelBuilder.Entity<Veiculo>()
                .HasOne(v => v.CategoriaVeiculo)
                .WithMany(c => c.Veiculos)
                .HasForeignKey(v => v.CategoriaVeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Veiculo 1:N Locacoes
            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Veiculo)
                .WithMany(v => v.Locacoes)
                .HasForeignKey(l => l.VeiculoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cliente 1:N Locacoes
            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Cliente)
                .WithMany(c => c.Locacoes)
                .HasForeignKey(l => l.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // CPF único
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.CPF)
                .IsUnique();

            // Placa única
            modelBuilder.Entity<Veiculo>()
                .HasIndex(v => v.Placa)
                .IsUnique();

            // Precisão dos valores monetários
            modelBuilder.Entity<CategoriaVeiculo>()
                .Property(c => c.ValorDiaria)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Locacao>()
                .Property(l => l.ValorTotal)
                .HasPrecision(10, 2);
        }
    }
}
