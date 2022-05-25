CREATE TABLE [dbo].[ParticipantBankDetail] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [ParticipantID] INT           NOT NULL,
    [BankID]        INT           NOT NULL,
    [AccountTypeID] INT           NOT NULL,
    [AccountName]   VARCHAR (100) NOT NULL,
    [AccountNumber] NUMERIC (18)  NOT NULL,
    [SortCode]      NUMERIC (18)  NOT NULL,
    [Verified]      BIT           NOT NULL,
    [Deleted]       BIT           DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ParticipantBankDetail] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ParticipantBankDetail_AccountType] FOREIGN KEY ([AccountTypeID]) REFERENCES [dbo].[AccountType] ([AccountTypeID]),
    CONSTRAINT [FK_ParticipantBankDetail_BankorCreditUnion] FOREIGN KEY ([BankID]) REFERENCES [dbo].[BankorCreditUnion] ([BankID]),
    CONSTRAINT [FK_ParticipantBankDetail_Participant] FOREIGN KEY ([ParticipantID]) REFERENCES [dbo].[Participant] ([ParticipantID])
);

