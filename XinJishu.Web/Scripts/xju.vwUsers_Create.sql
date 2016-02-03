DECLARE @schema varchar(3);
DECLARE @view varchar(25);
DECLARE @table varchar(25);

SELECT @schema = 'xju', @view = 'vwUsers', @table = 'Users'

IF NOT EXISTS(select * FROM sys.views where name = @view)
BEGIN

	DECLARE @sql NVARCHAR(4000) = N'CREATE VIEW ' + @schema + '.' + @view + '
	AS
		SELECT * FROM ' + @schema + '.' + @table + '
	';

	EXEC sp_executesql @sql

END