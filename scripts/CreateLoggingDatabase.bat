sqlcmd -S (local) -i %~dp0\CreateLoggingDatabase.sql
sqlcmd -S (local) -i %~dp0\CreateLoggingDatabaseObjects.sql