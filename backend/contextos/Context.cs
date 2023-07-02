using Microsoft.EntityFrameworkCore;

namespace contextos
{
    public class Context : DbContext
    {
        public DbSet<CarroModel> Carros { get; set; }
        public DbSet<ReservaModel> Reservas { get; set; }
        //public DbSet<Usuario> Usuario { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração do relacionamento entre Carro e Reserva
            modelBuilder.Entity<CarroModel>()
                .HasMany(c => c.Reservas)
                .WithOne(r => r.Carro)
                .HasForeignKey(r => r.IdCarro);

            // Configuração do relacionamento entre Usuario e Reserva
            //modelBuilder.Entity<Usuario>()
            //    .HasMany(u => u.Reservas)
            //    .WithOne(r => r.Usuario)
            //    .HasForeignKey(r => r.UsuarioId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
