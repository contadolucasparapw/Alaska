using Microsoft.EntityFrameworkCore;
using Alaska.API.Models;

namespace Alaska.API.Data;

public class AlaskaContext : DbContext
{
    public DbSet<Filme> Filmes => Set<Filme>();
    public DbSet<Sala> Salas => Set<Sala>();
    public DbSet<Sessao> Sessoes => Set<Sessao>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Assento> Assentos => Set<Assento>();
    public DbSet<ReservaAssento> ReservaAssentos => Set<ReservaAssento>();
    public DbSet<VendaRegistro> Vendas => Set<VendaRegistro>();
    public AlaskaContext(DbContextOptions<AlaskaContext> options)
    : base(options){
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // Relacionamento Filme e Sessao
    modelBuilder.Entity<Sessao>()
        .HasOne(s => s.Filme)
        .WithMany(f => f.Sessoes)
        .HasForeignKey(s => s.FilmeId)
        .OnDelete(DeleteBehavior.NoAction);

    // Relacionamento Sala e Sessao
    modelBuilder.Entity<Sessao>()
        .HasOne(s => s.Sala)
        .WithMany(s => s.Sessoes)
        .HasForeignKey(s => s.SalaId)
        .OnDelete(DeleteBehavior.NoAction);

    // Relacionamento Sessao e Reserva
    modelBuilder.Entity<Reserva>()
        .HasOne(r => r.Sessao)
        .WithMany(s => s.Reservas)
        .HasForeignKey(r => r.SessaoId)
        .OnDelete(DeleteBehavior.NoAction);
}
}
