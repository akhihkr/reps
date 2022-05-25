-- =============================================
-- Author: Pravina Barosah 
-- Create date: 02 Feb 2017
-- Description:	get report dropdown filters
-- =============================================
CREATE PROCEDURE [dbo].[REPS_GetReportFilters]
		@ReportId  int = null,
		@Start  int = null,
		@End  int = null
AS
	Select * FROM
	(
			SELECT [ReportsFiltersId]
			,[ReportsId]
			,[Description]
			,[Type]
			,[DropdownProcedure]
			,[Parameter]
			,[FilterID]
			,ROW_NUMBER() OVER (ORDER BY [ReportsFiltersId] DESC) AS num
			FROM [dbo].[ReportFilters]
	  ) 
		As allEntities
	  WHERE 
		(	@ReportId IS NULL OR [ReportsId] = @ReportId	)
		AND
		(	@Start IS NULL OR num>= @Start	)
		AND
		(	@End IS NULL OR num<= @End	)	  	

	  ORDER BY [ReportsFiltersId] DESC
GO