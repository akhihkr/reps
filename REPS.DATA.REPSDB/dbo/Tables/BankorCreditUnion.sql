CREATE TABLE [dbo].[BankorCreditUnion] (
    [BankID]            INT          IDENTITY (1, 1) NOT NULL,
    [EntityID]          INT          NOT NULL,
    [BankName]          VARCHAR (50) NOT NULL,
    [UniversalSortCode] VARCHAR (50) NOT NULL,
    [SwiftCode]         VARCHAR (30) NOT NULL,
    [DateCreated]       DATETIME     NOT NULL,
    [DateModified]      DATETIME     NULL,
    [Deleted]           BIT          NOT NULL,
    CONSTRAINT [PK_BankorCreditUnion] PRIMARY KEY CLUSTERED ([BankID] ASC),
    CONSTRAINT [FK_BankorCreditUnion_Entity] FOREIGN KEY ([EntityID]) REFERENCES [dbo].[Entity] ([EntityID])
);

