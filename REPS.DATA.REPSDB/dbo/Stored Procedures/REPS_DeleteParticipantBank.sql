-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Delete Participant bank
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteParticipantBank]
			@ParticipantBankDetailID int,
			@Deleted bit,
			@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
    -- update statements for procedure here
		UPDATE [dbo].[ParticipantBankDetail]
			SET 				
				[Deleted]= @Deleted
		WHERE [ID] = @ParticipantBankDetailID
END
SET @rowCount=  @@ROWCOUNT
GO