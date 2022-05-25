CREATE TABLE [dbo].[Province] (
    [ProvinceID]   INT          IDENTITY (1, 1) NOT NULL,
    [CountryID]    INT          NOT NULL,
    [Description]  VARCHAR (50) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [DateModified] DATETIME     NULL,
    [Deleted]      BIT          NOT NULL,
    CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED ([ProvinceID] ASC)
);

