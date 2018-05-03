sqlcmd -S (local)\SQL2016 -i %~dp0\CreateLoggingDatabase.sql
sqlcmd -S (local)\SQL2016 -i %~dp0\CreateLoggingDatabaseObjects.sql