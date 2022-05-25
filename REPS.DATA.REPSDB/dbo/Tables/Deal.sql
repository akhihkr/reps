CREATE TABLE [dbo].[Deal] (
    [DealID]            INT            IDENTITY (1, 1) NOT NULL,
    [UniqueReference]   VARCHAR (1000) NOT NULL,
    [DealTypeID]        INT            NOT NULL,
    [WorkflowTaskID]    INT            NULL,
    [DateCreated]       DATETIME       NOT NULL,
    [UserID]            INT            NOT NULL,
    [DateModified]      DATETIME       NULL,
    [Deleted]           BIT            NOT NULL,
    [ClientReference]   VARCHAR (1000) NULL,
    [CompletionDate]    DATETIME       NULL,
    [DealProcessTaskID] INT            NULL,
    CONSTRAINT [PK_Deal] PRIMARY KEY CLUSTERED ([DealID] ASC),
    CONSTRAINT [FK_Deal_DealType] FOREIGN KEY ([DealTypeID]) REFERENCES [dbo].[DealType] ([DealTypeID]),
    CONSTRAINT [FK_Deal_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);



