-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update Participant
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateParticipant]
			@ParticipantTypeID int,
			@ParticipantRoleID int,
			@PersonID int,
			@OrganizationID int,
			@BankID int,
			@ParticipantID int,
			@rowCount INT OUTPUT 

AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
		UPDATE [dbo].[Participant]
			SET 			
				[ParticipantTypeID] =  IsNull(@ParticipantTypeID, [ParticipantTypeID]),
				[ParticipantRoleID] = IsNull(@ParticipantRoleID, [ParticipantRoleID]),
				[PersonID] = IsNull(@PersonID, [PersonID]),
				[OrganizationID] = IsNull(@OrganizationID, [OrganizationID]),
				[BankID] = IsNull(@BankID, [BankID]),
				[DateModified] = GETDATE()			
		WHERE [ParticipantID] = @ParticipantID
		AND [Deleted] != 0
END
SET @rowCount=  @@ROWCOUNT