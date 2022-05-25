-- =============================================
-- Author: Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Property Info to Property table 
-- =============================================
CREATE PROCEDURE [dbo].[REPS_AddProperty]
			@DealID int,
			@PropertyDescription varchar(1000),
			@LegalDescription varchar(1000),
			@AddressID int,
			@PropertyTypeID int,
			@Verified bit,
			@DateCreated datetime,
			@Deleted bit,
			@Identity int OUTPUT,
			@WorkflowTaskID int,
			@UserID int
AS
	INSERT INTO [dbo].[Property]
           (				
				[DealID]
				,[PropertyDescription]
				,[LegalDescription]
				,[AddressID]
				,[PropertyTypeID]
				,[Verified]
				,[DateCreated]
				,[Deleted]
				,[PropertyGUID]
		   )
     VALUES
           (
				@DealID,
				@PropertyDescription,
				@LegalDescription,
				@AddressID,
				@PropertyTypeID,
				@Verified,
				@DateCreated,
				@Deleted,
				NEWID()
		   )

		SET @Identity = SCOPE_IDENTITY()

		/* Check if need to update deal status form new to draft */
		DECLARE @Cnt int 
		SELECT @Cnt=COUNT([ParticipantID])
		FROM 
				[dbo].[Property] pr
				LEFT JOIN [dbo].[Deal] de
				ON pr.[DealID] = de.[DealID]
				LEFT JOIN [dbo].[Participant] pa
				ON de.[DealID] = pa.[DealID]
		WHERE pa.DealID = @DealID
		AND de.[WorkflowTaskID] =100

		/* Need to so Update from NEW to DRAFT */
		IF (@Cnt >= 1)
		BEGIN
			UPDATE [dbo].[Deal] SET [WorkflowTaskID] = 200 WHERE  DealID = @DealID	
		END

	--	INSERT INTO [dbo].[Transaction]
	--		(
	--			 [DealID]
	--			,[TransactionTypeID]
	--			,[DateCreated]
	--			,[TransactionStatusID]
	--			,[Deleted]
	--		)
	--	VALUES
	--		(
	--			@DealID
	--			,10 -- 10 = new
	--			,GETDATE()
	--			,4 -- 4 = New
	--			,0
	--		);

	--DECLARE @TransactionID INT
	--SET @TransactionID = SCOPE_IDENTITY();
	---- Create WorkflowProgress
	--INSERT INTO [dbo].[WorkflowProgress]
	--		([WorkflowProgressGUID]
	--		,[DealID]
	--		,[WorkflowTaskID]
	--		,[TransactionID]
	--		,[WorkflowParentID]
	--		,[UserID]
	--		,[DateCreated]
	--		,[DateModified]
	--		,[Deleted])
	--	VALUES
	--		(NEWID()
	--		,@DealID
	--		,@WorkflowTaskID
	--		,@TransactionID
	--		,null
	--		,@UserID
	--		,GETDATE()
	--		,null
	--		,0);
GO

