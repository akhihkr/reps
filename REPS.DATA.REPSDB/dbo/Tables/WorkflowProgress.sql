CREATE TABLE [dbo].[WorkflowProgress] (
    [WorkflowProgressGUID] UNIQUEIDENTIFIER NOT NULL,
    [WorkflowProgressID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [DealID]               INT              NOT NULL,
    [WorkflowTaskID]       INT              NOT NULL,
    [TransactionID]        BIGINT           NULL,
    [WorkflowParentID]     INT              NULL,
    [UserID]               INT              NULL,
    [DateCreated]          DATETIME         NOT NULL,
    [DateModified]         DATETIME         NULL,
    [Deleted]              BIT              NULL,
    CONSTRAINT [PK_WorkflowProgress] PRIMARY KEY CLUSTERED ([WorkflowProgressID] ASC)
);

