CREATE TABLE [dbo].[VariableType] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [Type]             VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (100) NULL,
    [DefaultSortOrder] INT           NULL,
    CONSTRAINT [PK_VariableType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

