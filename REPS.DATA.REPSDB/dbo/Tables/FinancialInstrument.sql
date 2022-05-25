CREATE TABLE [dbo].[FinancialInstrument] (
    [InstrumentID]     INT             IDENTITY (1, 1) NOT NULL,
    [ParticipantID]    INT             NULL,
    [CurrencyTypeID]   INT             NULL,
    [Value]            NUMERIC (18, 3) NULL,
    [Deposit]          NUMERIC (18, 3) NULL,
    [InterestRate]     NUMERIC (18, 3) NULL,
    [Term]             VARCHAR (MAX)   NULL,
    [LenderID]         INT             NOT NULL,
    [InstrumentTypeID] INT             NOT NULL,
    [InterestTypeID]   INT             NOT NULL,
    [DateCreated]      DATETIME        NULL,
    [DateModified]     DATETIME        NULL,
    [Deleted]          BIT             NULL,
    [InstrumentGUID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_FinancialInstrument] PRIMARY KEY CLUSTERED ([InstrumentID] ASC),
    CONSTRAINT [FK_FinancialInstrument_InstrumentType] FOREIGN KEY ([InstrumentTypeID]) REFERENCES [dbo].[InstrumentType] ([InstrumentTypeID]),
    CONSTRAINT [FK_FinancialInstrument_InterestType] FOREIGN KEY ([InterestTypeID]) REFERENCES [dbo].[InterestType] ([InterestTypeID]),
    CONSTRAINT [FK_FinancialInstrument_Lenders] FOREIGN KEY ([LenderID]) REFERENCES [dbo].[Lenders] ([ID])
);

