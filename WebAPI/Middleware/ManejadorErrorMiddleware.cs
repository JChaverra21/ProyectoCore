using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Aplicacion.Cursos.ManejadorError;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class ManejadorErrorMiddleware
    {
        public RequestDelegate _next;
        public ILogger<ManejadorErrorMiddleware> _logger;
        public ManejadorErrorMiddleware(RequestDelegate next, ILogger<ManejadorErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Metodo para manejar Request y Response
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await ManejadorExcepcionAsincrono(context, e, _logger);
            }
        }

        private async Task ManejadorExcepcionAsincrono(HttpContext context, Exception e, ILogger<ManejadorErrorMiddleware> logger)
        {
            object errores = null;
            switch (e)
            {
                case ManejadorExcepcion me :
                    logger.LogError(e, "Manejador Error");
                    errores = me.Errores;
                    context.Response.StatusCode = (int)me.Code;
                    break;
                case Exception ex :
                    logger.LogError(e, "Error de Servidor");
                    errores = string.IsNullOrWhiteSpace(ex.Message) ? "Error" : ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}