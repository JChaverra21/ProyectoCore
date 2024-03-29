using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        // Clase que representa la consulta de cursos, devuelve una lista de tipo IRequest envolviendo una lista de clase Curso
        public class ListaCursos : IRequest<List<Curso>> {}

        public class Manejador : IRequestHandler<ListaCursos, List<Curso>>
        {
            private readonly CursosOnlineContext _context;
            // Metodo que maneja la consulta de cursos
            public Manejador(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<List<Curso>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                // Devuelve una lista de cursos
                var cursos = await _context.Curso.ToListAsync();
                return cursos;
            }
        }
    }
}