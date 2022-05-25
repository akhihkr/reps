CREATE TABLE [dbo].[UserRoleAction] (
    [UserRoleActionID] INT IDENTITY (1, 1) NOT NULL,
    [RoleID]           INT NOT NULL,
    [UserActionID]     INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserRoleActionID] ASC),
    CONSTRAINT [FK_UserRoleAction_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID]),
    CONSTRAINT [FK_UserRoleAction_UserAction] FOREIGN KEY ([UserActionID]) REFERENCES [dbo].[UserAction] ([UserActionID])
);

