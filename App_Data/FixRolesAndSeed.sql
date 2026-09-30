-- Aarambha: run on your EXISTING AarambhaDB (safe to re-run, drops nothing).
-- 1) Adds the missing Roles rows (their absence made Sign Up fail)
-- 2) Creates a Teacher account -> username: teacher1  password: Teacher@123
-- 3) Adds sample subjects
USE AarambhaDB;
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE RoleID = 1)
BEGIN
    SET IDENTITY_INSERT dbo.Roles ON;
    INSERT INTO dbo.Roles (RoleID, RoleName, Description) VALUES (1, 'Teacher', 'Teacher/Admin role'), (2, 'Student', 'Student role');
    SET IDENTITY_INSERT dbo.Roles OFF;
END
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Username = 'teacher1')
    INSERT INTO dbo.Users (FullName, Username, Email, PasswordHash, PasswordSalt, RoleID, IsActive)
    VALUES ('Class Teacher', 'teacher1', 'teacher@sanskar.edu', '527bd5ec18e044aefe38f8dbc201b50721b8cd5ceb48a713fc0436ccbd6d7464', 'a1b2c3d4e5f60718293a4b5c6d7e8f90', 1, 1);
IF NOT EXISTS (SELECT 1 FROM dbo.Subjects)
    INSERT INTO dbo.Subjects (SubjectName, Description, IsActive) VALUES
    ('Mathematics', 'Grade 8 Mathematics', 1), ('Science', 'Grade 8 General Science', 1), ('English', 'Grade 8 English language and literature', 1);
GO
-- ONLY if the site says "Login failed for user 'aarambha_app'" (needs SQL Server mixed-mode authentication):
-- USE master; CREATE LOGIN aarambha_app WITH PASSWORD = 'Aarambha@2026', CHECK_POLICY = OFF;
-- USE AarambhaDB; CREATE USER aarambha_app FOR LOGIN aarambha_app; ALTER ROLE db_owner ADD MEMBER aarambha_app;
