CREATE TABLE [dbo].[TransactionStatus] (
    [TransactionStatusID] INT          IDENTITY (1, 1) NOT NULL,
    [Description]         VARCHAR (50) NOT NULL,
    [DateCreated]         DATETIME     NOT NULL,
    [DateModified]        DATETIME     NULL,
    [Deleted]             BIT          NOT NULL,
    CONSTRAINT [PK_TransactionStatus] PRIMARY KEY CLUSTERED ([TransactionStatusID] ASC)
);

