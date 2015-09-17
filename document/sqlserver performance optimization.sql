DBCC HELP('?') 
DBCC help('CHECKIDENT')
dbcc CHECKIDENT('user')
dbcc help('CHECKTABLE')
dbcc CHECKTABLE('user')

dbcc help('show_statistics')
DBCC SHOW_STATISTICS('user','PK_User')

select * from sys.dm_os_wait_stats

select cast(object_name(object_id) as varchar(20)) as 'table', * from sys.stats where  object_id = object_id('user')  


select * from sys.dm_exec_query_stats

select * from sys.dm_exec_text_query_plan(0x05000500AB59313F1055E8710200000001000000000000000000000000000000000000000000000000000000,0,-1)


SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
SELECT                                                     
    DB_NAME() AS DatbaseName 
    , SCHEMA_NAME(o.Schema_ID) AS SchemaName 
    , OBJECT_NAME(s.[object_id]) AS TableName 
    , i.name AS IndexName 
    , ROUND(s.avg_fragmentation_in_percent,2) AS [Fragmentation %] 
INTO #TempFragmentation 
FROM sys.dm_db_index_physical_stats(db_id(),null, null, null, null) s 
INNER JOIN sys.indexes i ON s.[object_id] = i.[object_id] 
    AND s.index_id = i.index_id 
INNER JOIN sys.objects o ON i.object_id = O.object_id    
WHERE 1=2 
EXEC sp_MSForEachDB 'USE [?];                                
INSERT INTO #TempFragmentation 
SELECT TOP 20 
    DB_NAME() AS DatbaseName 
    , SCHEMA_NAME(o.Schema_ID) AS SchemaName 
    , OBJECT_NAME(s.[object_id]) AS TableName 
    , i.name AS IndexName 
    , ROUND(s.avg_fragmentation_in_percent,2) AS [Fragmentation %] 
FROM sys.dm_db_index_physical_stats(db_id(),null, null, null, null) s 
INNER JOIN sys.indexes i ON s.[object_id] = i.[object_id] 
    AND s.index_id = i.index_id 
INNER JOIN sys.objects o ON i.object_id = O.object_id    
WHERE s.database_id = DB_ID() 
  AND i.name IS NOT NULL 
  AND OBJECTPROPERTY(s.[object_id], ''IsMsShipped'') = 0 
ORDER BY [Fragmentation %] DESC'                          
SELECT top 20 * FROM #TempFragmentation ORDER BY [Fragmentation %] DESC 
DROP TABLE #TempFragmentation
