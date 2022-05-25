CREATE TABLE [dbo].[TransactionDocuments] (
    [ID]            BIGINT IDENTITY (1, 1) NOT NULL,
    [TransactionID] BIGINT NOT NULL,
    [DocumentID]    INT    NOT NULL,
    [IsDeleted]     BIT    CONSTRAINT [DF_TransactionDocuments_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TransactionDocuments] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TransactionDocuments_Document] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[Document] ([ID]),
    CONSTRAINT [FK_TransactionDocuments_Transaction] FOREIGN KEY ([TransactionID]) REFERENCES [dbo].[Transaction] ([TransactionID])
);

