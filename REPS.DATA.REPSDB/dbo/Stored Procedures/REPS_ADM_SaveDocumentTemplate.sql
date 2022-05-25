-- =============================================
-- Author:	Pravina Barosah
-- Create date: 06 Jan 2016
-- Description:	Save Document Template
-- =============================================
CREATE PROCEDURE [dbo].[REPS_ADM_SaveDocumentTemplate]
			@templateID int,
			@templateName VARCHAR(MAX),
			@versionName VARCHAR(MAX),
			@xmlSPROC NVARCHAR(MAX),
			@templateFile NVARCHAR(MAX),
			@mimeTypeID VARCHAR(50),
			@userID int,
			@eSignable bit,
			@isStaticDoc bit,
			@isActive bit,
			@isDocfusion bit,
			@Identity int OUTPUT
AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @OldVersionID int,
			@OldTemplate  nvarchar(MAX),
			@OldXmlSPROC nvarchar(max),
			@DocTypeID int,
			@OldMimeTypeID varchar(50),
			@NewVersionID int,
			@InsertedDocTemplateID int;

	-- Get Old Version ID
	SELECT @OldVersionID = [DocumentVersionID]
		FROM [dbo].[DocumentTemplate]
		WHERE [ID] = @templateID

	-- Archive Old Version
	UPDATE [dbo].[DocumentVersion]
	   SET [IsDeleted] = 1
	 WHERE [ID] = @OldVersionID

	 -- Get previous data if no file has been uploaded
	SELECT @OldTemplate = [TemplateFileName],
		   @DocTypeID   = [DocumentTypeID],
		   @OldMimeTypeID = [MimeTypeID],
		   @OldXmlSPROC = [XMLStoredProc]
	  FROM [dbo].[DocumentVersion]
	  WHERE [ID] = @OldVersionID

	DECLARE @FinalTemplateFile nvarchar(max),
			@FinalMimeTypeID   varchar(50),
			@FinalXmlSPROC nvarchar(max)

	IF @templateFile IS null
		BEGIN
		 SET @FinalTemplateFile = @OldTemplate
		END
	ELSE
		BEGIN
		 SET @FinalTemplateFile = @templateFile
		END

	IF @mimeTypeID IS null
		BEGIN
		 SET @FinalMimeTypeID = @OldMimeTypeID
		END
	ELSE
		BEGIN
		 SET @FinalMimeTypeID = @mimeTypeID
		END

	IF @xmlSPROC IS null
		BEGIN
		 SET @FinalXmlSPROC = @OldXmlSPROC
		END
	ELSE
		BEGIN
		 SET @FinalXmlSPROC = @xmlSPROC
		END

	-- Create New Version
	INSERT INTO [dbo].[DocumentVersion]
			   ([DocumentTypeID]
			   ,[VersionName]
			   ,[TemplateFileName]
			   ,[MimeTypeID]
			   ,[XMLStoredProc]
			   ,[DateCreated]
			   ,[CreatedByUserID]
			   ,[eSignable]
			   ,[IsStaticDoc]
			   ,[IsDeleted])
		 VALUES
			   ( @DocTypeID
			   , @versionName
			   , @FinalTemplateFile
			   , @FinalMimeTypeID
			   ,@FinalXmlSPROC
			   ,GETDATE()
			   ,@userID
			   ,@eSignable
			   ,@isStaticDoc
			   ,0)

	SELECT @NewVersionID = SCOPE_IDENTITY();

	-- Archive old template
	UPDATE [dbo].[DocumentTemplate]
	   SET [IsDeleted] = 1
	 WHERE [ID] = @templateID

	-- Insert New template
	INSERT INTO [dbo].[DocumentTemplate]
			   ([TemplateDisplayName]
			   ,[DocumentVersionID]
			   ,[IsActive]
			   ,[IsDeleted]
			   ,[IsDocfusionTemplate])
		 VALUES
			   (@templateName
			   ,@NewVersionID
			   ,@isActive
			   ,0
			   ,@isDocfusion)


	SELECT @InsertedDocTemplateID = SCOPE_IDENTITY();

	--Archive Old Template in Workflow
	UPDATE [dbo].[DocumentWorkflow]
	   SET [IsDeleted] = 1
	 WHERE [DocumentVersionID] = @NewVersionID

	--Insert New Template in workflow
	INSERT INTO [dbo].[DocumentWorkflow]
			([DocumentVersionID]
			,[WorkflowStepID]
			,[DateCreated]
			,[CreatedByUserID]
			,[IsDeleted])
		VALUES
			(@InsertedDocTemplateID
			,1
			,GETDATE()
			,@userID
			,0)


	SET @Identity = @NewVersionID;

END