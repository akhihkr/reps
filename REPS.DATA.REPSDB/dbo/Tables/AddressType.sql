CREATE TABLE [dbo].[AddressType] (
    [AddressTypeID] INT          NOT NULL,
    [Description]   VARCHAR (50) NOT NULL,
    [DateCreated]   DATETIME     NOT NULL,
    [DateModified]  DATETIME     NULL,
    [Deleted]       BIT          NOT NULL,
    CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED ([AddressTypeID] ASC)
);

