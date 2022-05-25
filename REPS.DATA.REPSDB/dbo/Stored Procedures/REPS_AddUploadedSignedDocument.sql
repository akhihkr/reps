-- ========================================================
-- Author:Pravina Barosah
-- Create date: 20 June 2016
-- Description:	Save uploaded Signed document to DB
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_AddUploadedSignedDocument]
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

	SELECT @docCount = Count([ID])
		FROM  [dbo].[Document]
		where [DocumentTemplateID] = @documentTemplateID
		AND (@workflowStepId IS NULL OR [WorkflowStepID] = @workflowStepId)
		AND [IsDeleted] = 0
		and [DealID] = @dealID

		IF @docCount = 1
			BEGIN
				  UPDATE [dbo].[Document]
				   SET 
					   [SignedDocFileName] = @filename
					  ,[SignedDocMimeTypeID] = @mimeType
					  ,[SignedDocDate] = GETDATE()
					  ,[SignedUploadedByUserID] = @userID
					  ,[IsDeleted] = 0
				 WHERE [DocumentTemplateID] = @documentTemplateID
				 AND (@workflowStepId IS NULL OR [WorkflowStepID] = @workflowStepId)
				 AND	[DealID] = @dealID
				 AND	[IsDeleted] = 0;

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
						   ,[SignedDocFileName]
						   ,[SignedDocMimeTypeID]
						   ,[SignedDocDate]
						   ,[SignedUploadedByUserID]
						   ,[IsDeleted]
						   ,[WorkflowStepID])
					 VALUES
						   (@dealID
						   ,@documentTemplateID
						   ,@filename
						   ,@mimeType
						   ,GETDATE()
						   ,@userID
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