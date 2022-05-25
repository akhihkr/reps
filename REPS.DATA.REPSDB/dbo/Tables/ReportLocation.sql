CREATE TABLE [dbo].[ReportLocation] (
    [reportLocationID] INT             NOT NULL,
    [Location]         NVARCHAR (4000) NULL,
    [Deleted]          BIT             DEFAULT ((0)) NULL,
    [Order]            INT             NULL,
    [ReportsId]        INT             NULL,
    [ChartType]        NVARCHAR (500)  NULL,
    [ChartName]        NVARCHAR (500)  NULL,
    PRIMARY KEY CLUSTERED ([reportLocationID] ASC)
);

