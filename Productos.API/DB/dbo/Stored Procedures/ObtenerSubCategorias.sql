CREATE PROCEDURE ObtenerSubCategorias
    @IdCategoria AS uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        [Id],
        [IdCategoria],
        [Nombre]
    FROM [dbo].[SubCategorias]
    WHERE [IdCategoria] = @IdCategoria
    ORDER BY [Nombre];
END
