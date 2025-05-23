
-- Create the database
CREATE DATABASE IF NOT EXISTS tacos;
USE tacos;

-- Table: UserInfo
CREATE TABLE UserInfo (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    DateOfBirth DATETIME NOT NULL,
    SSN VARCHAR(11) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Password TEXT NOT NULL,
    Role VARCHAR(50) NOT NULL DEFAULT 'Patient',
    PhoneNumber VARCHAR(20),
    Address VARCHAR(100)
);

-- Table: Doctors
CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY,
    UserId INT NOT NULL,
    Specialty TEXT,
    LicenseNumber TEXT,
    YearsOfExperience INT,
    Certifications TEXT,
    FOREIGN KEY (UserId) REFERENCES UserInfo(UserId),
    FOREIGN KEY (DoctorId) REFERENCES UserInfo(UserId)
);

-- Table: Appointments
CREATE TABLE Appointments (
    AppointmentId INT AUTO_INCREMENT PRIMARY KEY,
    AppointmentDate DATETIME NOT NULL,
    Reason VARCHAR(255),
    UserId INT NOT NULL,
    DoctorId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES UserInfo(UserId),
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId) ON DELETE CASCADE
);

-- Table: Messages
CREATE TABLE Messages (
    MessageId INT AUTO_INCREMENT PRIMARY KEY,
    SenderId INT NOT NULL,
    ReceiverId INT NOT NULL,
    MessageText TEXT NOT NULL,
    SentAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ReadAt DATETIME
);

-- Table: PatientDetails
CREATE TABLE PatientDetails (
    PatientId INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    InsuranceName VARCHAR(255),
    MemberID VARCHAR(255),
    FOREIGN KEY (UserId) REFERENCES UserInfo(UserId)
);

-- Table: Prescriptions
CREATE TABLE Prescriptions (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Dosage TEXT NOT NULL,
    Frequency TEXT NOT NULL,
    Milligrams INT NOT NULL,
    Refills INT NOT NULL,
    Doctor VARCHAR(255) NOT NULL,
    Phone VARCHAR(20) NOT NULL,
    Reason VARCHAR(255) NOT NULL DEFAULT 'No reason provided',
    FOREIGN KEY (UserId) REFERENCES UserInfo(UserId)
);

INSERT INTO UserInfo (
    FirstName, LastName, DateOfBirth, SSN, Email, Password, Role, PhoneNumber, Address
) VALUES
('Test', 'Patient', '1990-01-01', '123-45-6789', 'PatientTest@email.com', 'pass123', 'Patient', '212-555-0000', '123 Main St'),
('Test', 'Doctor',  '1985-06-15', '987-65-4321', 'DoctorTest@email.com',  'pass123', 'Doctor',  '555-777-0000', '456 Clinic Dr'),
('Test', 'Admin',   '1975-12-31', '555-66-7777', 'AdminTest@email.com', 'pass123', 'Admin',   '555-888-0000', '789 Admin Ln');