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
-- Author:		Jango (ChengZhengguo)
-- Create date: 2015Äê9ÔÂ9ÈÕ14:22:20
-- Description:	<Description,,>
-- =============================================
IF (OBJECT_ID('Proc_InsertUser', 'P') IS NOT NULL)
    DROP  PROC Proc_InsertUser
GO
CREATE PROCEDURE Proc_InsertUser 
	-- Add the parameters for the stored procedure here
	@UserName NVARCHAR(50), 
	@UserPWD NVARCHAR(50),
	@Email NVARCHAR(100),
	@Mobile NVARCHAR(15)
	--,@Id BIGINT OUTPUT

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO [dbo].[User]
           ([UserName]
           ,[UserPWD]
           ,[Email]
           ,[Mobile])
		   OUTPUT Inserted.ID
     VALUES
           (@UserName
           ,@UserPWD
           ,@Email
           ,@Mobile);

	--SELECT @Id=@@IDENTITY

END
GO
