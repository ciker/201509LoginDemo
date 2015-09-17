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
-- Author:		Jango (chengzhengguo)
-- Create date: 2015Äê9ÔÂ8ÈÕ18:45:55
-- Description:	<Description,,>
-- =============================================
IF (OBJECT_ID('ProcGetDataPagerList', 'P') IS NOT NULL)
    DROP  PROC ProcGetDataPagerList
GO
CREATE PROCEDURE ProcGetDataPagerList 
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
	
	DECLARE @SQL VARCHAR(5000)
	DECLARE @SQLCOUNT NVARCHAR(4000)
	DECLARE @STARTROW NVARCHAR(100)
	DECLARE @ENDROW  NVARCHAR(100)

	SET NOCOUNT ON;
	
	IF @PageIndex <1
	SET @PageIndex =1

	IF ISNULL(@TableName,'')='' OR @PageSize <0 OR @PageIndex <1 OR ISNULL(@Fields,'')=''
	BEGIN 
	PRINT 'TABLE IS NULL OR PAGESIZE <0 OR PAGEINDEX <0 '
		RETURN
	END
	IF ISNULL(@Orderby,'')=''
	SET @Orderby ='ORDER BY Id DESC '

	SET @STARTROW = (@PageIndex-1)* @PageSize+1
	SET @ENDROW =CONVERT(INT,@STARTROW)+@PageSize -1
	declare @PageSizestr varchar(100);
	set @PageSizestr =@PageSize;

	SET @SQL = 'SELECT * FROM (SELECT  ROW_NUMBER() OVER('+@Orderby+' ) AS NUMBER ,'+@Fields +' FROM '+@TableName +'  WHERE 1 = 1 '
	SET @SQLCOUNT = 'SELECT @Total= COUNT(1) ,@Pages=CEILING((COUNT(1)+0.0)/convert(int,'+@PageSizestr+'))  FROM ' + @TableName +' WHERE 1 =1  '
	
	PRINT @STARTROW;
	PRINT @ENDROW

	SET @SQL =@SQL+@Condition +')AS T WHERE T.NUMBER BETWEEN  '+@STARTROW+'  AND  '+@ENDROW+'' ;
	SET @SQLCOUNT =@SQLCOUNT+@Condition
	

	BEGIN 
		PRINT @SQLCOUNT;
		EXEC SP_EXECUTESQL @SQLCOUNT ,N'@Total INT OUTPUT,@Pages INT OUTPUT', @Total OUTPUT,@Pages OUTPUT
		--EXEC(@SQLCOUNT)
	END
	PRINT @SQL;
	EXEC(@SQL);

END
GO
