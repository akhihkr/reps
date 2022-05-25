CREATE TABLE [dbo].[Lenders] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [AddressLine1]  VARCHAR (100) NULL,
    [AddressLine2]  VARCHAR (100) NULL,
    [AddressLine3]  VARCHAR (100) NULL,
    [AddressLine4]  VARCHAR (100) NULL,
    [PostalCode]    VARCHAR (50)  NULL,
    [ContactNumber] VARCHAR (50)  NULL,
    [website]       VARCHAR (100) NULL,
    [email]         VARCHAR (100) NULL,
    CONSTRAINT [PK_Lenders] PRIMARY KEY CLUSTERED ([ID] ASC)
);

