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
--( SUM([Source]) FOR [Subject] IN ( [数学],[英语],[语文] ) ) AS pvt
( count([Source]) FOR [Subject] IN ( [数学],[英语],[语文] ) ) AS pvt
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


--PIVOT 行转列
SELECT * FROM (

	SELECT A.NAME,A.ID AS [DID] FROM SYSCOLUMNS A INNER JOIN SYSOBJECTS B ON A.ID= B.ID AND B.XTYPE='U' AND B.[NAME] = 'USER'
	) P 
PIVOT(
	--MAX([DID]) FOR [NAME] IN ([Id],[Email],[Mobile],[UserName],[UserPWD],[DataStatus],[CreateDateTime],[UpdateDateTime])) AS PVT
	SUM([DID]) FOR [NAME] IN ([Id],[Email],[Mobile],[UserName],[UserPWD],[DataStatus],[CreateDateTime],[UpdateDateTime])) AS PVT

SELECT * FROM (
	
SELECT OBJ.ID AS OID, OBJ.NAME as [TABLENAME],A.NAME AS [INDEXNAME] FROM SYS.INDEXES A INNER JOIN SYSOBJECTS OBJ ON A.OBJECT_ID= OBJ.ID AND OBJ.XTYPE='U'  
	) P
	PIVOT(
		SUM(OID) FOR [TABLENAME] IN ([USER],[USERINFO],[USERINFOACCOUNT])
	)AS PVT



SELECT * FROM (
	SELECT OBJ.ID AS OID, OBJ.NAME AS [TABLENAME],A.NAME AS [INDEXNAME] FROM SYS.INDEXES A INNER JOIN SYSOBJECTS OBJ ON A.OBJECT_ID= OBJ.ID AND OBJ.XTYPE='U'  
	) P
	PIVOT(
	SUM(OID) FOR [TABLENAME] IN ([USER],[USERINFO],[USERINFOACCOUNT])
	)AS PVT
	


--UNPIVOT 将与 PIVOT 执行几乎完全相反的操作，将列转换为行


--SELECT PName,电话类型,电话号码  
--FROM  tb_Tel  
--UNPIVOT(电话类型 FOR 电话号码 IN (Mobile1,Mobile2,Mobile3) ) p  
  

IF NOT OBJECT_ID('tb_Tel') IS NULL  
    DROP TABLE [tb_Tel]  
  
CREATE TABLE [dbo].[tb_Tel](  
----[PKID] int primary key identity(101,1),  
    [PName] [Nvarchar](20) NOT NULL,  
    [Mobile1] [Nvarchar](20) NOT NULL,  
    [Mobile2] [Nvarchar](20) NOT NULL,  
    [Mobile3] [Nvarchar](20) Not Null  
    )  
GO  
INSERT [dbo].[tb_Tel]   
SELECT '胡一刀','13067894562','13567889667','16767894562'  
union ALL SELECT '苗人凤','1507894562','15267889667','15367894562'  
union ALL SELECT '郑希来','18067894562','18567889667','18767894562'  
GO  

SELECT * FROM tb_Tel  

SELECT PName,电话类型,电话号码  
FROM  tb_Tel  
UNPIVOT(电话类型 FOR 电话号码 IN (Mobile1,Mobile2,Mobile3) ) p  


SELECT * FROM (
SELECT PVT.[INDEXNAME] AS INM,PVT.[USER], PVT.[USERINFO], PVT.[USERINFOACCOUNT] FROM (
	SELECT OBJ.ID AS OID, OBJ.NAME AS [TABLENAME],A.NAME AS [INDEXNAME] FROM SYS.INDEXES A INNER JOIN SYSOBJECTS OBJ ON A.OBJECT_ID= OBJ.ID AND OBJ.XTYPE='U'  
	) P
	PIVOT(
	SUM(OID) FOR [TABLENAME] IN ([USER],[USERINFO],[USERINFOACCOUNT])
	)AS PVT
)A
UNPIVOT(
	OID FOR [TABLENAME] IN([USER],[USERINFO],[USERINFOACCOUNT])
)AS UNPVT
ORDER BY UNPVT.TABLENAME 

SELECT OBJ.ID AS OID, OBJ.NAME AS [TABLENAME],A.NAME AS [INDEXNAME] FROM SYS.INDEXES A INNER JOIN SYSOBJECTS OBJ ON A.OBJECT_ID= OBJ.ID AND OBJ.XTYPE='U' 

	