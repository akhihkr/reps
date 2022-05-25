-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	update deal approve table 
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateDealApprove]
	@ParticipantID int,
	@DealID int,
	@Approve bit,
	@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 

				IF (@Approve = 1)
				
					BEGIN
		
						UPDATE [dbo].[Participant]
						SET [ApprovedDate] =  GetDate()
						WHERE [DealID] = @DealID AND
						[ParticipantID] = @ParticipantID
						AND [Deleted] = 0

						/* Update to finalised after all participants approved */
						DECLARE @countNonAppr int; SET @countNonAppr = 0;
						SELECT @countNonAppr=COUNT([ParticipantID]) FROM [dbo].[Participant] 
						WHERE [DealID] = @DealID AND [ParticipantRoleID] BETWEEN  30 AND 70 AND ([ApprovedDate] is null OR [ApprovedDate] ='');
						IF(@countNonAppr=0)
						BEGIN
							UPDATE [dbo].[Deal] SET [WorkflowTaskID] = 400 WHERE [DealID] = @DealID
						END
					END

				ELSE
					BEGIN
						BEGIN
						UPDATE [dbo].[Participant]
						SET [ApprovedDate] =  NULL
						WHERE [DealID] = @DealID AND
						[ParticipantID] = @ParticipantID
						AND [Deleted] = 0
					END
					END
				
END
SET @rowCount=  @@ROWCOUNT
GO