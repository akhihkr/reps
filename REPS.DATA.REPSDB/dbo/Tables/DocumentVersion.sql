CREATE TABLE [dbo].[DocumentVersion] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [DocumentTypeID]   INT            NULL,
    [VersionName]      NVARCHAR (50)  NULL,
    [TemplateFileName] NVARCHAR (MAX) NULL,
    [Comments]         NCHAR (10)     NULL,
    [MimeTypeID]       NVARCHAR (50)  NULL,
    [XMLStoredProc]    NVARCHAR (50)  NULL,
    [DateCreated]      DATETIME       NULL,
    [CreatedByUserID]  INT            NULL,
    [eSignable]        BIT            NULL,
    [IsStaticDoc]      BIT            NULL,
    [IsDeleted]        BIT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

