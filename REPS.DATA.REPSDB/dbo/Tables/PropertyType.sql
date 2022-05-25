CREATE TABLE [dbo].[PropertyType] (
    [PropertyTypeID] INT          NOT NULL,
    [Description]    VARCHAR (50) NOT NULL,
    [DateCreated]    DATETIME     NOT NULL,
    [DateModified]   DATETIME     NULL,
    [Deleted]        BIT          NOT NULL,
    CONSTRAINT [PK_PropertyType] PRIMARY KEY CLUSTERED ([PropertyTypeID] ASC)
);

