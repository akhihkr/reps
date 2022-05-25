-- =============================================
-- Author:	Hemraj B
-- Create date: 08-Aug-2017
-- Description:	Get WorkFlowAction name
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetWorkFlowAction]
	@WorkFlowStepID int
AS
BEGIN
	Select DISTINCT WA.DisplayName from [DocumentWorkflow] DW  
	INNER JOIN 
	(
		SELECT ID, DisplayName 
		FROM [WorkflowAction] 
	)
	WA  ON WA.ID = DW.WorkflowStepID
	WHERE DW.WorkflowStepID = @WorkFlowStepID
END
