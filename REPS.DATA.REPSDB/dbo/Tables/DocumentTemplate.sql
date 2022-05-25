CREATE TABLE [dbo].[DocumentTemplate] (
    [ID]                  INT            IDENTITY (1, 1) NOT NULL,
    [TemplateDisplayName] NVARCHAR (MAX) NULL,
    [DocumentVersionID]   INT  NULL,
    [AttributeName]       NCHAR (10)     NULL,
    [IsActive]            BIT            NULL,
    [IsDeleted]           BIT            NULL,
    [IsDocfusionTemplate] BIT            NULL,
    [DisplayOrder]        INT            NULL,
    [TemplateFixGUID] UNIQUEIDENTIFIER NULL, 
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

