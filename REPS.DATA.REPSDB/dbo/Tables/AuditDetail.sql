CREATE TABLE [dbo].[AuditDetail] (
    [AuditDetailID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [AuditID]       BIGINT        NULL,
    [Column]        VARCHAR (100) NULL,
    [OldValue]      VARCHAR (MAX) NULL,
    [NewValue]      VARCHAR (MAX) NULL,
    [Deleted]       BIT           NOT NULL,
    CONSTRAINT [PK_AuditDetail] PRIMARY KEY CLUSTERED ([AuditDetailID] ASC),
    CONSTRAINT [FK_AuditDetail_Audit] FOREIGN KEY ([AuditID]) REFERENCES [dbo].[Audit] ([AuditID])
);

