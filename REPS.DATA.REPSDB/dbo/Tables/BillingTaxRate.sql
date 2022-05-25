CREATE TABLE [dbo].[BillingTaxRate] (
    [TaxRateID]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [BillingRateID] BIGINT          NOT NULL,
    [TaxValue]      NUMERIC (18, 3) NOT NULL,
    [DateCreated]   DATETIME        NOT NULL,
    [DateModified]  DATETIME        NULL,
    [Deleted]       BIT             NOT NULL,
    CONSTRAINT [PK_BillingTaxRate] PRIMARY KEY CLUSTERED ([TaxRateID] ASC),
    CONSTRAINT [FK_BillingTaxRate_BillingRate] FOREIGN KEY ([BillingRateID]) REFERENCES [dbo].[BillingRate] ([BillingRateID])
);

