 DBCC DROPCLEANBUFFERS
set statistics io on;
set statistics time on;
SET STATISTICS PROFILE ON;

SELECT 
		                     U.ID
		                    ,U.ACCOUNT
                            ,U.PASSWORD
		                    ,U.NICKNAME
		                    ,U.GENDER
		                    ,U.COMPANYNAME
		                    ,U.ADDRESS
		                    ,U.REMARK
		
		                    FROM USERINFO AS U
			                    INNER JOIN USERINFO_ACCOUNTTYPE_MAPPING AS UM
			                    ON U.ID = UM.USERINFO_ID
                                WHERE 1 = 1  
		                     ORDER BY Id DESC  OFFSET  0 ROWS  FETCH NEXT 10 ROWS ONLY;
							  --SELECT  COUNT(1) AS Total ,CEILING((COUNT(1)+0.0)/10) AS Pages FROM USERINFO WITH(INDEX(PK_USER_USERID)) where ID>0 --WHERE 1 =1  ;

							  --select ID from USERINFO with(index(PK_USER_USERID))

set statistics io off;
set statistics time off;
SET STATISTICS PROFILE off;

 

--select count(1) from UserInfo_AccountType_Mapping