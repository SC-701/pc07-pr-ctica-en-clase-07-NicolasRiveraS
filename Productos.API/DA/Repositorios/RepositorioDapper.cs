using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DA.Repositorios {
    public class RepositorioDapper : IRepositorioDapper {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _conexionBaseDatos;

        public RepositorioDapper (IConfiguration configuration) {
            _configuration = configuration;
            _conexionBaseDatos = new SqlConnection(_configuration.GetConnectionString("BD"));
        }

        public SqlConnection ObtenerRepositorio () {
            return _conexionBaseDatos;
        }
    }
}
