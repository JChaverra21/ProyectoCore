using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Cursos.ManejadorError;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;

            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                // Encontrar el curso
                var curso = await _context.Curso.FindAsync(request.Id);

                if(curso == null)
                {
                    //throw new Exception("No se encontró el curso");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el curso" });
                }

                _context.Remove(curso);
                var valor = await _context.SaveChangesAsync();

                if (valor > 0) { return Unit.Value; }

                //throw new Exception("No se pudo eliminar el curso");
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se pudo eliminar el curso" });
            }
        }
    }
}