CREATE TABLE [dbo].[FinancialInstrumentDetail] (
    [InstrumentDetailID]  INT             IDENTITY (1, 1) NOT NULL,
    [InstrumentID]        INT             NOT NULL,
    [InstrumentTypeID]    INT             NOT NULL,
    [InterestVariationID] INT             NOT NULL,
    [InterestTypeID]      INT             NOT NULL,
    [CurrencyTypeID]      INT             NOT NULL,
    [Value]               NUMERIC (18, 3) NOT NULL,
    [DateCreated]         DATETIME        NOT NULL,
    [DateModified]        DATETIME        NULL,
    [Deleted]             BIT             NOT NULL,
    CONSTRAINT [PK_FinancialInstrumentDetail] PRIMARY KEY CLUSTERED ([InstrumentDetailID] ASC),
    CONSTRAINT [FK_FinancialInstrumentDetail_CurrencyType] FOREIGN KEY ([CurrencyTypeID]) REFERENCES [dbo].[CurrencyType] ([CurrencyTypeID]),
    CONSTRAINT [FK_FinancialInstrumentDetail_InstrumentType] FOREIGN KEY ([InstrumentTypeID]) REFERENCES [dbo].[InstrumentType] ([InstrumentTypeID]),
    CONSTRAINT [FK_FinancialInstrumentDetail_InterestType] FOREIGN KEY ([InterestTypeID]) REFERENCES [dbo].[InterestType] ([InterestTypeID]),
    CONSTRAINT [FK_FinancialInstrumentDetail_InterestVariation] FOREIGN KEY ([InterestVariationID]) REFERENCES [dbo].[InterestVariation] ([InterestVariationID])
);

