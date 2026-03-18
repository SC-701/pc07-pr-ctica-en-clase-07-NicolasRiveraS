-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE AgregarProducto
	-- Add the parameters for the stored procedure here
	@Id AS uniqueidentifier,
	@IdSubCategoria AS uniqueidentifier,
	@Nombre AS varchar(max),
	@Descripcion AS varchar(max),
	@Precio AS decimal(18,2),
	@Stock AS int,
	@CodigoBarras AS varchar(max)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION;
	INSERT INTO [dbo].[Producto]
		   ([Id]
		   ,[IdSubCategoria]
		   ,[Nombre]
		   ,[Descripcion]
		   ,[Precio]
		   ,[Stock]
		   ,[CodigoBarras])
	VALUES
		   (@Id
		   ,@IdSubCategoria
		   ,@Nombre
		   ,@Descripcion
		   ,@Precio
		   ,@Stock
		   ,@CodigoBarras)

	-- Retornar solo el Id
	SELECT @Id
	COMMIT TRANSACTION;
END