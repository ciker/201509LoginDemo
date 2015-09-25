--�������Ա�
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

--�����������
INSERT INTO [TestRows2Columns] ([UserName],[Subject],[Source]) 
    SELECT N'����',N'����',60  UNION ALL
    SELECT N'����',N'��ѧ',70  UNION ALL
    SELECT N'����',N'Ӣ��',80  UNION ALL
    SELECT N'����',N'��ѧ',75  UNION ALL
    SELECT N'����',N'����',57  UNION ALL
    SELECT N'����',N'����',80  UNION ALL
    SELECT N'����',N'Ӣ��',100
GO

SELECT * FROM [TestRows2Columns]

--1����̬ƴ����ת��
SELECT [UserName],
SUM(CASE [Subject] WHEN '��ѧ' THEN [Source] ELSE 0 END) AS '[��ѧ]',
SUM(CASE [Subject] WHEN 'Ӣ��' THEN [Source] ELSE 0 END) AS '[Ӣ��]',
SUM(CASE [Subject] WHEN '����' THEN [Source] ELSE 0 END) AS '[����]'     
FROM [TestRows2Columns]
GROUP BY [UserName]
GO

--2����̬ƴ����ת��
DECLARE @sql VARCHAR(8000)
SET @sql = 'SELECT [UserName],'   
SELECT @sql = @sql + 'SUM(CASE [Subject] WHEN '''+[Subject]+''' THEN [Source] ELSE 0 END) AS '''+QUOTENAME([Subject])+''','   
FROM (SELECT DISTINCT [Subject] FROM [TestRows2Columns]) AS a     
SELECT @sql = LEFT(@sql,LEN(@sql)-1) + ' FROM [TestRows2Columns] GROUP BY [UserName]'   
PRINT(@sql)
EXEC(@sql)
GO


--3����̬PIVOT��ת��
SELECT  *
FROM    ( SELECT    [UserName] ,
                    [Subject] ,
                    [Source]
          FROM      [TestRows2Columns]
        ) p PIVOT
( SUM([Source]) FOR [Subject] IN ( [��ѧ],[Ӣ��],[����] ) ) AS pvt
ORDER BY pvt.[UserName];
GO

--4����̬PIVOT��ת��
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