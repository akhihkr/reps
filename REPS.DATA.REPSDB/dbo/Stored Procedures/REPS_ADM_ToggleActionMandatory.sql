-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Toggle Action Mandatory
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_ToggleActionMandatory]
			@actionID int,
			@toggle bit,
			@rowCount INT OUTPUT 
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	IF ( @toggle = 1 )
		BEGIN
			UPDATE [dbo].[WorkflowActionMap]
			   SET [IsMandatory] = 1
			 WHERE [ID] = @actionID

			 SET @rowCount = @@ROWCOUNT
		 END
	ELSE
		BEGIN
			UPDATE [dbo].[WorkflowActionMap]
			   SET [IsMandatory] = 0
			 WHERE [ID] = @actionID

			 SET @rowCount = @@ROWCOUNT
		END

END