CREATE TABLE [dbo].[Township] (
    [Description]  VARCHAR (50) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [DateModified] NCHAR (10)   NULL,
    [Deleted]      BIT          NOT NULL,
    [TownshipID]   INT          IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Township] PRIMARY KEY CLUSTERED ([TownshipID] ASC)
);

