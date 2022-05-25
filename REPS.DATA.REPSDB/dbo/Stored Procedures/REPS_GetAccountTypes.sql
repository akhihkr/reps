-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Account Type
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetAccountTypes]
		@AccountTypeID int = null,
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
		SELECT  [AccountTypeID]
				,[Description]
				,[DateCreated]
				,[DateModified]
				,[Deleted]
			
 
		,ROW_NUMBER() OVER (ORDER BY [AccountTypeID] DESC) AS num
				 FROM [dbo].[AccountType]
	  ) 
		As allUsers
	  WHERE 
	  (	@AccountTypeID IS NULL OR [AccountTypeID]= @AccountTypeID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] =0

END