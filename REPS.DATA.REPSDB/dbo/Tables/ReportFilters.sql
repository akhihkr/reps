CREATE TABLE [dbo].[ReportFilters] (
    [ReportsFiltersId]  INT             NOT NULL,
    [ReportsId]         INT             NULL,
    [Description]       NVARCHAR (50)   NOT NULL,
    [Type]              NVARCHAR (4)    NOT NULL,
    [DropdownProcedure] NVARCHAR (50)   NULL,
    [Parameter]         NVARCHAR (MAX)  NULL,
    [FilterID]          NVARCHAR (1000) NULL,
    CONSTRAINT [PK__Report] PRIMARY KEY CLUSTERED ([ReportsFiltersId] ASC)
);

