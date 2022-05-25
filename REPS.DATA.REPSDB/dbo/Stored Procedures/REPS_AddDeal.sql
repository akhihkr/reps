-- =============================================
-- Author:	pravina
-- Create date: 06-Jan-2016
-- Description:	Insert Deal details of to deal table
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddDeal]
		 @UniqueReference varchar(1000)
		,@DealTypeID int
		,@WorkflowTaskID int
		,@DateCreated datetime
		,@ClientReference varchar(1000)
		,@UserID int
		,@Deleted bit
		,@Identity int OUTPUT
		,@DealProcessTaskID int
AS
	IF EXISTS (
		  SELECT [UniqueReference] FROM  [dbo].[Deal]  WHERE [UniqueReference]=@UniqueReference
		 )
		 BEGIN
			--RAISERROR('Already exists',5,1)
			PRINT 'ERROR: Already Exists.'
			--RETURN(-1)
			SET @Identity = -2
		 END
	ELSE
		BEGIN
				INSERT INTO [dbo].[Deal]
			   (				
				    [UniqueReference]
				   ,[ClientReference]
				   ,[DealTypeID]
				   ,[WorkflowTaskID]
				   ,[DateCreated]				   
				   ,[UserID]
				   ,[Deleted]
				   ,[DealProcessTaskID]
			   )
			 VALUES
				   (
						@UniqueReference
						,@ClientReference
						,@DealTypeID
						,@WorkflowTaskID
						,@DateCreated					
						,@UserID
						,@Deleted
						,@DealProcessTaskID
				   )
				SET @Identity = SCOPE_IDENTITY()
		 END
GO
