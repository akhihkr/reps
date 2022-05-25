CREATE TABLE [dbo].[CurrencyType] (
    [CurrencyTypeID] INT          NOT NULL,
    [CurrencyName]   VARCHAR (50) NOT NULL,
    [CurrencyCode]   VARCHAR (10) NOT NULL,
    [DateCreated]    DATETIME     NOT NULL,
    [DateModified]   DATETIME     NULL,
    [Deleted]        BIT          NOT NULL,
    CONSTRAINT [PK_CurrencyType] PRIMARY KEY CLUSTERED ([CurrencyTypeID] ASC)
);

