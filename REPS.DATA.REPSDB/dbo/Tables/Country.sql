CREATE TABLE [dbo].[Country] (
    [CountryID]    INT           IDENTITY (1, 1) NOT NULL,
    [ISOCode]      VARCHAR (10)  NOT NULL,
    [Description]  VARCHAR (50)  NOT NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [DateModified] DATETIME      NULL,
    [Deleted]      BIT           NOT NULL,
    [RegistrarApi] VARCHAR (300) NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([CountryID] ASC)
);

