-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Stored Attachment by ID
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetCorrespondenceAttachmentByID]
			@documentID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
		[dbo].[Document].[CreatedByUserID],
		[dbo].[Document].[GeneratedDocFileName]
	FROM 
		[dbo].[Document]
	WHERE
		[dbo].[Document].ID = @documentID
	AND
		[dbo].[Document].[IsDeleted] = 0
END