using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aplicacion.Cursos.ManejadorError
{
    public class ManejadorExcepcion : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errores { get; }
        public ManejadorExcepcion(HttpStatusCode code, object errores = null)
        {
            Code = code;
            Errores = errores;
        }
    }
}