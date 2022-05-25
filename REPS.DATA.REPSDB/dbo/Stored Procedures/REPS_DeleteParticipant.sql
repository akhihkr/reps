-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Remove Participant and set field Deleted true
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteParticipant]
			@ParticipantID int,
			@Deleted bit,
			@rowCount INT OUTPUT 
AS
SET NOCOUNT OFF -- enable rowcount return 
		UPDATE [dbo].[Participant]
			SET 				
				[Deleted]= @Deleted
		WHERE [ParticipantID] = @ParticipantID
SET @rowCount=  @@ROWCOUNT
GO
