using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Persistencia
{
    public class CursosOnlineContext : DbContext
    {
        // Realiza migraciones de entidades y como puente de injeción de dependencias
        public CursosOnlineContext(DbContextOptions options) : base(options)
        {

        }

        // Entidades que se van a mapear
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CursoInstructor>().HasKey(ci => new { ci.InstructorId, ci.CursoId });
        }

        public DbSet<Curso> Curso { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<CursoInstructor> CursoInstructor { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Precio> Precio { get; set; }
    }
}