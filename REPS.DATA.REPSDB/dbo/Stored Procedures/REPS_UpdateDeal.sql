-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	update deal table 
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_UpdateDeal]
		 @UniqueReference varchar(1000)
		,@DealID int
		,@DealTypeID int
		,@ClientReference varchar(1000)
		,@DealProcessTaskID int
		,@dateCreatedModified date
		,@rowCount INT OUTPUT 
		
AS
	BEGIN
		SET NOCOUNT OFF -- enable rowcount return
			UPDATE [dbo].[Deal]
			SET [ClientReference] = @ClientReference ,[DealTypeID] = @DealTypeID ,[DealProcessTaskID] = @DealProcessTaskID ,[DateModified] = @dateCreatedModified
			
			WHERE([DealID] = @DealID ) 
				AND 
				-- if the variables values has update 
				(ClientReference != @ClientReference OR DealTypeId !=  @DealTypeID OR [DealProcessTaskID] != @DealProcessTaskID)
	 END
	SET @rowCount=  @@ROWCOUNT
GO