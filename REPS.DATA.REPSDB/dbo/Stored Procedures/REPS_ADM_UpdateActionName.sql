-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Update Action Name
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_UpdateActionName]
			@actionName varchar(max),
			@actionID int,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [dbo].[WorkflowAction]
	   SET [DisplayName] = @actionName
	 WHERE [ID] = @actionID

	 SET @rowCount=  @@ROWCOUNT	

END