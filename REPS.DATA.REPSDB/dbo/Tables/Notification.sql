CREATE TABLE [dbo].[Notification] (
    [NotificationID]     INT            IDENTITY (1, 1) NOT NULL,
    [NotificationTypeID] INT            NULL,
    [UserID]             INT            NULL,
    [Description]        NVARCHAR (255) NULL,
    [IsRead]             BIT            NULL,
    [DateRead]           DATETIME       NULL,
    [DateCreated]        DATETIME       NULL,
    [DateModified]       DATETIME       NULL,
    [Deleted]            BIT            NOT NULL,
    CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED ([NotificationID] ASC)
);

