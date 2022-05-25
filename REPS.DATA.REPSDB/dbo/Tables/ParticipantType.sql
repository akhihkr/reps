CREATE TABLE [dbo].[ParticipantType] (
    [ParticipantTypeID] INT          NOT NULL,
    [Description]       VARCHAR (50) NOT NULL,
    [DateCreated]       DATETIME     NOT NULL,
    [DateModified]      DATETIME     NULL,
    [Deleted]           BIT          NOT NULL,
    CONSTRAINT [PK_ParticipantType] PRIMARY KEY CLUSTERED ([ParticipantTypeID] ASC)
);

