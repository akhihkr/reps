CREATE TABLE [dbo].[WorkflowActionMap] (
    [ID]                INT      IDENTITY (1, 1) NOT NULL,
    [WorkflowActionID]  INT      NOT NULL,
    [WorkflowTaskID]    INT      NOT NULL,
    [AllowAttachments]  BIT      CONSTRAINT [DF_WorkflowActionMap_AllowAttachments] DEFAULT ((0)) NOT NULL,
    [ParticipantTypeID] INT      NULL,
    [DateCreated]       DATETIME CONSTRAINT [DF_WorkflowActionMap_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedByUserID]   INT      NULL,
    [IsDeleted]         BIT      CONSTRAINT [DF_WorkflowActionMap_IsDeleted] DEFAULT ((0)) NOT NULL,
    [IsMandatory]       BIT      NULL,
    CONSTRAINT [PK_WorkflowActionMap] PRIMARY KEY CLUSTERED ([ID] ASC)
);

