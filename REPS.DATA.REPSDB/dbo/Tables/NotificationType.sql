CREATE TABLE [dbo].[NotificationType] (
    [NotificationTypeID] INT           IDENTITY (1, 1) NOT NULL,
    [Description]        VARCHAR (255) NULL,
    [Date Created]       DATETIME      NULL,
    [Deleted]            BIT           NOT NULL,
    CONSTRAINT [PK_NotificationType] PRIMARY KEY CLUSTERED ([NotificationTypeID] ASC)
);

