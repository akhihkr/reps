-- =============================================
-- Author:Pravina 
-- Create date: 13 Feb 2017
-- Description:	Get Titles
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetTitles]
		@TitleID int = null
AS
	Select * FROM
	(
		SELECT [TitleID]
		  ,[Description]
		  ,[DateCreated]
		  ,[DateModified]
		  ,[Deleted]
		,ROW_NUMBER() OVER (ORDER BY [TitleID] ASC) AS num
				 FROM [dbo].[Title]
	  ) 
		As allUsers
	  WHERE 
	  (	@TitleID IS NULL OR [TitleID]= @TitleID	)
	  AND
	  [Deleted] =0