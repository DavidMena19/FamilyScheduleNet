using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace FamilySchedule.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)       
        {           
        }

        //Agregando los modelos a la base de datos
        public DbSet<EventoModel> Eventos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NotificacionesModel> Notificaciones { get; set; }
        public DbSet<TiposDeNotificaciones> TiposDeNotificaciones { get; set; }
        public DbSet<EventoUsuario> EventoUsuario { get; set; }
        public DbSet<usuarioGFModel> UsuarioGFModel { get; set; }
        public DbSet<GFamiliaresModel> GFamiliares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventoUsuario>()
                .HasKey(em => new { em.EventoId, em.UsuarioId });

            modelBuilder.Entity<EventoUsuario>()
                .HasOne(em => em.Evento)
                .WithMany(e => e.EventoUsuarios)
                .HasForeignKey(em => em.EventoId);

            modelBuilder.Entity<EventoUsuario>()
                .HasOne(em => em.Usuario)
                .WithMany(m => m.EventoUsuarios)
                .HasForeignKey(em => em.UsuarioId);
        }

    }
}
