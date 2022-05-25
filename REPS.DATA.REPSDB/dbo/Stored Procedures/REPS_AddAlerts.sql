-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Alerts details into the Alerts table
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_AddAlerts]
			@EventName varchar(max),
			@Location varchar(max),
			@StartDate datetime,
			@EndDate datetime,
			@Description varchar(max),
			@UserID int,
			@AlertTypeID int,
			@DealID int,
			@WorkflowTaskID int,
			@Identity INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Alerts]
				(
					[EventName]
					,[Location]
					,[StartDate]
					,[EndDate]
					,[Comment]
					,[CreatedByUserID]
					,[StatusID]
					,[DealID]
					,[AlertTypeID]
					,[AlertsGUID]
				)
				VALUES
					(@EventName
					,@Location
					,@StartDate
					,@EndDate
					,@Description
					,@UserID
					,1
					,@DealID
					,@AlertTypeID
					,NEWID()
				);
				
	INSERT INTO [dbo].[Transaction]
				([DealID]
				,[TransactionTypeID]
				,[DateCreated]
				,[TransactionStatusID]
				,[Deleted])
			VALUES
				(@DealID
				,10 -- 10 = new
				,GETDATE()
				,4 -- 4 = New
				,0);

	DECLARE @TransactionID INT
	SET @TransactionID = SCOPE_IDENTITY();


	-- Create WorkflowProgress
	INSERT INTO [dbo].[WorkflowProgress]
			([WorkflowProgressGUID]
			,[DealID]
			,[WorkflowTaskID]
			,[TransactionID]
			,[WorkflowParentID]
			,[UserID]
			,[DateCreated]
			,[DateModified]
			,[Deleted])
		VALUES
			(NEWID()
			,@DealID
			,@WorkflowTaskID
			,@TransactionID
			,null
			,@UserID
			,GETDATE()
			,null
			,0);

	SET @Identity = SCOPE_IDENTITY();

END