-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Filename
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetFileNameFromTemplateDescription]
			@templateID int,
			@mimeTypeID varchar(50)

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @FileName nvarchar(MAX);

	SELECT @FileName = [TemplateDisplayName]
	FROM [dbo].[DocumentTemplate]
	WHERE [ID] = @templateID;

	SET @FileName = @FileName +  '.' + @mimeTypeID;

	SELECT @FileName;


END