CREATE TABLE [dbo].[Reports] (
    [ReportsId]      INT             IDENTITY (1, 1) NOT NULL,
    [Description]    NVARCHAR (50)   NULL,
    [Deleted]        BIT             NULL,
    [TableProcedure] NVARCHAR (50)   NULL,
    [ChartProcedure] NVARCHAR (50)   NULL,
    [TableParameter] NVARCHAR (1000) NULL,
    [ChartParameter] NVARCHAR (1000) NULL,
    CONSTRAINT [PK__Reports] PRIMARY KEY CLUSTERED ([ReportsId] ASC)
);

