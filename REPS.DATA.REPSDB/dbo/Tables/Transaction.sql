CREATE TABLE [dbo].[Transaction] (
    [TransactionID]       BIGINT   IDENTITY (1, 1) NOT NULL,
    [DealID]              INT      NOT NULL,
    [TransactionTypeID]   INT      NOT NULL,
    [DateCreated]         DATETIME CONSTRAINT [DF_Transaction_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified]        DATETIME NULL,
    [TransactionStatusID] INT      CONSTRAINT [DF_Transaction_TransactionStatusID] DEFAULT ((0)) NOT NULL,
    [Deleted]             BIT      CONSTRAINT [DF_Transaction_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([TransactionID] ASC),
    CONSTRAINT [FK_Transaction_Deal] FOREIGN KEY ([DealID]) REFERENCES [dbo].[Deal] ([DealID]),
    CONSTRAINT [FK_Transaction_TransactionStatus] FOREIGN KEY ([TransactionStatusID]) REFERENCES [dbo].[TransactionStatus] ([TransactionStatusID]),
    CONSTRAINT [FK_Transaction_TransactionType] FOREIGN KEY ([TransactionTypeID]) REFERENCES [dbo].[TransactionType] ([TransactionTypeID])
);

