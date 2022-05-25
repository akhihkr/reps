CREATE TABLE [dbo].[BillingDiscount] (
    [BillingDiscountID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [UserID]            INT             NOT NULL,
    [BillingRateID]     INT             NOT NULL,
    [DiscountReference] VARCHAR (MAX)   NOT NULL,
    [DiscountTypeID]    INT             NOT NULL,
    [DiscountValue]     NUMERIC (18, 3) NOT NULL,
    [DateCreated]       DATETIME        NOT NULL,
    [DateModified]      DATETIME        NULL,
    [Deleted]           BIT             NOT NULL,
    CONSTRAINT [PK_BillingDiscount] PRIMARY KEY CLUSTERED ([BillingDiscountID] ASC),
    CONSTRAINT [FK_BillingDiscount_DiscountType] FOREIGN KEY ([DiscountTypeID]) REFERENCES [dbo].[DiscountType] ([DiscountTypeID])
);

