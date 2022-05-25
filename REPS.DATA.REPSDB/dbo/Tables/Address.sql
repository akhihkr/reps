CREATE TABLE [dbo].[Address] (
    [AddressID]     INT           IDENTITY (1, 1) NOT NULL,
    [ParticipantID] INT           NULL,
    [AddressTypeID] INT           NOT NULL,
    [AddressLine1]  VARCHAR (100) NOT NULL,
    [AddressLine2]  VARCHAR (100) NULL,
    [City]          VARCHAR (30)  NULL,
    [ProvinceID]    INT           NULL,
    [CountryID]     INT           NOT NULL,
    [PostalCode]    VARCHAR (10)  NULL,
    [DateCreated]   DATETIME      NOT NULL,
    [DateModified]  DATETIME      NULL,
    [Deleted]       BIT           NOT NULL,
    [Verified]      BIT           CONSTRAINT [DF_Address_Verified] DEFAULT ((0)) NOT NULL,
    [AddressGUID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressID] ASC),
    CONSTRAINT [FK_Address_AddressType] FOREIGN KEY ([AddressTypeID]) REFERENCES [dbo].[AddressType] ([AddressTypeID]),
    CONSTRAINT [FK_Address_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([CountryID]),
    CONSTRAINT [FK_Address_Province] FOREIGN KEY ([ProvinceID]) REFERENCES [dbo].[Province] ([ProvinceID])
);

