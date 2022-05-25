CREATE TABLE [dbo].[AlertType] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (200) NOT NULL,
    [IsDeleted]   BIT           CONSTRAINT [DF_AlertType_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_AlertType] PRIMARY KEY CLUSTERED ([ID] ASC)
);

