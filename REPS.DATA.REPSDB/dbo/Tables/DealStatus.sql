CREATE TABLE [dbo].[DealStatus] (
    [DealStatusID] INT          NOT NULL,
    [Description]  VARCHAR (50) NOT NULL,
    [DateCreated]  DATETIME     NOT NULL,
    [DateModified] DATETIME     NULL,
    [Deleted]      BIT          NOT NULL,
    CONSTRAINT [PK_DealStatus] PRIMARY KEY CLUSTERED ([DealStatusID] ASC)
);

