-- ===========================================
-- Author:Pravina Barosah
-- Create date: 21 Jan 2017
-- Description: Get report deatails from report location table
-- ===========================================
CREATE PROCEDURE [dbo].[REPS_GetReportName]
		@location varchar(4000) =null,
		@chartName varchar(500) =null,
		@chartType varchar(500) =null,
		@description varchar(50) =null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
			SELECT RN.[ReportsId]
			,[Description]
			,RN.[Deleted]
			,[TableProcedure]
			,[ChartProcedure]
			,[TableParameter]
			,[ChartParameter]
			,RL.[Location] [location]
			,RL.[ChartType]
			,RL.[ChartName]
			,RL.[Order]
			,ROW_NUMBER() OVER (ORDER BY RN.[ReportsId] DESC) AS num
			FROM [dbo].[Reports] RN
			INNER JOIN [dbo].[ReportLocation] RL ON RN.[ReportsId] = RL.[ReportsId]
	  ) 
		As allRec
	  WHERE 
		(	@Start IS NULL OR num>= @Start	)
		AND
		(	@End IS NULL OR num<= @End	)	  	
		AND
		([Deleted] =0 OR [Deleted] IS NULL)
		AND 
		(@location IS NULL OR [location] = @location)
		AND (@chartName IS NULL OR [ChartName] = @chartName)
		AND (@chartType IS NULL OR [ChartType] = @chartType)
		AND (@description IS NULL OR [Description] = @description)
	  ORDER BY [ReportsId] DESC
GO