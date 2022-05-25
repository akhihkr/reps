CREATE TABLE [dbo].[WorkflowActionVariable] (
    [ID]                 INT IDENTITY (1, 1) NOT NULL,
    [WorkflowActionID]   INT NULL,
    [WorkflowVariableID] INT NULL,
    [IsRequired]         BIT CONSTRAINT [DF_WorkflowActionVariable_IsRequired] DEFAULT ((0)) NOT NULL,
    [IsDeleted]          BIT CONSTRAINT [DF_Table_1_AllowAttachment] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_WorkflowActionVariable] PRIMARY KEY CLUSTERED ([ID] ASC)
);

