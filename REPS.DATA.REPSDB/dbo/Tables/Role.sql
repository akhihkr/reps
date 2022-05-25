CREATE TABLE [dbo].[Role] (
    [RoleID]       INT         IDENTITY (1, 1) NOT NULL,
    [Description]  NCHAR (255) NULL,
    [DateCreated]  DATETIME    NOT NULL,
    [DateModified] DATETIME    NULL,
    [Deleted]      BIT         NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

