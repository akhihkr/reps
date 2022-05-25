-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Get Workflow Action IsMandatory value
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_GetWorkflowActionIsMandatory]
			@actionID int,
			@workflowTaskID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 1 [ID], [IsMandatory]
	  FROM [dbo].[WorkflowActionMap]
	  WHERE [WorkflowActionID] = @actionID
	  AND	[WorkflowTaskID] = @workflowTaskID
END