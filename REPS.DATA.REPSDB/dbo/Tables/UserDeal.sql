CREATE TABLE [dbo].[UserDeal] (
    [UserRoleID]   INT           IDENTITY (1, 1) NOT NULL,
    [UserID]       INT           NOT NULL,
    [DealID]       INT           NOT NULL,
    [IsActive]     BIT           NOT NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [DateModified] DATETIME      NULL,
    [Deleted]      BIT           NOT NULL,
    [Order]        INT           NULL,
    [LastViewName] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserDeal] PRIMARY KEY CLUSTERED ([UserRoleID] ASC),
    CONSTRAINT [FK_UserDeal_Deal] FOREIGN KEY ([DealID]) REFERENCES [dbo].[Deal] ([DealID]),
    CONSTRAINT [FK_UserDeal_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

