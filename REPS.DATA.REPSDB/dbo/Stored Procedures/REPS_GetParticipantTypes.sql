-- =============================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Participant types
-- ==============================================
CREATE PROCEDURE [dbo].[REPS_GetParticipantTypes]
		@ParticipantTypeID int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
		SELECT [ParticipantTypeID]
			  ,[Description]
			  ,[DateCreated]
			  ,[DateModified]
			  ,[Deleted]
 
		,ROW_NUMBER() OVER (ORDER BY [ParticipantTypeID] DESC) AS num
				 FROM [dbo].[ParticipantType]
	  ) 
		As allUsers
	  WHERE 
	  (	@ParticipantTypeID IS NULL OR [ParticipantTypeID]= @ParticipantTypeID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] = 0	  
GO  