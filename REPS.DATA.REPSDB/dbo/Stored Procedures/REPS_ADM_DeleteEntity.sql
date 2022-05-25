-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description: remove entity and set delete true
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_DeleteEntity]
			@EntityID int,
			@Deleted bit,
			@rowCount INT OUTPUT 
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
	UPDATE [dbo].[Entity]
		SET 				
			[Deleted]= @Deleted
	WHERE [EntityID] = @EntityID
END	
SET @rowCount=  @@ROWCOUNT	
GO

