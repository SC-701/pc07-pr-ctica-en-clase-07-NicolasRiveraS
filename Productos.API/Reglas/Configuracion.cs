using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos.Servicios;
using Microsoft.Extensions.Configuration;

namespace Reglas {
    public class Configuracion : IConfiguracion {
        /* Este es el que nos deja consumir el AppSettings.json
           No es el que creamos */
        private IConfiguration _configuration;

        public Configuracion (IConfiguration configuration) {
            _configuration = configuration;
        }

        public string ObtenerMetodo (string seccion, string nombre) {
            string? UrlBase = ObtenerUrlBase(seccion);
            var Metodo = _configuration.GetSection(seccion).Get<ApiEndPoint>().Metodos.Where(m => m.Nombre == nombre).FirstOrDefault().Valor;
            return $"{UrlBase}/{Metodo}";
        }

        private string? ObtenerUrlBase (string seccion) {
            return _configuration.GetSection(seccion).Get<ApiEndPoint>().UrlBase;
        }

        public string ObtenerValor (string llave) {
            return _configuration.GetSection(llave).Value;
        }
    }
}
