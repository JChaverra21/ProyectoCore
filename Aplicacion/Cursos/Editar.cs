using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest
        {
            public int CursoId { get; set; }
            public string Titulo { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            // Instancia de CursosOnlineContext
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Encontrar el curso
                var curso = await _context.Curso.FindAsync(request.CursoId);
                if (curso == null)
                {
                    throw new Exception("No se encontró el curso");
                }

                // Actualizar el curso
                curso.Titulo = request.Titulo ?? curso.Titulo; // Si el titulo es nulo, se mantiene el mismo
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                // Guardar cambios
                var valor = await _context.SaveChangesAsync();  // Devuelve el numero de cambios realizados
                if (valor > 0) { return Unit.Value; }

                throw new Exception("No se guardaron los cambios en el curso");
            }
        }
    }
}