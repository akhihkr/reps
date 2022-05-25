-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Sizes Type
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetSizes]
		@SizeTypeID int = null,
		@Start  int = null,
		@End  int = null

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * FROM
	(
		SELECT   [SizeTypeID]
				,[Description]
				,[DateCreated]
				,[DateModified]
				,[Deleted]
 
		,ROW_NUMBER() OVER (ORDER BY [SizeTypeID] DESC) AS num
				 FROM [dbo].[SizeType]
	  ) 
		As allUsers
	  WHERE 
	  (	@SizeTypeID IS NULL OR [SizeTypeID]= @SizeTypeID )
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0	

END