CREATE TABLE [dbo].[Document] (
    [ID]                     INT            IDENTITY (1, 1) NOT NULL,
    [DealID]                 INT            NULL,
    [DocumentTemplateID]     INT            NULL,
    [DateCreated]            DATETIME       NULL,
    [CreatedByUserID]        INT            NULL,
    [GeneratedDocFileName]   NVARCHAR (MAX) NULL,
    [FileHash]               NVARCHAR (MAX) NULL,
    [MimeTypeID]             NVARCHAR (50)  NULL,
    [SignedDocFileName]      NVARCHAR (MAX) NULL,
    [SignedDocMimeTypeID]    NVARCHAR (50)  NULL,
    [SignedDocDate]          DATETIME       NULL,
    [SignedDocHash]          NVARCHAR (MAX) NULL,
    [SignedUploadedByUserID] INT            NULL,
    [IsDeleted]              BIT            NULL,
    [WorkflowStepID] INT NULL, 
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

