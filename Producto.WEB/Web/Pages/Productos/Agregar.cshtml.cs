using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Reglas;
using System.Net;
using System.Text.Json;

namespace Web.Pages.Productos {
    public class AgregarModel : PageModel {
        private readonly IConfiguracion _configuracion;
        private readonly ProductoReglas _productoReglas;

        [BindProperty]
        public ProductoRequest producto { get; set; } = new();

        [BindProperty]
        public List<SelectListItem> categorias { get; set; } = new();

        [BindProperty]
        public List<SelectListItem> subCategorias { get; set; } = new();

        [BindProperty]
        public Guid categoriaSeleccionada { get; set; }

        public AgregarModel(IConfiguracion configuracion, ProductoReglas productoReglas) {
            _configuracion = configuracion;
            _productoReglas = productoReglas;
        }

        public async Task<ActionResult> OnGet() {
            await ObtenerCategoriasAsync();
            return Page();
        }

        public async Task<ActionResult> OnPost() {
            if (!ModelState.IsValid) {
                await ObtenerCategoriasAsync();
                if (categoriaSeleccionada != Guid.Empty) {
                    await ObtenerSubCategoriasSelectAsync(categoriaSeleccionada, producto.IdSubCategoria);
                }
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarProducto");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync(endpoint, _productoReglas.CrearRequest(producto));

            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid categoriaId) {
            var resultado = await ObtenerSubCategoriasAsync(categoriaId);
            return new JsonResult(resultado);
        }

        private async Task ObtenerCategoriasAsync() {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK) {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var categoriasResultado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones) ?? new List<Categoria>();
                categorias = categoriasResultado.Select(a => new SelectListItem {
                    Value = a.Id.ToString(),
                    Text = a.Nombre,
                    Selected = a.Id == categoriaSeleccionada
                }).ToList();
            }
        }

        private async Task ObtenerSubCategoriasSelectAsync(Guid categoriaId, Guid subCategoriaSeleccionada = default) {
            var resultado = await ObtenerSubCategoriasAsync(categoriaId);
            subCategorias = resultado.Select(a => new SelectListItem {
                Value = a.Id.ToString(),
                Text = a.Nombre,
                Selected = a.Id == subCategoriaSeleccionada
            }).ToList();
        }

        private async Task<List<SubCategoria>> ObtenerSubCategoriasAsync(Guid categoriaId) {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));

            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK) {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones) ?? new List<SubCategoria>();
            }

            return new List<SubCategoria>();
        }
    }
}
