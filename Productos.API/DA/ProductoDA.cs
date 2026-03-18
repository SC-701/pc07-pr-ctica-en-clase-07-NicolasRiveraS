using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA {
    public class ProductoDA : IProductoDA {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones
        public async Task<Guid> Agregar(ProductoRequest producto) {
            string query = @"AgregarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            }, commandType: CommandType.StoredProcedure);
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto) {
            await verificarProductoExiste(Id);
            string query = @"EditarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id,
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            }, commandType: CommandType.StoredProcedure);
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id) {
            await verificarProductoExiste(Id);
            string query = @"EliminarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new {
                Id
            }, commandType: CommandType.StoredProcedure);
            return resultadoConsulta;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener() {
            string query = @"SELECT
                                    p.[Id],
                                    p.[IdSubCategoria],
                                    sc.[IdCategoria] AS IdCategoria,
                                    p.[Nombre],
                                    p.[Descripcion],
                                    p.[Precio],
                                    p.[Stock],
                                    p.[CodigoBarras],
                                    sc.[Nombre] AS SubCategoria,
                                    c.[Nombre] AS Categoria
                              FROM [dbo].[Producto] p
                              INNER JOIN [dbo].[SubCategorias] sc ON sc.[Id] = p.[IdSubCategoria]
                              INNER JOIN [dbo].[Categorias] c ON c.[Id] = sc.[IdCategoria];";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ProductoDetalle?> Obtener(Guid Id) {
            string query = @"SELECT
                                    p.[Id],
                                    p.[IdSubCategoria],
                                    sc.[IdCategoria] AS IdCategoria,
                                    p.[Nombre],
                                    p.[Descripcion],
                                    p.[Precio],
                                    p.[Stock],
                                    p.[CodigoBarras],
                                    sc.[Nombre] AS SubCategoria,
                                    c.[Nombre] AS Categoria
                              FROM [dbo].[Producto] p
                              INNER JOIN [dbo].[SubCategorias] sc ON sc.[Id] = p.[IdSubCategoria]
                              INNER JOIN [dbo].[Categorias] c ON c.[Id] = sc.[IdCategoria]
                              WHERE p.[Id] = @Id;";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoDetalle>(query, new {
                Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategorias() {
            string query = @"SELECT [Id], [Nombre]
                              FROM [dbo].[Categorias]
                              ORDER BY [Nombre];";
            var resultadoConsulta = await _sqlConnection.QueryAsync<Categoria>(query);
            return resultadoConsulta;
        }

        public async Task<IEnumerable<SubCategoria>> ObtenerSubCategorias(Guid IdCategoria) {
            string query = @"SELECT [Id], [IdCategoria], [Nombre]
                              FROM [dbo].[SubCategorias]
                              WHERE [IdCategoria] = @IdCategoria
                              ORDER BY [Nombre];";
            var resultadoConsulta = await _sqlConnection.QueryAsync<SubCategoria>(query, new {
                IdCategoria
            });
            return resultadoConsulta;
        }
        #endregion

        #region Helpers
        private async Task verificarProductoExiste(Guid Id) {
            ProductoDetalle? resultadoConsultaProducto = await Obtener(Id);
            if (resultadoConsultaProducto == null) {
                throw new Exception("No se encontró el producto");
            }
        }
        #endregion
    }
}
