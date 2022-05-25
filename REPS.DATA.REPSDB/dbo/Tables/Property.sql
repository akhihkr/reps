CREATE TABLE [dbo].[Property] (
    [PropertyID]          INT            IDENTITY (1, 1) NOT NULL,
    [DealID]              INT            NOT NULL,
    [PropertyDescription] VARCHAR (1000) NOT NULL,
    [LegalDescription]    VARCHAR (1000) NULL,
    [AddressID]           INT            NOT NULL,
    [PropertyTypeID]      INT            NOT NULL,
    [Verified]            BIT            NOT NULL,
    [DateCreated]         DATETIME       NOT NULL,
    [DateModified]        DATETIME       NULL,
    [Deleted]             BIT            NOT NULL,
    [PropertyGUID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_Property] PRIMARY KEY CLUSTERED ([PropertyID] ASC),
    CONSTRAINT [FK_Property_Address] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Address] ([AddressID]),
    CONSTRAINT [FK_Property_Deal] FOREIGN KEY ([DealID]) REFERENCES [dbo].[Deal] ([DealID]),
    CONSTRAINT [FK_Property_PropertyType] FOREIGN KEY ([PropertyTypeID]) REFERENCES [dbo].[PropertyType] ([PropertyTypeID])
);

