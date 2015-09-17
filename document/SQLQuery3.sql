INSERT INTO [dbo].[User]
                               ([UserName]
                               ,[UserPWD]
                               ,[Email]
                               ,[Mobile])
                               VALUES(123,123,123,123);
							    select SCOPE_IDENTITY()
                                --SELECT @@identity


exec ProcGetDataPagerList @Condition=N'',@Fields=N'*',@Orderby=N'',@PageIndex=0,@Pages=0,@PageSize=10,@TableName=N'[User]',@Total=0
