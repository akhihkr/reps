-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert Financial Instrument
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_AddFinancialInstrument]
			@Participant int,
			@Value decimal(18,3),
			@Deposit decimal(18,3),
			@TermLoan varchar(max),
			@InterestRate decimal(18,3),
			@LenderID int,
			@InstrumentTypeID int,
			@InterestTypeID int,
			@Identity int OUTPUT,
			@DealID int,
			@WorkflowTaskID int,
			@UserID int
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO
		[dbo].[FinancialInstrument]
		(
			 [ParticipantID]
			,[Value]
			,[Deposit]
			,[Term]
			,[InterestRate]
			,[LenderID]
			,[InstrumentTypeID]
			,[InterestTypeID]
			,[DateCreated]
			,[Deleted]
			,[InstrumentGUID]
		)
		VALUES
		(
			@Participant
			,@Value
			,@Deposit
			,@TermLoan
			,@InterestRate
			,@LenderID
			,@InstrumentTypeID
			,@InterestTypeID
			,GETDATE()
			,0
			,NEWID()
		);

		SET @Identity = SCOPE_IDENTITY()

		INSERT INTO [dbo].[Transaction]
			(
				 [DealID]
				,[TransactionTypeID]
				,[DateCreated]
				,[TransactionStatusID]
				,[Deleted]
			)
		VALUES
			(
				@DealID
				,10 -- 10 = new
				,GETDATE()
				,4 -- 4 = New
				,0
			);

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
END