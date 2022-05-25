-- ========================================================
-- Author:Pravina Barosah
-- Create date: 08 March 2017
-- Description:	Save uploaded Standard document to DB
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_AddUploadedStandardDocument]
			@filename nvarchar(max),
			@mimeType varchar(50),
			@documentTemplateID int,
			@dealID int,
			@userID int,
			@transactionTypeID int,
			@transactionStatusID int,
			@workflowStepId int,
			@WorkflowTaskID int,
			@rowCount INT OUTPUT

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @docCount int
	SET  @docCount = 0
	DECLARE @DocumentID int;

	--make a select statement to check if doc exist and make an update 
	SELECT @docCount = Count([ID])
		FROM  [dbo].[Document]
		where [DocumentTemplateID] = @documentTemplateID
		AND (@workflowStepId IS NULL OR [WorkflowStepID] = @workflowStepId)
		AND [IsDeleted] = 0
		AND [DealID] = @dealID

		IF @docCount = 1
			BEGIN
				  UPDATE [dbo].[Document]
				   SET 
					   [DateCreated] = GETDATE()
					  ,[CreatedByUserID] = @userID
					  ,[GeneratedDocFileName] = @filename
					  ,[MimeTypeID] = @mimeType
					  ,[IsDeleted] = 0
				 WHERE [DocumentTemplateID] = @documentTemplateID
				 AND (@workflowStepId IS NULL OR [WorkflowStepID] = @workflowStepId)
				 AND	[DealID] = @dealID
				 AND	[IsDeleted] = 0

				  -- Get current Document ID
				 SELECT @DocumentID = [dbo].[Document].ID
				 FROM [dbo].[Document]
				 WHERE [DocumentTemplateID] = @documentTemplateID
				 AND (@workflowStepId IS NULL OR [WorkflowStepID] = @workflowStepId)
				 AND	[DealID] = @dealID
				 AND	[IsDeleted] = 0;
			 END
		 ELSE
			BEGIN
				INSERT INTO [dbo].[Document]
						   ([DealID]
						   ,[DocumentTemplateID]
						   ,[DateCreated]
						   ,[CreatedByUserID]
						   ,[GeneratedDocFileName]
						   ,[MimeTypeID]
						   ,[IsDeleted]
						   ,[WorkflowStepID])
					 VALUES
						   (@dealID
						   ,@documentTemplateID
						   ,GETDATE()
						   ,@userID
						   ,@filename
						   ,@mimeType
						   ,0
						   ,@workflowStepId)	

				-- Newly inserted DocumentID
				SELECT @DocumentID = SCOPE_IDENTITY();
			END

	DECLARE @TransactionID int;

	-- Create Transaction
	INSERT INTO [dbo].[Transaction]
			   ([DealID]
			   ,[TransactionTypeID]
			   ,[DateCreated]
			   ,[DateModified]
			   ,[TransactionStatusID]
			   ,[Deleted])
		 VALUES
			   (@dealID
			   ,@transactionTypeID		--12
			   ,GETDATE()
			   ,null
			   ,@transactionStatusID	--4
			   ,0);

	 SELECT @TransactionID =  SCOPE_IDENTITY();

	 --Set all previous TransactionDocuments IsDeleted = True
	UPDATE  [dbo].[TransactionDocuments]
		SET [IsDeleted] = 1
		WHERE [DocumentID] IN ( Select [DocumentID] 
								FROM   [dbo].[Document]
								WHERE  [DealID] = @dealID
								AND	   [DocumentTemplateID] = @documentTemplateID
								AND    [IsDeleted] = 1
								)


	 -- Create TransactionDocuments
	 INSERT INTO [dbo].[TransactionDocuments]
				([TransactionID]
				,[DocumentID]
				,[IsDeleted])
			VALUES
				(@TransactionID
				,@DocumentID
				,0)

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
			   ,@workflowStepId
			   ,@UserID
			   ,GETDATE()
			   ,null
			   ,0);

	--SELECT @DocumentID;
	SET @rowCount = @DocumentID;
END