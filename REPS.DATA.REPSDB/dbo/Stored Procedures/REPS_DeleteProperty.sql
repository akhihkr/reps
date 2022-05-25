-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: Delete Property Details
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteProperty]
			@PropertyID int,
			@Deleted bit,
			@rowCount INT OUTPUT 
AS
SET NOCOUNT OFF -- enable rowcount return 
		UPDATE [dbo].[Property]
			SET 				
				[Deleted]= @Deleted
		WHERE [PropertyID] = @PropertyID

SET @rowCount=  @@ROWCOUNT
GO