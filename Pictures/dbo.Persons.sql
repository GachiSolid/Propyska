CREATE TABLE [dbo].[Persons] (
    [PersonID]   INT           NOT NULL default AUTO_INCREMENT PRIMARY KEY,
    [Surname]    NVARCHAR (50) NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Patronymic] NVARCHAR (50) NOT NULL,
)

