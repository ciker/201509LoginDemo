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
WHEN MATCHED --Դ����,Ŀ�����,�����
THEN UPDATE SET T.TEXTVAL = S.TEXTVAL+'source_update'
WHEN NOT MATCHED--Ŀ�겻����,Դ����,������
THEN INSERT VALUES(S.ID,S.TEXTVAL+'source_insert')
WHEN NOT MATCHED BY SOURCE  ---Ŀ�����,Դ������,��ɾ��
THEN DELETE
OUTPUT $ACTION AS [ACTION],INSERTED.*,DELETED.*;


DROP  TABLE TargetTable,SourceTable
GO


