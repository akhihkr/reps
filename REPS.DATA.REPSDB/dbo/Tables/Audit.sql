CREATE TABLE [dbo].[Audit] (
    [AuditID]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [TableName]    VARCHAR (100) NOT NULL,
    [ForeignKey]   VARCHAR (100) NOT NULL,
    [UserID]       INT           NOT NULL,
    [DateModified] VARCHAR (MAX) NOT NULL,
    [Deleted]      BIT           NOT NULL,
    [Description]  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED ([AuditID] ASC),
    CONSTRAINT [FK_Audit_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

