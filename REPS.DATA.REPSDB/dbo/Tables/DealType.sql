CREATE TABLE [dbo].[DealType] (
    [DealTypeID]   INT          NOT NULL,
    [Description]  VARCHAR (50) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [DateModified] DATETIME     NULL,
    [Deleted]      BIT          NOT NULL,
    CONSTRAINT [PK_DealType] PRIMARY KEY CLUSTERED ([DealTypeID] ASC)
);

