CREATE TABLE SourceTable (ID INT, TEXTVal VARCHAR(100))  
CREATE TABLE TargetTable (ID INT, TEXTVal VARCHAR(100))  
GO

TRUNCATE TABLE SourceTable
TRUNCATE table TargetTable

SELECT * FROM SourceTable
SELECT * FROM TargetTable

INSERT SourceTable (ID, TEXTVal)  
OUTPUT Inserted.*
VALUES (1,'FirstVal');  
INSERT SourceTable (ID, TEXTVal)  
OUTPUT Inserted.*
VALUES (2,'SecondVal');  


MERGE INTO TargetTable AS T
USING SourceTable AS S
ON T.ID = S.ID
WHEN MATCHED --源存在,目标存在,则更新
THEN UPDATE SET T.TEXTVAL = S.TEXTVAL+'source_update'
WHEN NOT MATCHED--目标不存在,源存在,则新增
THEN INSERT VALUES(S.ID,S.TEXTVAL+'source_insert')
WHEN NOT MATCHED BY SOURCE  ---目标存在,源不存在,则删除
THEN DELETE
OUTPUT $ACTION AS [ACTION],INSERTED.*,DELETED.*;


DROP  TABLE TargetTable,SourceTable
GO


