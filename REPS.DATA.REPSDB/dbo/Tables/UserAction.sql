CREATE TABLE [dbo].[UserAction] (
    [UserActionID] INT            IDENTITY (1, 1) NOT NULL,
    [Code]         NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [AbsoluteUri]  NVARCHAR (MAX) NULL,
    [Deleted]      BIT            DEFAULT ((0)) NOT NULL,
    [Editable]     BIT            NULL,
    PRIMARY KEY CLUSTERED ([UserActionID] ASC)
);

