CREATE TABLE [dbo].[AccountType] (
    [AccountTypeID] INT          NOT NULL,
    [Description]   VARCHAR (50) NOT NULL,
    [DateCreated]   DATETIME     NOT NULL,
    [DateModified]  DATETIME     NULL,
    [Deleted]       BIT          NOT NULL,
    CONSTRAINT [PK_AccountType] PRIMARY KEY CLUSTERED ([AccountTypeID] ASC)
);

