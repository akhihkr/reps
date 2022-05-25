CREATE TABLE [dbo].[InstrumentType] (
    [InstrumentTypeID] INT          NOT NULL,
    [Description]      VARCHAR (50) NOT NULL,
    [DateCreated]      DATETIME     NOT NULL,
    [DateModified]     DATETIME     NULL,
    [Deleted]          BIT          NOT NULL,
    CONSTRAINT [PK_InstrumentType] PRIMARY KEY CLUSTERED ([InstrumentTypeID] ASC)
);

