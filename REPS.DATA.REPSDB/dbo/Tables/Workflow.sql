CREATE TABLE [dbo].[Workflow] (
    [WorkflowGUID] UNIQUEIDENTIFIER NOT NULL,
    [WorkflowID]   INT              NOT NULL,
    [WorkflowName] VARCHAR (255)    NULL,
    [IsDeleted]    BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Workflow] PRIMARY KEY CLUSTERED ([WorkflowID] ASC)
);

