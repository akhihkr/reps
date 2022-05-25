CREATE TABLE [dbo].[Task] (
    [TaskGUID]        UNIQUEIDENTIFIER NOT NULL,
    [TaskID]          INT              NOT NULL,
    [TaskName]        VARCHAR (255)    NOT NULL,
    [TaskDisplayIcon] VARCHAR (255)    NULL,
    [TaskControl]     VARCHAR (255)    NULL,
    [TaskWorkflowID]  INT              NULL,
    [IsDeleted]       BIT              DEFAULT ((0)) NOT NULL,
    [IsWorkflowTask] BIT NULL, 
    CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED ([TaskID] ASC)
);

