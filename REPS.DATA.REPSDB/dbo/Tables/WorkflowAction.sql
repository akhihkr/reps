CREATE TABLE [dbo].[WorkflowAction] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [DisplayName] VARCHAR (100) NOT NULL,
    [IsActive]    BIT           CONSTRAINT [DF_WorkflowAction_IsActive] DEFAULT ((1)) NULL,
    [IsDeleted]   BIT           CONSTRAINT [DF_WorkflowAction_IsDeleted] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_WorkflowAction] PRIMARY KEY CLUSTERED ([ID] ASC)
);

