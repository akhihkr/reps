CREATE TABLE [dbo].[DocumentType] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [OrderBy]     INT            NULL,
    [DateCreated] DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

