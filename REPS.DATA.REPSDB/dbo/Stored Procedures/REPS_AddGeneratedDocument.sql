-- ========================================================
-- Author: Pravina Barosah
-- Create date: 08 March 2017
-- Description:	Save generated document
-- ========================================================
CREATE PROCEDURE [dbo].[REPS_AddGeneratedDocument]
			@dealID int,
			@userID int,
			@templateID int,
			@WorkflowStepID int,
			@mimeType varchar(50),
			@transactionTypeID int,
			@transactionStatusID int,
			@WorkflowTaskID int,
			@filename nvarchar(MAX),
			@rowCount INT OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

		DECLARE @ExistingDocuments int,
			@SignedDocFileName nvarchar(max) = NULL,
			@SignedDocMimeTypeID nvarchar(50) = NULL,
			@SignedDocDate datetime = NULL,
			@SignedUploadedByUserID int = NULL;


	SELECT @ExistingDocuments = Count(*)
	 FROM	[dbo].[Document]
		 WHERE [DealID] = @dealID
		 AND [DocumentTemplateID] = @templateID
		 AND [WorkflowStepID] =@WorkflowStepID
		 AND [IsDeleted] = 0
		 AND [SignedDocFileName] IS NOT NULL;

	IF ( @ExistingDocuments = 1 )		-- Get previous row values and store it in the new row
	BEGIN
		SELECT  @SignedDocFileName = [SignedDocFileName],
			    @SignedDocMimeTypeID = [SignedDocMimeTypeID],
				@SignedDocDate = [SignedDocDate],
				@SignedUploadedByUserID = [SignedUploadedByUserID]
		FROM	[dbo].[Document]
		 WHERE [DealID] = @dealID
		 AND   [DocumentTemplateID] = @templateID
		 AND [WorkflowStepID] =@WorkflowStepID
		 AND   [IsDeleted] = 0
		 AND   [SignedDocFileName] IS NOT NULL;
	END

	-- Set all previous documents IsDeleted = True
		UPDATE [dbo].[Document]
		   SET [IsDeleted] = 1
		 WHERE [DealID] = @dealID
		  AND [DocumentTemplateID] = @templateID
		  AND [WorkflowStepID] = @WorkflowStepID;	

	DECLARE @DocumentID int;

	-- Insert New generated file
		INSERT INTO [dbo].[Document]
				   ([DealID]
				   ,[DocumentTemplateID]
				   ,[DateCreated]
				   ,[CreatedByUserID]
				   ,[GeneratedDocFileName]
				   ,[MimeTypeID]
				   ,[SignedDocFileName]
				   ,[SignedDocMimeTypeID]
				   ,[SignedDocDate]
				   ,[SignedUploadedByUserID]
				   ,[IsDeleted]
				   ,[WorkflowStepID])
			 VALUES
				   (@dealID
				   ,@templateID
				   ,GETDATE()
				   ,@userID
				   ,@filename
				   ,@mimeType,
				   	@SignedDocFileName,
					@SignedDocMimeTypeID,
					@SignedDocDate,
					@SignedUploadedByUserID
				   ,0
				   ,@WorkflowStepID)

	SELECT @DocumentID = SCOPE_IDENTITY();

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
			   (@DealID
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
								AND	   [DocumentTemplateID] = @templateID
								AND [WorkflowStepID] = @WorkflowStepID
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
			   ,@dealID
			   ,@WorkflowTaskID
			   ,@TransactionID
			   ,null
			   ,@userID
			   ,GETDATE()
			   ,null
			   ,0)

	--SELECT @DocumentID;
	SET @rowCount = @DocumentID

END