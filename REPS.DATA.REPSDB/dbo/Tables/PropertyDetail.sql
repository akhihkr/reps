CREATE TABLE [dbo].[PropertyDetail] (
    [PropertyDetailID]     INT           IDENTITY (1, 1) NOT NULL,
    [PropertyID]           INT           NOT NULL,
    [RightTypeID]          INT           NULL,
    [PropertyNumber]       VARCHAR (100) NULL,
    [PortionNumber]        VARCHAR (100) NULL,
    [Township]             VARCHAR (MAX) NULL,
    [PropertyName]         VARCHAR (80)  NULL,
    [RegistrationDivision] VARCHAR (MAX) NULL,
    [SectionNumber]        VARCHAR (20)  NULL,
    [PlanNumber]           VARCHAR (20)  NULL,
    [UnitNumber]           INT           NULL,
    [SizeTypeID]           INT           NOT NULL,
    [Size]                 VARCHAR (100) NULL,
    [Geo]                  VARCHAR (100) NOT NULL,
    [DateCreated]          DATETIME      NOT NULL,
    [DateModified]         DATETIME      NULL,
    [Deleted]              BIT           NOT NULL,
    [PropertyDetailGUID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [PK_PropertyDetail] PRIMARY KEY CLUSTERED ([PropertyDetailID] ASC),
    CONSTRAINT [FK_PropertyDetail_Property] FOREIGN KEY ([PropertyID]) REFERENCES [dbo].[Property] ([PropertyID]),
    CONSTRAINT [FK_PropertyDetail_SizeType] FOREIGN KEY ([SizeTypeID]) REFERENCES [dbo].[SizeType] ([SizeTypeID])
);

