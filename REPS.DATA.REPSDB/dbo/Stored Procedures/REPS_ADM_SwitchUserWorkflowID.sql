-- =============================================
-- Author:	Kenny
-- Create date: 25 Jan 2016
-- Description: Delete Workflow
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_SwitchUserWorkflowID]
			@oldWorkflowID int,
			@newWorkflowID int,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE
		[dbo].[User]
	SET
		[WorkflowID] = @newWorkflowID,
		[TokenId] = NULL
	WHERE
		[WorkflowID] = @oldWorkflowID
	AND
		[Deleted] = 0

	 SET @rowCount=  @@ROWCOUNT
END