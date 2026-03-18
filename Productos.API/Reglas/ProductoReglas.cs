using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;

namespace Reglas {
    public class ProductoReglas : IProductoReglas {
        private readonly ITipoCambioServicio _tipoCambioServicio;
        private readonly IConfiguracion _configuracion;

        public ProductoReglas(ITipoCambioServicio tipoCambioServicio, IConfiguracion configuracion) {
            _tipoCambioServicio = tipoCambioServicio;
            _configuracion = configuracion;
        }

        public async Task AplicarTipoCambio (ProductoDetalle producto) {
            producto.PrecioUSD = await CalcularPrecioUSD(producto.Precio);
        }

        private static string ObtenerFechaActual () {
            return DateTime.UtcNow.ToString("yyyy/MM/dd");
        }

        private async Task<decimal> CalcularPrecioUSD(decimal precioColones) {
            string fecha = ObtenerFechaActual();
            var tipoCambio = await _tipoCambioServicio.ObtenerTipoCambio(fecha);
            double valorTipoCambio = tipoCambio.datos[0].indicadores[0].series[0].valorDatoPorPeriodo;
            return precioColones / (decimal) valorTipoCambio;
        }
    }
}
