CREATE TABLE [dbo].[UserRole] (
    [UserRoleID]   INT      IDENTITY (1, 1) NOT NULL,
    [UserID]       INT      NOT NULL,
    [RoleID]       INT      NOT NULL,
    [IsActive]     BIT      NOT NULL,
    [DateCreated]  DATETIME NOT NULL,
    [DateModified] DATETIME NULL,
    [Deleted]      BIT      NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserRoleID] ASC),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID]),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

