CREATE TABLE [dbo].[JobTitle] (
    [JobTitleID]   INT          IDENTITY (1, 1) NOT NULL,
    [Description]  VARCHAR (50) NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [DateModified] DATETIME     NULL,
    [Deleted]      BIT          NOT NULL,
    CONSTRAINT [PK_JobTitle] PRIMARY KEY CLUSTERED ([JobTitleID] ASC)
);

