CREATE TABLE [dbo].[TransactionType] (
    [TransactionTypeID] INT          NOT NULL,
    [Description]       VARCHAR (50) NOT NULL,
    [DateCreated]       DATETIME     NOT NULL,
    [DateModified]      DATETIME     NULL,
    [Deleted]           BIT          NOT NULL,
    CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED ([TransactionTypeID] ASC)
);

