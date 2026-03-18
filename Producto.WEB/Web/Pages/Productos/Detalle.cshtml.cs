using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos {
    public class DetalleModel : PageModel {
        private readonly IConfiguracion _configuracion;
        public ProductoDetalle producto { get; set; } = new();

        public DetalleModel(IConfiguracion configuracion) {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnGet(Guid? id) {
            if (id is null) {
                return NotFound();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, id));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.StatusCode == HttpStatusCode.NotFound) {
                return NotFound();
            }

            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK) {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<ProductoDetalle>(resultado, opciones) ?? new ProductoDetalle();
            }

            return Page();
        }
    }
}
