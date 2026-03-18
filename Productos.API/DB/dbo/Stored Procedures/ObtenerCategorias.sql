CREATE PROCEDURE ObtenerCategorias
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id],
        [Nombre]
    FROM [dbo].[Categorias]
    ORDER BY [Nombre];
END
