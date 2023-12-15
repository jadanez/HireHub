# Introduction 
HireHub is an innovative job application platform designed to meet the changing demands of the employment
market. Drawing inspiration from successful platforms like Indeed, HireHub is a user-friendly, robust, and
efficient system designed to meet the requirements of both employers and job seekers. With a simple and
user-friendly UI, HireHub Simplifies the hiring process and creates a helping ecosystem for employers to find
the right candidates and job seekers to find the right opportunities. With its easy-to-use features and
comprehensive functionalities, HireHub is set to revolutionize the job application process and create more
opportunities for job seekers and employers alike.

# Database Design
![image](https://github.com/jadanez/HireHub/assets/122333839/b74a4de3-8820-485f-b7a6-3b647ccd28d5)

# Getting Started
The following software is required:
1. Visual Studio
2. Microsoft SQL Server Management Studio

# Deployment
To deploy the solution:
1. Download HireHub.sql locally.
2. Run the sql file in Microsoft SQL Server Management Studio.
3. Clone the repo in Visual Studio
4. Connect to database in Visual Studio
5. Change the connection string written in DbConnection.cs which is in Database folder
6. Run the program with the following credentials:

Employer Account
      Email: john.doe@company.com
      Password: employerpass

Job Seeker Account
      Email: alice.smith@example.com
      Password: strongpassword
  
# Features  

1. All Users
   1. Registration: User can register themselves as a Job seeker or recruiter.
   2. Login: User can use their existing email and password to log in to their account.
  
3. Job Seeker
   1. Enrollment: User can add their personal information.
   2. Job Search: The user can add keywords to find any job.
   3. Search: Display all jobs, related to description, responsibilities and name
   4. Job Listing: The user can view a detailed information list with a job title, description, and ability to apply for a job role.
4. Employer
   1. Enrollment: User can add their company information.
   2. Job Posting: User can post jobs by adding detailed information.
   3. Job Listing: User can click on the job description and check how many users apply for that job.
