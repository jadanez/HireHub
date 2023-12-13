-- HireHub Database
-- Revision History:
-- John Danez, 2023.12.12: Job, jobDetails changed to NVARCHAR(MAX)
-- John Danez, 2023.11.28: Created


USE master
GO
 
IF DB_ID('HireHub') IS NOT NULL
	DECLARE @killCommand NVARCHAR(MAX) = '';
	SELECT @killCommand += 'KILL ' + CAST(spid AS NVARCHAR) + ';'
	FROM sys.sysprocesses
	WHERE dbid = DB_ID('HireHub');
	EXEC sp_executesql @killCommand;

	DROP DATABASE HireHub
GO




CREATE DATABASE HireHub
GO
 
-- Change to HireHub database 
USE HireHub
GO


 -- Create Tables
DROP TABLE IF EXISTS dbo.Account; -- Dropping table in case it exists
CREATE TABLE Account(
	accountID INT NOT NULL IDENTITY
)


DROP TABLE IF EXISTS dbo.Profile; -- Dropping table in case it exists
CREATE TABLE Profile(
	profileID INT NOT NULL IDENTITY
)


DROP TABLE IF EXISTS dbo.Job; -- Dropping table in case it exists
CREATE TABLE Job(
	jobID INT NOT NULL IDENTITY
)


DROP TABLE IF EXISTS dbo.Applicant; -- Dropping table in case it exists
CREATE TABLE Applicant(
	applicantID INT NOT NULL IDENTITY
)



-- Add Columns
ALTER TABLE Account 
ADD
	userType VARCHAR (30) NOT NULL,
	firstName VARCHAR(50) NOT NULL,
    lastName VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL CHECK (email LIKE '%_@__%.__%'),
    phoneNumber VARCHAR(10) CHECK (LEN(phoneNumber) = 10), 
    passphrase VARCHAR(255) NOT NULL
;

ALTER TABLE Profile
ADD

	accountID INT NOT NULL,
	profileHeader VARCHAR (255) NOT NULL,
	currentRole VARCHAR (255) NOT NULL,
	targetRole VARCHAR (30) NOT NULL,
	currentCompany VARCHAR (255) NOT NULL,
	expectedSalary DECIMAL(7,2) NOT NULL,
	employmentStatus VARCHAR (30) NOT NULL,
	employmentHistory VARCHAR (255) NOT NULL
;

ALTER TABLE Job
ADD

	employerID INT NOT NULL,
	jobStatus VARCHAR (30) NOT NULL,
	roleName VARCHAR (30) NOT NULL,
	companyName VARCHAR (30) NOT NULL,
	jobType VARCHAR (30) NOT NULL,
	experienceLevel VARCHAR (30) NOT NULL,
	jobDetails NVARCHAR(MAX) NOT NULL,
	salary DECIMAL(12,2) NOT NULL,
	jobLocation VARCHAR (30) NOT NULL,
	hiringManager VARCHAR (30) NOT NULL
;

ALTER TABLE Applicant
ADD
	profileID INT NOT NULL,
	jobID INT NOT NULL,
	applicantStatus VARCHAR (30) NOT NULL
;


-- Set PKs
ALTER TABLE Account 
ADD 
	CONSTRAINT PK_AccountID
    PRIMARY KEY (accountID);


ALTER TABLE Profile 
ADD 
	CONSTRAINT PK_ProfileID
    PRIMARY KEY (profileID);

ALTER TABLE Job 
ADD 
	CONSTRAINT PK_JobID
    PRIMARY KEY (jobID);


ALTER TABLE Applicant 
ADD 
	CONSTRAINT PK_ApplicantID
    PRIMARY KEY (applicantID);



-- Set FKs
ALTER TABLE Profile 
ADD 
	CONSTRAINT FK_Profile_AccountID 
	FOREIGN KEY (accountID)
	REFERENCES Account(accountID);


ALTER TABLE Job 
ADD 
	CONSTRAINT FK_Job_EmployerID 
	FOREIGN KEY (employerID)
	REFERENCES Account(accountID);


ALTER TABLE Applicant
ADD
	CONSTRAINT FK_Applicant_ProfileID 
	FOREIGN KEY (profileID)
	REFERENCES Profile(profileID),

	CONSTRAINT FK_Applicant_JobID 
	FOREIGN KEY (jobID)
	REFERENCES Job(jobID);
GO

 -- initialize data
INSERT INTO Account (userType, firstName, lastName, email, phoneNumber, passphrase)
VALUES
    ('Job Seeker', 'Alice', 'Smith', 'alice.smith@example.com', '9876543210', 'strongpassword'),
    ('Job Seeker', 'Bob', 'Johnson', 'bob.johnson@example.com', '5678901234', 'safeandsecure'),
    ('Job Seeker', 'Emily', 'Jones', 'emily.jones@example.com', '8765432109', 'password123'),
    ('Job Seeker', 'David', 'Brown', 'david.brown@example.com', '2345678901', 'userpass'),
    ('Job Seeker', 'Sophia', 'Miller', 'sophia.miller@example.com', '8901234567', 'secret123'),
    ('Job Seeker', 'Michael', 'Davis', 'michael.davis@example.com', '3456789012', 'accessgranted'),
    ('Job Seeker', 'Emma', 'Taylor', 'emma.taylor@example.com', '0123456789', 'passwordsafe'),
    ('Job Seeker', 'Christopher', 'White', 'christopher.white@example.com', '7890123456', 'secure123'),
    ('Job Seeker', 'Olivia', 'Martin', 'olivia.martin@example.com', '4567890123', 'usersecure'),
    ('Job Seeker', 'Daniel', 'Anderson', 'daniel.anderson@example.com', '9012345678', 'pass1234'),
    ('Employer', 'John', 'Doe', 'john.doe@company.com', '1234567890', 'employerpass'),
    ('Employer', 'Mary', 'Johnson', 'mary.johnson@company.com', '9876543210', 'company123'),
    ('Employer', 'Richard', 'Smith', 'richard.smith@company.com', '5678901234', 'employersecure'),
    ('Employer', 'Laura', 'Williams', 'laura.williams@company.com', '8765432109', 'company456'),
    ('Employer', 'Steven', 'Davis', 'steven.davis@company.com', '2345678901', 'employerpass123'),
    ('Employer', 'Catherine', 'Brown', 'catherine.brown@company.com', '8901234567', 'company789'),
    ('Employer', 'Alex', 'Taylor', 'alex.taylor@company.com', '3456789012', 'employersecure123'),
    ('Employer', 'Eva', 'Martin', 'eva.martin@company.com', '0123456789', 'companysecure'),
    ('Employer', 'Robert', 'White', 'robert.white@company.com', '7890123456', 'employer123'),
    ('Employer', 'Grace', 'Anderson', 'grace.anderson@company.com', '4567890123', 'companypass');
GO

INSERT INTO Profile (accountID, profileHeader, currentRole, targetRole, currentCompany, expectedSalary, employmentStatus, employmentHistory)
VALUES
    (1, 'Sample Profile Header Alice', 'Sample Current Role Alice', 'Sample Target Role Alice', 'Sample Current Company Alice', 1000, 'Employed', 'Sample Employment History Alice'),
    (2, 'Sample Profile Header Bob', 'Sample Current Role Bob', 'Sample Target Role Bob', 'Sample Current Company Bob', 1200, 'Unemployed', 'Sample Employment History Bob'),
    (3, 'Sample Profile Header Emily', 'Sample Current Role Emily', 'Sample Target Role Emily', 'Sample Current Company Emily', 1100, 'Employed', 'Sample Employment History Emily'),
    (4, 'Sample Profile Header David', 'Sample Current Role David', 'Sample Target Role David', 'Sample Current Company David', 1500, 'Unemployed', 'Sample Employment History David'),
    (5, 'Sample Profile Header Sophia', 'Sample Current Role Sophia', 'Sample Target Role Sophia', 'Sample Current Company Sophia', 1300, 'Employed', 'Sample Employment History Sophia'),
    (6, 'Sample Profile Header Michael', 'Sample Current Role Michael', 'Sample Target Role Michael', 'Sample Current Company Michael', 1400, 'Unemployed', 'Sample Employment History Michael'),
    (7, 'Sample Profile Header Emma', 'Sample Current Role Emma', 'Sample Target Role Emma', 'Sample Current Company Emma', 1600, 'Employed', 'Sample Employment History Emma'),
    (8, 'Sample Profile Header Christopher', 'Sample Current Role Christopher', 'Sample Target Role Christopher', 'Sample Current Company Christopher', 1700, 'Unemployed', 'Sample Employment History Christopher'),
    (9, 'Sample Profile Header Olivia', 'Sample Current Role Olivia', 'Sample Target Role Olivia', 'Sample Current Company Olivia', 1800, 'Employed', 'Sample Employment History Olivia'),
    (10, 'Sample Profile Header Daniel', 'Sample Current Role Daniel', 'Sample Target Role Daniel', 'Sample Current Company Daniel', 1900, 'Employed', 'Sample Employment History Daniel'),
    (11, 'Sample Profile Header John', 'Sample Current Role John', 'Sample Target Role John', 'Sample Current Company John', 2000, 'Employed', 'Sample Employment History John'),
    (12, 'Sample Profile Header Mary', 'Sample Current Role Mary', 'Sample Target Role Mary', 'Sample Current Company Mary', 2200, 'Unemployed', 'Sample Employment History Mary'),
    (13, 'Sample Profile Header Richard', 'Sample Current Role Richard', 'Sample Target Role Richard', 'Sample Current Company Richard', 2100, 'Employed', 'Sample Employment History Richard'),
    (14, 'Sample Profile Header Laura', 'Sample Current Role Laura', 'Sample Target Role Laura', 'Sample Current Company Laura', 2500, 'Unemployed', 'Sample Employment History Laura'),
    (15, 'Sample Profile Header Steven', 'Sample Current Role Steven', 'Sample Target Role Steven', 'Sample Current Company Steven', 2300, 'Employed', 'Sample Employment History Steven'),
    (16, 'Sample Profile Header Catherine', 'Sample Current Role Catherine', 'Sample Target Role Catherine', 'Sample Current Company Catherine', 2400, 'Unemployed', 'Sample Employment History Catherine'),
    (17, 'Sample Profile Header Alex', 'Sample Current Role Alex', 'Sample Target Role Alex', 'Sample Current Company Alex', 2600, 'Employed', 'Sample Employment History Alex'),
    (18, 'Sample Profile Header Eva', 'Sample Current Role Eva', 'Sample Target Role Eva', 'Sample Current Company Eva', 2700, 'Unemployed', 'Sample Employment History Eva'),
    (19, 'Sample Profile Header Robert', 'Sample Current Role Robert', 'Sample Target Role Robert', 'Sample Current Company Robert', 2800, 'Employed', 'Sample Employment History Robert'),
    (20, 'Sample Profile Header Grace', 'Sample Current Role Grace', 'Sample Target Role Grace', 'Sample Current Company Grace', 2900, 'Employed', 'Sample Employment History Grace');
GO



INSERT INTO Job (EmployerID, jobStatus, roleName, companyName, jobType, experienceLevel, jobDetails, salary, jobLocation, hiringManager)
VALUES
    (11, 'Active', 'Software Developer', 'Tech Solutions Inc.', 'Full-Time', 'Mid-Level', 'Develop software applications', 80000, 'City A', 'John Doe'),
    (12, 'Active', 'Marketing Specialist', 'Marketing Pro Agency', 'Part-Time', 'Entry-Level', 'Execute marketing campaigns', 50000, 'City B', 'Jane Smith'),
    (13, 'Active', 'Data Analyst', 'Data Insights Co.', 'Contract', 'Senior-Level', 'Analyze and interpret data', 90000, 'City C', 'Michael Johnson'),
    (14, 'Active', 'Graphic Designer', 'Design Studio Ltd.', 'Full-Time', 'Mid-Level', 'Create visual concepts', 70000, 'City D', 'Emily Davis'),
    (15, 'Active', 'Sales Representative', 'Sales Dynamics Corp.', 'Full-Time', 'Entry-Level', 'Sell products or services', 60000, 'City E', 'Christopher Wilson'),
    (16, 'Active', 'HR Manager', 'PeopleFirst Ltd.', 'Full-Time', 'Senior-Level', 'Manage human resources functions', 100000, 'City F', 'Olivia Martin'),
    (17, 'Active', 'Financial Analyst', 'Finance Experts Inc.', 'Contract', 'Mid-Level', 'Analyze financial data', 85000, 'City G', 'Daniel Brown'),
    (18, 'Active', 'Network Engineer', 'Networking Solutions Co.', 'Full-Time', 'Senior-Level', 'Design and implement computer networks', 95000, 'City H', 'Sophia Turner'),
    (19, 'Active', 'Customer Support Specialist', 'CustomerCare Services', 'Part-Time', 'Entry-Level', 'Assist customers with inquiries', 55000, 'City I', 'Michael Robinson'),
    (20, 'Active', 'Project Manager', 'Project Management Pro', 'Full-Time', 'Senior-Level', 'Lead and manage projects', 110000, 'City J', 'Emma White');
GO

INSERT INTO Applicant (profileID, jobID, applicantStatus)
VALUES
    (1, 1, 'Pending'),
    (2, 2, 'Pending'),
    (3, 3, 'Pending'),
    (4, 4, 'Pending'),
    (5, 5, 'Pending'),
    (6, 6, 'Pending'),
    (7, 7, 'Pending'),
    (8, 8, 'Pending'),
    (9, 9, 'Pending'),
    (10, 10, 'Pending');
GO



-- CREATE VIEW for Applicants List
DROP VIEW IF EXISTS dbo.ApplicantsList;
GO



CREATE VIEW ApplicantsList AS
SELECT
	ap.jobID AS jobID,
    ac.accountID AS applicantID,
	CONCAT(ac.firstName,' ', ac.lastName) AS applicantName,
    pr.expectedSalary AS expectedSalary

FROM Account ac, Profile pr, Applicant ap
WHERE (ac.accountID = pr.accountID) AND (pr.profileID = ap.profileID)
GO




	--query test: 
SELECT * FROM Account;
SELECT * FROM Profile;
SELECT * FROM Job;
SELECT * FROM Applicant;
SELECT * FROM ApplicantsList
GO



