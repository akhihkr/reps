CREATE TABLE [dbo].[UserDocument] (
    [DocumentID]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [EntityID]      INT            NOT NULL,
    [UserID]        INT            NOT NULL,
    [ParticipantID] INT            NULL,
    [MimeTypeID]    INT            NOT NULL,
    [DocumentName]  VARCHAR (200)  NOT NULL,
    [FileContent]   VARBINARY (50) NOT NULL,
    [FileHash]      VARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_UserDocument] PRIMARY KEY CLUSTERED ([DocumentID] ASC),
    CONSTRAINT [FK_UserDocument_Entity] FOREIGN KEY ([EntityID]) REFERENCES [dbo].[Entity] ([EntityID]),
    CONSTRAINT [FK_UserDocument_Participant] FOREIGN KEY ([ParticipantID]) REFERENCES [dbo].[Participant] ([ParticipantID]),
    CONSTRAINT [FK_UserDocument_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID]),
    CONSTRAINT [FK_UserDocument_UserDocument] FOREIGN KEY ([MimeTypeID]) REFERENCES [dbo].[MimeType] ([MimeTypeID])
);

