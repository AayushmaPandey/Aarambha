-- Aarambha Database Creation Script
-- Target: SQL Server
-- Database: AarambhaDB

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'AarambhaDB')
BEGIN
	CREATE DATABASE AarambhaDB;
END
GO

USE AarambhaDB;
GO

-- Drop existing tables if they exist (for clean recreation)
IF OBJECT_ID('dbo.QuizAttempts', 'U') IS NOT NULL DROP TABLE dbo.QuizAttempts;
IF OBJECT_ID('dbo.QuestionOptions', 'U') IS NOT NULL DROP TABLE dbo.QuestionOptions;
IF OBJECT_ID('dbo.Questions', 'U') IS NOT NULL DROP TABLE dbo.Questions;
IF OBJECT_ID('dbo.Quizzes', 'U') IS NOT NULL DROP TABLE dbo.Quizzes;
IF OBJECT_ID('dbo.Resources', 'U') IS NOT NULL DROP TABLE dbo.Resources;
IF OBJECT_ID('dbo.Notes', 'U') IS NOT NULL DROP TABLE dbo.Notes;
IF OBJECT_ID('dbo.Announcements', 'U') IS NOT NULL DROP TABLE dbo.Announcements;
IF OBJECT_ID('dbo.ContactMessages', 'U') IS NOT NULL DROP TABLE dbo.ContactMessages;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Subjects', 'U') IS NOT NULL DROP TABLE dbo.Subjects;
IF OBJECT_ID('dbo.Roles', 'U') IS NOT NULL DROP TABLE dbo.Roles;

-- Create Roles Table
CREATE TABLE dbo.Roles (
	RoleID INT PRIMARY KEY IDENTITY(1,1),
	RoleName NVARCHAR(50) NOT NULL UNIQUE,
	Description NVARCHAR(255)
);

-- Create Subjects Table
CREATE TABLE dbo.Subjects (
	SubjectID INT PRIMARY KEY IDENTITY(1,1),
	SubjectName NVARCHAR(100) NOT NULL,
	Description NVARCHAR(500),
	IconPath NVARCHAR(255),
	IsActive BIT NOT NULL DEFAULT 1
);

-- Create Users Table
CREATE TABLE dbo.Users (
	UserID INT PRIMARY KEY IDENTITY(1,1),
	FullName NVARCHAR(100) NOT NULL,
	Username NVARCHAR(50) NOT NULL UNIQUE,
	Email NVARCHAR(100) NOT NULL,
	PasswordHash CHAR(64) NOT NULL,
	PasswordSalt CHAR(32) NOT NULL,
	RoleID INT NOT NULL,
	PhoneNumber NVARCHAR(20),
	IsActive BIT NOT NULL DEFAULT 1,
	CreatedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
	LastLoginAt DATETIME,
	FOREIGN KEY (RoleID) REFERENCES dbo.Roles(RoleID)
);

-- Create Notes Table
CREATE TABLE dbo.Notes (
	NoteID INT PRIMARY KEY IDENTITY(1,1),
	SubjectID INT NOT NULL,
	Title NVARCHAR(200) NOT NULL,
	ContentHtml NVARCHAR(MAX),
	SortOrder INT,
	FOREIGN KEY (SubjectID) REFERENCES dbo.Subjects(SubjectID)
);

-- Create Resources Table (for note attachments/PDFs)
CREATE TABLE dbo.Resources (
	ResourceID INT PRIMARY KEY IDENTITY(1,1),
	NoteID INT NOT NULL,
	Title NVARCHAR(200) NOT NULL,
	FilePath NVARCHAR(500) NOT NULL,
	UploadedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
	FOREIGN KEY (NoteID) REFERENCES dbo.Notes(NoteID) ON DELETE CASCADE
);

-- Create Quizzes Table
CREATE TABLE dbo.Quizzes (
	QuizID INT PRIMARY KEY IDENTITY(1,1),
	SubjectID INT NOT NULL,
	Title NVARCHAR(200) NOT NULL,
	PassMark INT,
	IsActive BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (SubjectID) REFERENCES dbo.Subjects(SubjectID)
);

-- Create Questions Table
CREATE TABLE dbo.Questions (
	QuestionID INT PRIMARY KEY IDENTITY(1,1),
	QuizID INT NOT NULL,
	QuestionText NVARCHAR(MAX) NOT NULL,
	QuestionType NVARCHAR(50),
	Marks INT,
	FOREIGN KEY (QuizID) REFERENCES dbo.Quizzes(QuizID) ON DELETE CASCADE
);

-- Create QuestionOptions Table
CREATE TABLE dbo.QuestionOptions (
	OptionID INT PRIMARY KEY IDENTITY(1,1),
	QuestionID INT NOT NULL,
	OptionText NVARCHAR(MAX),
	IsCorrect BIT NOT NULL DEFAULT 0,
	SortOrder INT,
	FOREIGN KEY (QuestionID) REFERENCES dbo.Questions(QuestionID) ON DELETE CASCADE
);

-- Create QuizAttempts Table
CREATE TABLE dbo.QuizAttempts (
	AttemptID INT PRIMARY KEY IDENTITY(1,1),
	UserID INT NOT NULL,
	QuizID INT NOT NULL,
	Score INT,
	TotalMarks INT,
	IsPassed BIT,
	AttemptedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
	FOREIGN KEY (UserID) REFERENCES dbo.Users(UserID),
	FOREIGN KEY (QuizID) REFERENCES dbo.Quizzes(QuizID)
);

-- Create Announcements Table
CREATE TABLE dbo.Announcements (
	AnnouncementID INT PRIMARY KEY IDENTITY(1,1),
	Title NVARCHAR(200) NOT NULL,
	Message NVARCHAR(MAX),
	PostedBy INT,
	PostedAt DATETIME NOT NULL DEFAULT GETUTCDATE(),
	IsActive BIT NOT NULL DEFAULT 1,
	FOREIGN KEY (PostedBy) REFERENCES dbo.Users(UserID)
);

-- Create ContactMessages Table
CREATE TABLE dbo.ContactMessages (
	MessageID INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	Subject NVARCHAR(200),
	Message NVARCHAR(MAX),
	IsRead BIT NOT NULL DEFAULT 0,
	SubmittedAt DATETIME NOT NULL DEFAULT GETUTCDATE()
);

-- Create Indexes
CREATE INDEX IX_Users_Username ON dbo.Users(Username);
CREATE INDEX IX_Users_Email ON dbo.Users(Email);
CREATE INDEX IX_Users_RoleID ON dbo.Users(RoleID);
CREATE INDEX IX_Notes_SubjectID ON dbo.Notes(SubjectID);
CREATE INDEX IX_Questions_QuizID ON dbo.Questions(QuizID);
CREATE INDEX IX_QuestionOptions_QuestionID ON dbo.QuestionOptions(QuestionID);
CREATE INDEX IX_Quizzes_SubjectID ON dbo.Quizzes(SubjectID);
CREATE INDEX IX_QuizAttempts_UserID ON dbo.QuizAttempts(UserID);
CREATE INDEX IX_QuizAttempts_QuizID ON dbo.QuizAttempts(QuizID);
CREATE INDEX IX_Announcements_PostedBy ON dbo.Announcements(PostedBy);

-- Insert Default Roles
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE RoleID = 1)
BEGIN
    SET IDENTITY_INSERT dbo.Roles ON;
    INSERT INTO dbo.Roles (RoleID, RoleName, Description) VALUES (1, 'Teacher', 'Teacher/Admin role'), (2, 'Student', 'Student role');
    SET IDENTITY_INSERT dbo.Roles OFF;
END

-- Insert Sample Data (Optional - comment out if not needed)
-- INSERT INTO dbo.Subjects (SubjectName, Description, IsActive) VALUES ('Mathematics', 'Mathematics fundamentals', 1);
-- INSERT INTO dbo.Subjects (SubjectName, Description, IsActive) VALUES ('Science', 'Science concepts and experiments', 1);
-- INSERT INTO dbo.Subjects (SubjectName, Description, IsActive) VALUES ('English', 'English language and literature', 1);

PRINT 'Aarambha database created successfully!';
GO
