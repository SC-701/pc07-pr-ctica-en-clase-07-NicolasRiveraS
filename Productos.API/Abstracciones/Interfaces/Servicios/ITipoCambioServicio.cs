using Abstracciones.Modelos.Servicios.TipoCambio;

namespace Abstracciones.Interfaces.Servicios {
    public interface ITipoCambioServicio {
        Task<TipoCambio> ObtenerTipoCambio(string fecha);
    }
}
