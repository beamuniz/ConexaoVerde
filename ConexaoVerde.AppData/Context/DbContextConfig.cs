using ConexaoVerde.AppData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConexaoVerde.AppData.Context;

public class DbContextConfig : DbContext
{
    public DbContextConfig(DbContextOptions<DbContextConfig> options) : base(options)
    {
    }

    public DbContextConfig()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Endereco> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("Usuarios");
        modelBuilder.Entity<Cliente>().ToTable("Clientes");
        modelBuilder.Entity<Fornecedor>().ToTable("Fornecedores");

        modelBuilder.Entity<Avaliacao>()
            .HasOne(a => a.Cliente)
            .WithMany()
            .HasForeignKey(a => a.ClienteId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Avaliacao>()
            .HasOne(a => a.Fornecedor)
            .WithMany()
            .HasForeignKey(a => a.FornecedorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}