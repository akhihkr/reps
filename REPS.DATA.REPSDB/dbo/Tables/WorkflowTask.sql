CREATE TABLE [dbo].[WorkflowTask] (
    [WorkflowTaskGUID]  UNIQUEIDENTIFIER NOT NULL,
    [WorkflowTaskID]    INT              NOT NULL,
    [WorkflowID]        INT              NOT NULL,
    [TaskID]            INT              NOT NULL,
    [WorkflowTaskOrder] INT              NULL,
    [IsDeleted]         BIT              DEFAULT ((0)) NULL,
    CONSTRAINT [PK_WorkflowTask] PRIMARY KEY CLUSTERED ([WorkflowTaskID] ASC)
);

