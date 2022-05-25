-- ===========================================
-- Author:Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Insert correspondence details
-- into the Correspondence & Transaction tables
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_AddCorrespondence]
			@DealID int,
			@Subject varchar(max),
			@UserID int,
			@Headers varchar(max),
			@Text varchar(max),
			@Html varchar(max),
			@Body varchar(max),
			@Status int,
			@filename nvarchar(max),
			@mimeType varchar(50),
			@documentTemplateID int,
			@transactionTypeID int,
			@transactionStatusID int,
			@WorkflowTaskID int,
			@attachmentstatus int,
			@Identity INT OUTPUT,
			@DocumentIDReturn INT OUTPUT
AS
BEGIN
	DECLARE @CorrespondenceID INT;
	DECLARE @DocumentID INT;

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Transaction]
				([DealID]
				,[TransactionTypeID]
				,[DateCreated]
				,[TransactionStatusID]
				,[Deleted])
			VALUES
				(@DealID
				,10 -- = new email
				,GETDATE()
				,1
				,0);

	DECLARE @TransactionID INT
	SET @TransactionID = SCOPE_IDENTITY();

	INSERT INTO [dbo].[Correspondence]
				([TransactionID]
				,[DateCreated]
				,[MessageDate]
				,[Incoming]
				,[Subject]
				,[Headers]
				,[Text]
				,[Html]
				,[Body]
				,[Hidden]
				,[SmtpStatusID]
				,[UserID]
				,[MessageID]
				,[CorrespondenceGUID])
			VALUES
				(@TransactionID
				,GETDATE()
				,GETDATE()
				,0
				,@Subject
				,@Headers
				,@Text
				,@Html
				,@Body
				,0
				,@Status
				,@UserID
				,'msgid'
				,NEWID());

	SET @CorrespondenceID = SCOPE_IDENTITY();

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
			   ,0)

	SET @Identity = SCOPE_IDENTITY();


	-- START ATTACHMENT --
	
	IF (@attachmentstatus <> 0)
	BEGIN
		INSERT INTO [dbo].[Document]
				([DealID]
				,[DocumentTemplateID]
				,[DateCreated]
				,[CreatedByUserID]
				,[GeneratedDocFileName]
				,[MimeTypeID]
				,[IsDeleted])
			VALUES
				(@dealID
				,null
				,GETDATE()
				,@userID
				,@filename
				,@mimeType
				,0);

		SET @DocumentIDReturn = SCOPE_IDENTITY();

		SELECT @DocumentID = SCOPE_IDENTITY();

		UPDATE
			[dbo].[Correspondence]
		SET
			[AttachmentID] = @DocumentID
		WHERE
			[ID] = @CorrespondenceID;

	
	 -- Create TransactionDocuments
	 INSERT INTO [dbo].[TransactionDocuments]
				([TransactionID]
				,[DocumentID]
				,[IsDeleted])
			VALUES
				(@TransactionID
				,@DocumentID
				,0)
	END

END