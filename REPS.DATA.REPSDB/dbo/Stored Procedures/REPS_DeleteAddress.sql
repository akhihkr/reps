-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Update address; set deleted 1
-- =============================================
CREATE PROCEDURE [dbo].[REPS_DeleteAddress]
			@AddressID int,
			@Deleted bit,
			@rowCount INT OUTPUT 
		
AS
BEGIN
SET NOCOUNT OFF -- enable rowcount return 
 		UPDATE [dbo].[Address]
			SET 				
				[Deleted]= @Deleted
		WHERE [AddressID] = @AddressID
END
SET @rowCount=  @@ROWCOUNT