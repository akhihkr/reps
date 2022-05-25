CREATE TABLE [dbo].[Billing] (
    [BillingID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [KeyName]         VARCHAR (100) NOT NULL,
    [ForeignKey]      INT           NOT NULL,
    [BillingRateID]   BIGINT        NOT NULL,
    [UniqueReference] VARCHAR (MAX) NOT NULL,
    [InvoiceToUserID] INT           NOT NULL,
    [DateCreated]     DATETIME      NOT NULL,
    [DateModified]    DATETIME      NULL,
    [Deleted]         BIT           NOT NULL,
    CONSTRAINT [PK_Billing] PRIMARY KEY CLUSTERED ([BillingID] ASC),
    CONSTRAINT [FK_Billing_Billing] FOREIGN KEY ([BillingRateID]) REFERENCES [dbo].[BillingRate] ([BillingRateID])
);

