CREATE TABLE [dbo].[Correspondence] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
	[CorrespondenceGUID] UNIQUEIDENTIFIER NULL,
    [TransactionID] BIGINT          NOT NULL,
    [DateCreated]   DATETIME        NOT NULL,
    [MessageDate]   DATETIME        NOT NULL,
    [Incoming]      BIT             NOT NULL,
    [Subject]       NVARCHAR (250)  NULL,
    [Headers]       NVARCHAR (MAX)  NULL,
    [Text]          NVARCHAR (MAX)  NULL,
    [Html]          NVARCHAR (MAX)  NULL,
    [Body]          NVARCHAR (MAX)  NULL,
    [Hidden]        BIT             NOT NULL,
    [SmtpStatusID]  INT             NOT NULL,
    [UserID]        INT             NULL,
    [MessageID]     NVARCHAR (1000) NULL,
    [AttachmentID]  INT             NULL,
    CONSTRAINT [PK_Conversations] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90),
    CONSTRAINT [FK_Conversations_Conversations] FOREIGN KEY ([TransactionID]) REFERENCES [dbo].[Transaction] ([TransactionID])
);

