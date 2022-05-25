CREATE TABLE [dbo].[Organization] (
    [OrganizationID]     INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationTypeID] INT           NOT NULL,
    [Name]               VARCHAR (100) NOT NULL,
    [RegistrationNumber] VARCHAR (20)  NOT NULL,
    [LegalName]          VARCHAR (100) NULL,
    [VatID]              VARCHAR (100) NULL,
    [Telephone]          NUMERIC (18)  NOT NULL,
    [FaxNumber]          NUMERIC (18)  NULL,
    [Email]              VARCHAR (200) NULL,
    [Verified]           BIT           NOT NULL,
    [DateCreated]        DATETIME      NOT NULL,
    [DateModified]       DATETIME      NULL,
    [Deleted]            BIT           NOT NULL,
    [EntityID]           INT           NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([OrganizationID] ASC),
    CONSTRAINT [FK_Organization_OrganizationType] FOREIGN KEY ([OrganizationTypeID]) REFERENCES [dbo].[OrganizationType] ([OrganizationTypeID])
);

