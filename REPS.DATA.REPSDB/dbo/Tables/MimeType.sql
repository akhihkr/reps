CREATE TABLE [dbo].[MimeType] (
    [MimeTypeID]   INT          IDENTITY (1, 1) NOT NULL,
    [Extension]    VARCHAR (50) NOT NULL,
    [Description]  VARCHAR (50) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [DateModified] DATETIME     NULL,
    [Deleted]      BIT          NOT NULL,
    CONSTRAINT [PK_MimeType] PRIMARY KEY CLUSTERED ([MimeTypeID] ASC)
);

