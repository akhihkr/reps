CREATE TABLE [dbo].[ReportHeaders] (
    [HeaderId]    INT           NULL,
    [ReportsId]   INT           NULL,
    [Description] NVARCHAR (50) NULL,
    [Size]        INT           NULL,
    [Active]      BIT           NULL,
    [DataRef]     NVARCHAR (50) NULL
);

