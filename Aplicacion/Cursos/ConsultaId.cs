using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        // Clase que representa la consulta de cursos, devuelve una lista de tipo IRequest envolviendo una lista de clase Curso
        public class CursoUnico : IRequest<Curso> 
        {
            public int Id { get; set; }
        }

        // Clase que maneja la consulta de cursos
        public class Manejador : IRequestHandler<CursoUnico, Curso>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.Id);
                return curso;
            }
        }
    }
}