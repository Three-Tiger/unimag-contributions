USE master;
GO

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'uni_mag_contributions_db')
BEGIN
    EXEC('CREATE DATABASE uni_mag_contributions_db');
END;
GO

USE uni_mag_contributions_db;
GO
