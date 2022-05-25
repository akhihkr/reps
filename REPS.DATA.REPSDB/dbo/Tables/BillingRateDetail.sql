CREATE TABLE [dbo].[BillingRateDetail] (
    [BillingRateDetailID] BIGINT        IDENTITY (1, 1) NOT NULL,
    [TierNumber]          INT           NOT NULL,
    [StartValue]          NUMERIC (18)  NOT NULL,
    [EndValue]            NUMERIC (18)  NOT NULL,
    [CurrencyID]          INT           NOT NULL,
    [Value]               VARCHAR (MAX) NOT NULL,
    [DateCreated]         DATETIME      NOT NULL,
    [DateModified]        DATETIME      NULL,
    [Deleted]             BIT           NOT NULL,
    CONSTRAINT [PK_BillingRateDetail] PRIMARY KEY CLUSTERED ([BillingRateDetailID] ASC),
    CONSTRAINT [FK_BillingRateDetail_CurrencyType] FOREIGN KEY ([CurrencyID]) REFERENCES [dbo].[CurrencyType] ([CurrencyTypeID])
);

