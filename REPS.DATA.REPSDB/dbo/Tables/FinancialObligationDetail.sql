CREATE TABLE [dbo].[FinancialObligationDetail] (
    [ObligationDetailID] INT             IDENTITY (1, 1) NOT NULL,
    [ObligationID]       INT             NOT NULL,
    [InterestRate]       NUMERIC (18, 3) NULL,
    [Value]              NUMERIC (18, 3) NOT NULL,
    [InstalmentValue]    NUMERIC (18, 3) NULL,
    [WaivedPercentage]   NUMERIC (18, 3) NULL,
    [DateCreated]        DATETIME        NOT NULL,
    [DateModified]       DATETIME        NULL,
    [Deleted]            BIT             NOT NULL,
    CONSTRAINT [PK_FinancialObligationDetail] PRIMARY KEY CLUSTERED ([ObligationDetailID] ASC),
    CONSTRAINT [FK_FinancialObligationDetail_FinancialObligation] FOREIGN KEY ([ObligationID]) REFERENCES [dbo].[FinancialObligation] ([ObligationID])
);

