using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Reglas {
    public interface IProductoReglas {
        Task AplicarTipoCambio (ProductoDetalle producto);
    }
}
