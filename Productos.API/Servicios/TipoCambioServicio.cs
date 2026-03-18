using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.TipoCambio;
using System.Net.Http;
using System.Text.Json;

namespace Servicios {
    public class TipoCambioServicio : ITipoCambioServicio {
        public readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public TipoCambioServicio (IConfiguracion configuracion, IHttpClientFactory httpClient) {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<TipoCambio> ObtenerTipoCambio (string fecha) {
            var endPoint = _configuracion.ObtenerMetodo("ApiEndPointsTipoCambio", "ObtenerTipoCambio");
            var servicioTipoCambio = _httpClient.CreateClient("ServicioTipoCambio");
            var respuesta = await servicioTipoCambio.GetAsync(string.Format(endPoint, fecha));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<TipoCambio>(resultado, opciones);
            return resultadoDeserializado;

        }
    }
}
