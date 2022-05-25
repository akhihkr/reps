CREATE TABLE [dbo].[Alerts] (
    [ID]              BIGINT         IDENTITY (1, 1) NOT NULL,
	[AlertsGUID]	UNIQUEIDENTIFIER NULL,
    [DealID]          INT            NULL,
    [AlertTypeID]     INT            NULL,
    [StatusID]        INT            NOT NULL,
    [EventName]       VARCHAR (200)  NOT NULL,
    [Location]        VARCHAR (2000) NULL,
    [StartDate]       DATETIME       NOT NULL,
    [EndDate]         DATETIME       NULL,
    [Comment]         VARCHAR (MAX)  NULL,
    [CreatedByUserID] INT            NOT NULL,
    [DateCreated]     DATETIME       CONSTRAINT [DF_Alerts_DateCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Alerts] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Alerts_AlertStatus] FOREIGN KEY ([StatusID]) REFERENCES [dbo].[AlertStatus] ([ID]),
    CONSTRAINT [FK_Alerts_AlertType] FOREIGN KEY ([AlertTypeID]) REFERENCES [dbo].[AlertType] ([ID]),
    CONSTRAINT [FK_Alerts_Deal] FOREIGN KEY ([DealID]) REFERENCES [dbo].[Deal] ([DealID]),
    CONSTRAINT [FK_Alerts_User] FOREIGN KEY ([CreatedByUserID]) REFERENCES [dbo].[User] ([UserID])
);

