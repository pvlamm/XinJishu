
DROP TABLE [dbo].[Email_Messages]

------ Table Creation  of basic Sent Email Needs
CREATE TABLE [dbo].[Email_Messages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1)
	,[public_id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID() 
    ,[to] NVARCHAR(255) NOT NULL
    ,[cc] NVARCHAR(255) NULL
    ,[bcc] NVARCHAR(255) NULL
    ,[from] NVARCHAR(255) NOT NULL
    ,[subject] NVARCHAR(128) NOT NULL
    ,[body] NVARCHAR(MAX) NOT NULL
    ,[create_date] DATETIME2 NOT NULL DEFAULT GETDATE()
	,[update_date] DATETIME2 NOT NULL DEFAULT GETDATE()
    ,[sent_date] DATETIME2 NULL
    ,[status] VARCHAR(4) NOT NULL

)

GO

CREATE PROCEDURE [dbo].[Email_Messages_Insert]
    @to NVARCHAR(255) 
    ,@cc NVARCHAR(255) 
    ,@bcc NVARCHAR(255)
    ,@from NVARCHAR(255)
    ,@subject NVARCHAR(128)
    ,@body NVARCHAR(MAX) 
    ,@sent_date DATETIME2 
    ,@status VARCHAR(4) 
AS
	
	INSERT INTO
		Email_Messages
		([to], [cc], [bcc], [from], [subject], [body], [sent_date], [status])
	VALUES
		(@to, @cc, @bcc, @from, @subject, @body, @sent_date, @status);

	SELECT SCOPE_IDENTITY();

RETURN 0

GO

CREATE PROCEDURE [dbo].[Email_Messages_UpdateById]
	@id INT
    ,@to NVARCHAR(255) 
    ,@cc NVARCHAR(255) 
    ,@bcc NVARCHAR(255)
    ,@from NVARCHAR(255)
    ,@subject NVARCHAR(128)
    ,@body NVARCHAR(MAX) 
    ,@sent_date DATETIME2 
    ,@status VARCHAR(4) 
AS
	
	UPDATE
		Email_Messages
	SET
		[to] = @to
		,[cc] = @cc
		,[bcc] = @bcc
		,[from] = @from
		,[subject] = @subject
		,[body] = @body
		,[sent_date] = @sent_Date
		,[status] = @status
		,[update_date] = GETDATE()
	WHERE
		id = @id


RETURN 0

GO

CREATE PROCEDURE [dbo].[Email_Messages_UpdateByPublicId]
	@public_id uniqueidentifier
    ,@to NVARCHAR(255) 
    ,@cc NVARCHAR(255) 
    ,@bcc NVARCHAR(255)
    ,@from NVARCHAR(255)
    ,@subject NVARCHAR(128)
    ,@body NVARCHAR(MAX) 
    ,@sent_date DATETIME2 
    ,@status VARCHAR(4) 
AS
	
	UPDATE
		Email_Messages
	SET
		[to] = @to
		,[cc] = @cc
		,[bcc] = @bcc
		,[from] = @from
		,[subject] = @subject
		,[body] = @body
		,[sent_date] = @sent_Date
		,[status] = @status
		,[update_date] = GETDATE()
	WHERE
		public_id = @public_id


RETURN 0

GO

CREATE PROCEDURE [dbo].[Email_Messages_UpdateStatusById]
	@id INT
	,@sent_date datetime2(7)
    ,@status VARCHAR(4) 
AS
	
	UPDATE
		Email_Messages
	SET
		[sent_date] = @sent_Date
		,[status] = @status
		,[update_date] = GETDATE()
	WHERE
		id = @id

RETURN 0

GO

CREATE PROCEDURE [dbo].[Email_Messages_UpdateStatusByPublicId]
	@public_id uniqueidentifier
	,@sent_date datetime2(7)
    ,@status VARCHAR(4) 
AS
	
	UPDATE
		Email_Messages
	SET
		[sent_date] = @sent_Date
		,[status] = @status
		,[update_date] = GETDATE()
	WHERE
		public_id = @public_id

RETURN 0

GO

CREATE PROCEDURE [dbo].[Email_Messages_SelectById]
	@id INT
AS
	
	SELECT
		id
		,public_id
		,[to]
		,[cc]
		,[bcc]
		,[from]
		,[subject]
		,[body]
		,[create_date]
		,[update_date]
		,[sent_date]
		,[status]
	FROM
		Email_Messages
	WHERE
		id = @id

RETURN 0

GO

CREATE PROCEDURE [dbo].[Email_Messages_SelectByPublicId]
	@public_id uniqueidentifier
AS
	
	SELECT
		id
		,public_id
		,[to]
		,[cc]
		,[bcc]
		,[from]
		,[subject]
		,[body]
		,[create_date]
		,[update_date]
		,[sent_date]
		,[status]
	FROM
		Email_Messages
	WHERE
		public_id = @public_id

RETURN 0

GO

CREATE PROCEDURE [dbo].[Email_Messages_SelectByStatus]
	@status varchar(4)
AS
	
	SELECT
		id
		,public_id
		,[to]
		,[cc]
		,[bcc]
		,[from]
		,[subject]
		,[body]
		,[create_date]
		,[update_date]
		,[sent_date]
		,[status]
	FROM
		Email_Messages
	WHERE
		[status] = @status
	ORDER BY 
		update_date DESC
		,id DESC

RETURN 0

GO
