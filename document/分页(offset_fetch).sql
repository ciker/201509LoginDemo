-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
IF OBJECT_ID('Proc_DataPagerList','P') IS NOT NULL
	DROP PROC Proc_DataPagerList
GO

CREATE PROCEDURE Proc_DataPagerList 
	-- Add the parameters for the stored procedure here
	@TableName NVARCHAR(50), 
	@Fields NVARCHAR(200),
	@PageIndex INT,
	@PageSize INT,
	@Condition NVARCHAR(500),
	@Orderby NVARCHAR(100),
	@Total INT output,
	@Pages INT output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @SQL NVARCHAR(4000)
	DECLARE @SQLCOUNT NVARCHAR(4000)

	SET NOCOUNT ON;
	
	IF @PageIndex <1
	SET @PageIndex =1;
	IF ISNULL(@Fields,'')=''
	SET @Fields=' * ';
	IF ISNULL(@TableName,'')='' OR @PageSize <0 OR @PageIndex <1 OR ISNULL(@Fields,'')=''
	BEGIN 
	PRINT 'TABLE IS NULL OR PAGESIZE <0 OR PAGEINDEX <0 '
		RETURN
	END
	IF ISNULL(@Orderby,'')=''
	SET @Orderby ='ORDER BY Id DESC '
    -- Insert statements for procedure here
	DECLARE @PageSizestr VARCHAR(100);
	SET @PageSizestr =@PageSize;

	SET @SQL = N'SELECT '+@Fields +' FROM '+@TableName +'  WHERE 1 = 1 '
	SET @SQLCOUNT = N'SELECT @Total= COUNT(1) ,@Pages=CEILING((COUNT(1)+0.0)/@PageSize)  FROM ' + @TableName +' WHERE 1 =1  '
	

	SET @SQL =@SQL+@Condition +@Orderby +' OFFSET (@PageIndex-1)*@PageSize ROWS  FETCH NEXT @PageSize ROWS ONLY; ' ;
	SET @SQLCOUNT =@SQLCOUNT+@Condition
	

	BEGIN 
		PRINT @SQLCOUNT;
		EXEC SP_EXECUTESQL @SQLCOUNT ,N'@PageSize INT,@Total INT OUTPUT,@Pages INT OUTPUT',@PageSize, @Total OUTPUT,@Pages OUTPUT
		--EXEC(@SQLCOUNT)
	END
	BEGIN
		PRINT @SQL;
		EXEC SP_EXECUTESQL @SQL ,N'@PageIndex INT ,@PageSize INT',@PageIndex,@PageSize
		--EXEC(@SQL);
	END
END
GO
