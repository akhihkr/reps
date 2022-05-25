-- =============================================
-- Author:Pravina Barosah
-- Create date: 18 Jan 2016
-- Description: get participant roles
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_GetParticipantRoles]
		@ParticipantRoleID int = null,
		@Start  int = null,
		@End  int = null
AS

	Select * FROM
	(
		SELECT [ParticipantRoleID]
			  ,[Description]
			  ,[Deleted]
 
		,ROW_NUMBER() OVER (ORDER BY [ParticipantRoleID] DESC) AS num
				 FROM [dbo].[ParticipantRole]
	  ) 
		As allUsers
	  WHERE 
	  (	@ParticipantRoleID IS NULL OR [ParticipantRoleID]= @ParticipantRoleID	)
		AND
	  (	@Start IS NULL OR num>= @Start	)
		AND
	  (	@End IS NULL OR num<= @End	)
	  AND
	  [Deleted] = 0	  

	  ORDER BY [Description] ASC
GO
