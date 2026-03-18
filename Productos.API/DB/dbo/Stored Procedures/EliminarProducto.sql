-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE EliminarProducto
	-- Add the parameters for the stored procedure here
	@Id AS uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
	DELETE FROM Producto
	WHERE (Id = @Id)

	-- Retornar solo el Id
	SELECT @Id
	COMMIT TRANSACTION
END