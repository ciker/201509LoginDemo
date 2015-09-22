IF EXISTS(SELECT name FROM sys.sequences WHERE name=N'UserDBSequence')
       DROP SEQUENCE UserDBSequence;
 GO

CREATE SEQUENCE  UserDBSequence
AS BIGINT 
START WITH 1
INCREMENT  BY 1
CYCLE


SELECT NEXT VALUE FOR UserDBSequence



select * from sys.objects where object_id =object_id('UserDBSequence')

select * from sys.sequences where object_id =object_id('UserDBSequence')


select * from [dbo].[UserInfo]
select * from [dbo].[userinfoaccount]

select * from [UserInfo] a inner join [userinfoaccount] b on a.id = b.userinfoid
select * from [dbo].[userinfoaccount]