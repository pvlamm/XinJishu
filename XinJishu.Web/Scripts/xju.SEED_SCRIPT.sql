
--- ROLE SEED SCRIPT FOR BASIC USERS

TRUNCATE TABLE [xju].[Role]
GO

INSERT INTO [xju].[Role] (publicId, name, createDate, companyId)
	VALUES(NEWID(), 'System Administrator', GETDATE(), 0 )
INSERT INTO [xju].[Role] (publicId, name, createDate, companyId)
	VALUES(NEWID(), 'System User', GETDATE(), 0 )
INSERT INTO [xju].[Role] (publicId, name, createDate, companyId)
	VALUES(NEWID(), 'Company Administrator', GETDATE(), 0 )
INSERT INTO [xju].[Role] (publicId, name, createDate, companyId)
	VALUES(NEWID(), 'Company User', GETDATE(), 0 )
GO
