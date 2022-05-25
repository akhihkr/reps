CREATE TABLE [dbo].[AlertStatus] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_AlertStatus] PRIMARY KEY CLUSTERED ([ID] ASC)
);

