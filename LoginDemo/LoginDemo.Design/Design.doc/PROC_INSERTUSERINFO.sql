
/***********************************/
/***********************************/
/***********************************/
/*
		author:jango cheng
		date  :2015-9-21 14:33:16
		name  :PROC_INSERTUSERINFO
		desc  : transaction insert userinfo and userinfo_accounttype_mapping 
*/
/***********************************/
/***********************************/
/***********************************/

		IF OBJECT_ID('PROC_INSERTUSERINFO') <>NULL

			DROP PROC PROC_INSERTUSERINFO

		GO

		CREATE PROC PROC_INSERTUSERINFO
			 @ACCOUNT nvarchar(50)
            ,@PASSWORD nvarchar(50)
            ,@NICKNAME nvarchar(30)
            ,@GENDER bit
            ,@COMPANYNAME nvarchar(50)
            ,@ADDRESS nvarchar(100)
            ,@REMARK nvarchar(100)
			,@ACCOUNT_TYPE int
		 AS

		 BEGIN
					DECLARE @USERINFO_TEMP TABLE(
							[ID] BIGINT,
							[ACCOUNT] NVARCHAR(50) NOT NULL,
							[PASSWORD] NVARCHAR(50) NOT NULL,
							[NICKNAME] NVARCHAR(30) NULL,
							[GENDER] BIT NULL,
							[COMPANYNAME] NVARCHAR(50) NULL,
							[ADDRESS] NVARCHAR(100) NULL,
							[REMARK] NVARCHAR(100) NULL)
						DECLARE @USERINFO_ID BIGINT;
							BEGIN TRAN
								BEGIN TRY
									INSERT INTO [dbo].[UserInfo]
									([ACCOUNT]
										,[PASSWORD]
										,[NICKNAME]
										,[GENDER]
										,[COMPANYNAME]
										,[ADDRESS]
										,[REMARK])
										OUTPUT INSERTED.* INTO @USERINFO_TEMP 
										VALUES(
										@ACCOUNT
									,@PASSWORD
									,@NICKNAME
									,@GENDER
									,@COMPANYNAME
									,@ADDRESS
									,@REMARK);
										PRINT @USERINFO_ID;
										SELECT  @USERINFO_ID =ID  FROM @USERINFO_TEMP
										PRINT @USERINFO_ID;
										INSERT INTO [DBO].[UserInfo_AccountType_Mapping] VALUES																																		(@USERINFO_ID,@ACCOUNT_TYPE)
						COMMIT
						SELECT * FROM @USERINFO_TEMP ;
				END TRY
				BEGIN CATCH
					THROW
					ROLLBACK
					RETURN 
				END CATCH
			END
	GO
