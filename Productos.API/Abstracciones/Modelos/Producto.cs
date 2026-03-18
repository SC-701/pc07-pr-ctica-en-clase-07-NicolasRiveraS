using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos {
    public class ProductoBase {
        [Required(ErrorMessage = "La propiedad Nombre es requerida")]
        [StringLength(100, ErrorMessage = "El nombre debe contener entre 1 y 100 caracteres", MinimumLength = 1)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La propiedad Descripción es requerida")]
        [StringLength(1000, ErrorMessage = "La descripción debe contener entre 1 y 1000 caracteres", MinimumLength = 1)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La propiedad Precio es requerida")]
        [Range(1, double.MaxValue, ErrorMessage = "El valor del precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La propiedad Stock es requerida")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser cero o superior")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La propiedad Código de Barras es requerida")]
        [StringLength(13, ErrorMessage = "El código de barras debe tener mínimo de 8 caracteres", MinimumLength = 8)]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase {
        [Required(ErrorMessage = "La propiedad Id de Sub-Categoría es requerida")]
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase {
        public Guid Id { get; set; }
        public string SubCategoria { get; set; }
        public string Categoria { get; set; }
    }

    public class ProductoDetalle : ProductoResponse {
        public decimal PrecioUSD { get; set; }
    }
}