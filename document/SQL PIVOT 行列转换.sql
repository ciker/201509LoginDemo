--创建测试表
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TestRows2Columns]') AND type in (N'U'))
DROP TABLE [dbo].[TestRows2Columns]
GO
CREATE TABLE [dbo].[TestRows2Columns](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserName] [nvarchar](50) NULL,
    [Subject] [nvarchar](50) NULL,
    [Source] [numeric](18, 0) NULL
) ON [PRIMARY]
GO

--插入测试数据
INSERT INTO [TestRows2Columns] ([UserName],[Subject],[Source]) 
    SELECT N'张三',N'语文',60  UNION ALL
    SELECT N'李四',N'数学',70  UNION ALL
    SELECT N'王五',N'英语',80  UNION ALL
    SELECT N'王五',N'数学',75  UNION ALL
    SELECT N'王五',N'语文',57  UNION ALL
    SELECT N'李四',N'语文',80  UNION ALL
    SELECT N'张三',N'英语',100
GO

SELECT * FROM [TestRows2Columns]

--1：静态拼接行转列
SELECT [UserName],
SUM(CASE [Subject] WHEN '数学' THEN [Source] ELSE 0 END) AS '[数学]',
SUM(CASE [Subject] WHEN '英语' THEN [Source] ELSE 0 END) AS '[英语]',
SUM(CASE [Subject] WHEN '语文' THEN [Source] ELSE 0 END) AS '[语文]'     
FROM [TestRows2Columns]
GROUP BY [UserName]
GO

--2：动态拼接行转列
DECLARE @sql VARCHAR(8000)
SET @sql = 'SELECT [UserName],'   
SELECT @sql = @sql + 'SUM(CASE [Subject] WHEN '''+[Subject]+''' THEN [Source] ELSE 0 END) AS '''+QUOTENAME([Subject])+''','   
FROM (SELECT DISTINCT [Subject] FROM [TestRows2Columns]) AS a     
SELECT @sql = LEFT(@sql,LEN(@sql)-1) + ' FROM [TestRows2Columns] GROUP BY [UserName]'   
PRINT(@sql)
EXEC(@sql)
GO


--3：静态PIVOT行转列
SELECT  *
FROM    ( SELECT    [UserName] ,
                    [Subject] ,
                    [Source]
          FROM      [TestRows2Columns]
        ) p PIVOT
( SUM([Source]) FOR [Subject] IN ( [数学],[英语],[语文] ) ) AS pvt
ORDER BY pvt.[UserName];
GO

--4：动态PIVOT行转列
DECLARE @sql_str VARCHAR(8000)
DECLARE @sql_col VARCHAR(8000)
SELECT @sql_col = ISNULL(@sql_col + ',','') + QUOTENAME([Subject]) FROM [TestRows2Columns] GROUP BY [Subject]
SET @sql_str = '
SELECT * FROM (
    SELECT [UserName],[Subject],[Source] FROM [TestRows2Columns]) p PIVOT 
    (SUM([Source]) FOR [Subject] IN ( '+ @sql_col +') ) AS pvt 
ORDER BY pvt.[UserName]'
PRINT (@sql_str)
EXEC (@sql_str)