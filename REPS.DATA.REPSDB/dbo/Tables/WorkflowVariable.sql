CREATE TABLE [dbo].[WorkflowVariable] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [VariableTypeID] INT           NOT NULL,
    [DisplayName]    VARCHAR (100) NOT NULL,
    [IsDeleted]      BIT           CONSTRAINT [DF_WorkflowVariable_IsDeleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_WorkflowVariable] PRIMARY KEY CLUSTERED ([ID] ASC)
);

