CREATE TABLE [dbo].[FinancialObligation] (
    [ObligationID]        INT             IDENTITY (1, 1) NOT NULL,
    [ParticipantID]       INT             NOT NULL,
    [CurrencyID]          INT             NOT NULL,
    [Value]               NUMERIC (18, 3) NOT NULL,
    [LegalDescription]    TEXT            NULL,
    [AccountBalance]      NUMERIC (18, 3) NOT NULL,
    [AccountNumber]       NUMERIC (18)    NOT NULL,
    [InterestVariationID] INT             NOT NULL,
    [InterestTypeID]      INT             NOT NULL,
    [DateCreated]         DATETIME        NOT NULL,
    [DateModified]        DATETIME        NULL,
    [Deleted]             BIT             NOT NULL,
    CONSTRAINT [PK_FinancialObligation] PRIMARY KEY CLUSTERED ([ObligationID] ASC),
    CONSTRAINT [FK_FinancialObligation_FinancialObligation] FOREIGN KEY ([ObligationID]) REFERENCES [dbo].[FinancialObligation] ([ObligationID]),
    CONSTRAINT [FK_FinancialObligation_InterestType] FOREIGN KEY ([InterestTypeID]) REFERENCES [dbo].[InterestType] ([InterestTypeID]),
    CONSTRAINT [FK_FinancialObligation_InterestVariation] FOREIGN KEY ([InterestVariationID]) REFERENCES [dbo].[InterestVariation] ([InterestVariationID]),
    CONSTRAINT [FK_FinancialObligation_Participant] FOREIGN KEY ([ParticipantID]) REFERENCES [dbo].[Participant] ([ParticipantID])
);

