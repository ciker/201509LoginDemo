USE [UserDB]
GO

SELECT [Id]
      ,[UserName]
      ,[UserPWD]
      ,[Email]
      ,[Mobile]
  FROM [dbo].[User]
GO


select COUNT(1),CEILING((COUNT(1)+0.0)/10) FROM [USER]

