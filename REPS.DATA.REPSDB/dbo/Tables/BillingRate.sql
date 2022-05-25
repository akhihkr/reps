CREATE TABLE [dbo].[BillingRate] (
    [BillingRateID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Description]   VARCHAR (1000) NOT NULL,
    [StartDate]     DATETIME       NOT NULL,
    [EndDate]       DATETIME       NOT NULL,
    [TieredRate]    BIT            NOT NULL,
    [IsActive]      BIT            NOT NULL,
    [DateCreated]   DATETIME       NOT NULL,
    [DateModified]  DATETIME       NULL,
    [Deleted]       BIT            NOT NULL,
    CONSTRAINT [PK_BillingRate] PRIMARY KEY CLUSTERED ([BillingRateID] ASC)
);

