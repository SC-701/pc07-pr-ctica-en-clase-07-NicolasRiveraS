using Abstracciones.Modelos;

namespace Reglas {
    public class ProductoReglas {
        public ProductoRequest CrearRequest(ProductoRequest producto) {
            return new ProductoRequest {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras,
                IdSubCategoria = producto.IdSubCategoria
            };
        }

        public ProductoRequest CrearRequest(ProductoDetalle producto) {
            return new ProductoRequest {
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras,
                IdSubCategoria = producto.IdSubCategoria
            };
        }
    }
}
