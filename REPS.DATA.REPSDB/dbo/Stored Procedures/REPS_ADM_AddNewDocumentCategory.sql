-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Document Category of admin 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_AddNewDocumentCategory]
			@categoryName varchar(max),
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @row int;

	INSERT INTO [dbo].[DocumentType]
				([Description]
				,[DateCreated])
			VALUES
				( @categoryName
				,GETDATE());

	SELECT @row = SCOPE_IDENTITY();
	SET @rowCount = @row;

	UPDATE [dbo].[DocumentType]
		SET [OrderBy] = @row
		WHERE [ID] = @row

END
