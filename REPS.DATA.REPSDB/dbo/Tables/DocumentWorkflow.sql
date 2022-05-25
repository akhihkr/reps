CREATE TABLE [dbo].[DocumentWorkflow] (
    [ID]                 INT      IDENTITY (1, 1) NOT NULL,
    [DocumentVersionID] INT      NULL,
    [WorkflowStepID]     INT      NULL,
    [DateCreated]        DATETIME NULL,
    [CreatedByUserID]    INT      NULL,
    [IsDeleted]          BIT      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

