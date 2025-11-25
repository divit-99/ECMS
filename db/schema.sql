-- ================================================
-- Database Schema Script for ECMS
-- ================================================

-- Companies table
CREATE TABLE Companies (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName NVARCHAR(255) NOT NULL,
    Domain NVARCHAR(255) NOT NULL,
    Industry NVARCHAR(255) NULL,
    Website NVARCHAR(255) NULL
);

-- Unique index on Domain
CREATE UNIQUE INDEX IX_Companies_Domain
    ON Companies (Domain);

-- Employees table
CREATE TABLE Employees (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(50) NULL,
    JobTitle NVARCHAR(255) NULL,
    CompanyID INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT FK_Employees_Companies_CompanyID
        FOREIGN KEY (CompanyID)
        REFERENCES Companies(ID)
        ON DELETE CASCADE
);

-- Unique index on Email (active employees only)
CREATE UNIQUE INDEX IX_Employees_Email
    ON Employees(Email)
    WHERE IsActive = 1;

-- FK / Lookup index
CREATE INDEX IX_Employees_CompanyID
    ON Employees (CompanyID);

-- Users table
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    IsAdmin BIT NOT NULL,
    CreatedAt DATETIME2 NOT NULL
);

CREATE UNIQUE INDEX IX_Users_Email
    ON Users (Email);
