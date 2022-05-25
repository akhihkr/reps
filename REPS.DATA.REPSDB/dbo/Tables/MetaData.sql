CREATE TABLE [dbo].[MetaData] (
    [MetadataID]   BIGINT        NOT NULL,
    [KeyName]      VARCHAR (100) NOT NULL,
    [ForeignKey]   INT           NOT NULL,
    [Type]         VARCHAR (50)  NOT NULL,
    [Value]        VARCHAR (MAX) NOT NULL,
    [DateCreated]  DATETIME      NOT NULL,
    [DateModified] DATETIME      NULL,
    [Deleted]      BIT           NOT NULL,
    CONSTRAINT [PK_MetaData] PRIMARY KEY CLUSTERED ([MetadataID] ASC)
);

