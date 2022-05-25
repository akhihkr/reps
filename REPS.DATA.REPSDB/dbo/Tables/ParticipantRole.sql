CREATE TABLE [dbo].[ParticipantRole] (
    [ParticipantRoleID] INT          NOT NULL,
    [Description]       VARCHAR (50) NOT NULL,
    [Deleted]           BIT          CONSTRAINT [DF_ParticipantRole_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ParticipantRole] PRIMARY KEY CLUSTERED ([ParticipantRoleID] ASC)
);

