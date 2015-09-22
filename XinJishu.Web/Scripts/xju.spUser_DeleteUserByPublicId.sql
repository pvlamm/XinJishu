DECLARE @schema VARCHAR(3);
DECLARE @procName NVARCHAR(25);

SELECT @schema = 'xju', @procName = 'User_DeleteUserByPublicId'

IF NOT EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(@procName)
					AND SCHEMA_ID = SCHEMA_ID(@schema)
                    AND type IN ( N'P', N'PC' ) ) 
BEGIN

DECLARE @sql NVARCHAR(4000) = N'CREATE PROCEDURE ' + @schema + '.' + @procName + '
	@publicId UNIQUEIDENTIFIER
AS
	DELETE FROM ' + @schema + '.Users WHERE publicId = @publicId
RETURN 0'

exec sp_executesql @sql

END
