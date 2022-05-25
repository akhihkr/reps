CREATE TABLE [dbo].[WorkflowTransaction] (
    [ID]               BIGINT IDENTITY (1, 1) NOT NULL,
    [WorkflowActionID] INT    NOT NULL,
    [TransactionID]    BIGINT NOT NULL,
    [IsDeleted]        BIT    CONSTRAINT [DF_WorkflowTransaction_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WorkflowTransaction] PRIMARY KEY CLUSTERED ([ID] ASC)
);

