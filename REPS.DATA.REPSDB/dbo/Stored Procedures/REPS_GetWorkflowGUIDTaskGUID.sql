-- ===========================================
-- Author:Pravina Barosah
-- Create date: 09 Jan 2016
-- Description:	Get UserID by ASpnetID
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetWorkflowGUIDTaskGUID]
	@taskGUID varchar(1000)  
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Select
		SELECT T.[TaskID] , T.[TaskWorkflowID],T.[TaskName] 
		FROM [dbo].[Task] T
		WHERE  T.[TaskGUID] = @taskGUID 
GO