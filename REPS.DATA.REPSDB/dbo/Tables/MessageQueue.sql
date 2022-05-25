CREATE TABLE [dbo].[MessageQueue] (
    [MessageID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [KeyName]         VARCHAR (100)  NOT NULL,
    [ForeignKey]      INT            NOT NULL,
    [MimeTypeID]      INT            NOT NULL,
    [Inbound]         BIT            NOT NULL,
    [FileName]        VARCHAR (200)  NOT NULL,
    [FileContent]     VARBINARY (50) NOT NULL,
    [FileHash]        VARCHAR (MAX)  NOT NULL,
    [DateCreated]     DATETIME       NOT NULL,
    [DateModified]    DATETIME       NOT NULL,
    [MessageStatusID] INT            NOT NULL,
    [Deleted]         BIT            NOT NULL,
    CONSTRAINT [PK_MessageQueue] PRIMARY KEY CLUSTERED ([MessageID] ASC),
    CONSTRAINT [FK_MessageQueue_MessageStatus] FOREIGN KEY ([MessageStatusID]) REFERENCES [dbo].[MessageStatus] ([MessageStatusID])
);

