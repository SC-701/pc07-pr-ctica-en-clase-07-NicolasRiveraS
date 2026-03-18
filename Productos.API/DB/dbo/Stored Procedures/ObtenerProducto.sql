-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ObtenerProducto
	-- Add the parameters for the stored procedure here
	@Id AS uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;

	SELECT
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
	WHERE p.[Id] = @Id;
END