CREATE TABLE [dbo].[MessageStatus] (
    [MessageStatusID] INT          IDENTITY (1, 1) NOT NULL,
    [Description]     VARCHAR (50) NOT NULL,
    [DateCreated]     DATETIME     NOT NULL,
    [DateModified]    DATETIME     NULL,
    [Deleted]         BIT          NOT NULL,
    CONSTRAINT [PK_MessageStatus] PRIMARY KEY CLUSTERED ([MessageStatusID] ASC)
);

