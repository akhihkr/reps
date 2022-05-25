CREATE TABLE [dbo].[FinancialTransaction] (
    [FinancialTransactionID] INT      IDENTITY (1, 1) NOT NULL,
    [DealID]                 INT      DEFAULT ((0)) NOT NULL,
    [ObligationID]           INT      NULL,
    [InstrumentID]           INT      NOT NULL,
    [DateCreated]            DATETIME NOT NULL,
    [DateModified]           DATETIME NULL,
    [Deleted]                BIT      NOT NULL,
    CONSTRAINT [PK_FinancialTransaction] PRIMARY KEY CLUSTERED ([FinancialTransactionID] ASC),
    CONSTRAINT [FK_FinancialTransaction_Deal] FOREIGN KEY ([DealID]) REFERENCES [dbo].[Deal] ([DealID]),
    CONSTRAINT [FK_FinancialTransaction_FinancialInstrument] FOREIGN KEY ([InstrumentID]) REFERENCES [dbo].[FinancialInstrument] ([InstrumentID])
);

