-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Get Task Workflow ID by Deal ID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetTaskWorkflowIDByDealID]
			@dealID int

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [dbo].[Task].TaskWorkflowID
	  FROM [dbo].[Deal]
		INNER JOIN [dbo].[Task]
		ON [dbo].[Deal].DealProcessTaskID =  [dbo].[Task].TaskID
	 WHERE [dbo].[Deal].DealID = @dealID

END