CREATE DATABASE LogDatabase;

CREATE TABLE [dbo].[LogModels]
(
    [Id] BIGINT NOT NULL IDENTITY (1,1), 
    [Date] DATETIME NULL, 
    [HostName] NVARCHAR(MAX) NULL, 
    [Route] NVARCHAR(MAX) NULL, 
    [Status] INT NULL, 
    [Params] NVARCHAR(MAX) NULL, 
    [Size] BIGINT NULL, 
    [Country] NVARCHAR(MAX) NULL,
    CONSTRAINT PK_LogModels PRIMARY KEY CLUSTERED (Id)
)

--Reset sequance

DBCC CHECKIDENT (LogModels, RESEED, 0)

--Clear table

DELETE FROM [LogModels];