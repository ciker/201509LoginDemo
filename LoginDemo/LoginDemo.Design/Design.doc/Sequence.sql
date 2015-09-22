IF EXISTS(SELECT name FROM sys.sequences WHERE name=N'UserDBSequence')
       DROP SEQUENCE UserDBSequence;
 GO

CREATE SEQUENCE  UserDBSequence
AS BIGINT 
START WITH 0
INCREMENT  BY 1
CYCLE


SELECT NEXT VALUE FOR UserDBSequence


select * from sys.objects where object_id =object_id('UserDBSequence')

select * from sys.sequences where object_id =object_id('UserDBSequence')