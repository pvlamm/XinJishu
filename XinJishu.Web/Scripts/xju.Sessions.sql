DECLARE @schema varchar(3);
DECLARE @table varchar(25);

SELECT @schema = 'xju', @table = 'Sessions'

IF (NOT EXISTS (SELECT * FROM sys.schemas WHERE name = @schema)) 
BEGIN
    EXEC ('CREATE SCHEMA ' + @schema + ' AUTHORIZATION [dbo]')
END

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = @schema
                 AND  TABLE_NAME = @table))
BEGIN

	DECLARE @sql NVARCHAR(4000) = N'CREATE TABLE ' + @schema + '.' + @table + '(
		id INT PRIMARY KEY IDENTITY(1,1)
		,sessionId UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID()
		,userId INT NOT NULL
		,ipAddress VARCHAR(15) NOT NULL
		,createDate DATETIME2 NOT NULL DEFAULT GETDATE()
		,expirationDate DATETIME2 NOT NULL DEFAULT DATEADD(HOUR, 1, GETDATE())
	)';

	EXEC sp_executesql @sql

END