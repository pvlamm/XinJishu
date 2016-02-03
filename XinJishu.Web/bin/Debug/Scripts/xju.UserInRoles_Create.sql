DECLARE @schema varchar(3);
DECLARE @table varchar(25);

SELECT @schema = 'xju', @table = 'UserInRoles'

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = @schema
                 AND  TABLE_NAME = @table))
BEGIN

	DECLARE @sql NVARCHAR(4000) = N'CREATE TABLE ' + @schema + '.' + @table + '(
		id INT PRIMARY KEY IDENTITY(1,1)
		,userId INT NOT NULL
		,roleId INT NOT NULL
		,createDate DATETIME2 NOT NULL DEFAULT GETDATE()
	)';

	EXEC sp_executesql @sql

END