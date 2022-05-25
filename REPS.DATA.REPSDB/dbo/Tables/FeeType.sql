CREATE TABLE [dbo].[FeeType] (
    [FeeTypeID]   INT             NOT NULL,
    [Description] NVARCHAR (1000) NULL,
    [Deleted]     BIT             DEFAULT ((0)) NULL,
    [DateCreated] DATETIME        DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([FeeTypeID] ASC)
);

