CREATE TABLE [dbo].[Participant] (
    [ParticipantID]     INT      IDENTITY (1, 1) NOT NULL,
    [DealID]            INT      NOT NULL,
    [ParticipantTypeID] INT      NOT NULL,
    [ParticipantRoleID] INT      NULL,
    [PersonID]          INT      NULL,
    [OrganizationID]    INT      NULL,
    [BankID]            INT      NULL,
    [DateCreated]       DATETIME NOT NULL,
    [DateModified]      DATETIME NULL,
    [Deleted]           BIT      NOT NULL,
    [ApprovedDate]      DATETIME NULL,
    [ParticipantGUID] UNIQUEIDENTIFIER NULL, 
    [EntityID] INT NULL, 
    CONSTRAINT [PK_Participant] PRIMARY KEY CLUSTERED ([ParticipantID] ASC),
    CONSTRAINT [FK_Participant_BankorCreditUnion] FOREIGN KEY ([BankID]) REFERENCES [dbo].[BankorCreditUnion] ([BankID]),
    CONSTRAINT [FK_Participant_Deal] FOREIGN KEY ([DealID]) REFERENCES [dbo].[Deal] ([DealID]),
    CONSTRAINT [FK_Participant_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [dbo].[Organization] ([OrganizationID]),
    CONSTRAINT [FK_Participant_ParticipantRole] FOREIGN KEY ([ParticipantRoleID]) REFERENCES [dbo].[ParticipantRole] ([ParticipantRoleID]),
    CONSTRAINT [FK_Participant_ParticipantType] FOREIGN KEY ([ParticipantTypeID]) REFERENCES [dbo].[ParticipantType] ([ParticipantTypeID])
);

