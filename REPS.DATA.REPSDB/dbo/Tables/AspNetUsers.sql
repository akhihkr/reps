﻿CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [BirthDate]            DATETIME       NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
    [UserName]             NVARCHAR (256) NOT NULL,
    [Firstname]            NVARCHAR (MAX) NULL,
    [Lastname]             NVARCHAR (MAX) NULL,
    [sex]                  NVARCHAR (MAX) NULL,
    [Question]             NVARCHAR (MAX) NULL,
    [Answer]               NVARCHAR (MAX) NULL,
    [Discriminator]        NVARCHAR (128) NULL,
    [DateCreated]          DATETIME       NOT NULL,
    [DateModified]         DATETIME       NULL,
    [Deleted]              BIT            NOT NULL,
    [TokenId]              NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);
