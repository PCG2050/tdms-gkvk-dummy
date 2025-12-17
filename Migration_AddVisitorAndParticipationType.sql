-- ===================================================================
-- Migration: Add Visitor and ParticipationType Support to Services
-- Description: This migration adds ParticipationType table and updates
--              the Services table to include VisitorId and ParticipationTypeId
-- ===================================================================

BEGIN TRANSACTION;

-- Step 1: Create ParticipationType table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ParticipationTypes')
BEGIN
    CREATE TABLE [dbo].[ParticipationTypes] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [Name] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_ParticipationTypes] PRIMARY KEY ([Id])
    );
    PRINT 'Created ParticipationTypes table';
END
ELSE
BEGIN
    PRINT 'ParticipationTypes table already exists';
END

-- Step 2: Seed ParticipationType data
IF NOT EXISTS (SELECT * FROM ParticipationTypes WHERE Name = 'Individual')
BEGIN
    INSERT INTO ParticipationTypes (Name) VALUES ('Individual');
    PRINT 'Inserted Individual participation type';
END

IF NOT EXISTS (SELECT * FROM ParticipationTypes WHERE Name = 'Group')
BEGIN
    INSERT INTO ParticipationTypes (Name) VALUES ('Group');
    PRINT 'Inserted Group participation type';
END

-- Step 3: Check if VisitorId and ParticipationTypeId columns exist in Services table
DECLARE @VisitorIdExists BIT = 0;
DECLARE @ParticipationTypeIdExists BIT = 0;

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND name = 'VisitorId')
    SET @VisitorIdExists = 1;

IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND name = 'ParticipationTypeId')
    SET @ParticipationTypeIdExists = 1;

-- Step 4: Drop existing foreign keys if they exist (in case migration failed before)
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Services_Visitors_VisitorId')
BEGIN
    ALTER TABLE [dbo].[Services] DROP CONSTRAINT [FK_Services_Visitors_VisitorId];
    PRINT 'Dropped existing FK_Services_Visitors_VisitorId constraint';
END

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_Services_ParticipationType_ParticipationTypeId')
BEGIN
    ALTER TABLE [dbo].[Services] DROP CONSTRAINT [FK_Services_ParticipationType_ParticipationTypeId];
    PRINT 'Dropped existing FK_Services_ParticipationType_ParticipationTypeId constraint';
END

-- Step 5: Add columns if they don't exist (as nullable first)
IF @VisitorIdExists = 0
BEGIN
    ALTER TABLE [dbo].[Services] ADD [VisitorId] INT NULL;
    PRINT 'Added VisitorId column to Services table (nullable)';
END

IF @ParticipationTypeIdExists = 0
BEGIN
    ALTER TABLE [dbo].[Services] ADD [ParticipationTypeId] INT NULL;
    PRINT 'Added ParticipationTypeId column to Services table (nullable)';
END

-- Step 6: Get default IDs for seeded data
DECLARE @DefaultVisitorId INT = (SELECT TOP 1 Id FROM Visitors ORDER BY Id);
DECLARE @IndividualTypeId INT = (SELECT Id FROM ParticipationTypes WHERE Name = 'Individual');

-- Step 7: Update existing NULL values with defaults
IF @DefaultVisitorId IS NOT NULL
BEGIN
    UPDATE [dbo].[Services]
    SET [VisitorId] = @DefaultVisitorId
    WHERE [VisitorId] IS NULL;
    PRINT 'Updated NULL VisitorId values to: ' + CAST(@DefaultVisitorId AS NVARCHAR(10));
END

IF @IndividualTypeId IS NOT NULL
BEGIN
    UPDATE [dbo].[Services]
    SET [ParticipationTypeId] = @IndividualTypeId
    WHERE [ParticipationTypeId] IS NULL;
    PRINT 'Updated NULL ParticipationTypeId values to: ' + CAST(@IndividualTypeId AS NVARCHAR(10));
END

-- Step 8: Make columns NOT NULL
ALTER TABLE [dbo].[Services] ALTER COLUMN [VisitorId] INT NOT NULL;
ALTER TABLE [dbo].[Services] ALTER COLUMN [ParticipationTypeId] INT NOT NULL;
PRINT 'Changed VisitorId and ParticipationTypeId to NOT NULL';

-- Step 9: Add foreign key constraints
ALTER TABLE [dbo].[Services]
    ADD CONSTRAINT [FK_Services_Visitors_VisitorId]
    FOREIGN KEY ([VisitorId]) REFERENCES [dbo].[Visitors] ([Id]);
PRINT 'Added FK constraint: FK_Services_Visitors_VisitorId';

ALTER TABLE [dbo].[Services]
    ADD CONSTRAINT [FK_Services_ParticipationType_ParticipationTypeId]
    FOREIGN KEY ([ParticipationTypeId]) REFERENCES [dbo].[ParticipationTypes] ([Id]);
PRINT 'Added FK constraint: FK_Services_ParticipationType_ParticipationTypeId';

-- Step 10: Add index for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Services_VisitorId' AND object_id = OBJECT_ID('Services'))
BEGIN
    CREATE INDEX [IX_Services_VisitorId] ON [dbo].[Services] ([VisitorId]);
    PRINT 'Created index IX_Services_VisitorId';
END

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Services_ParticipationTypeId' AND object_id = OBJECT_ID('Services'))
BEGIN
    CREATE INDEX [IX_Services_ParticipationTypeId] ON [dbo].[Services] ([ParticipationTypeId]);
    PRINT 'Created index IX_Services_ParticipationTypeId';
END

COMMIT TRANSACTION;

PRINT '========================================';
PRINT 'Migration completed successfully!';
PRINT 'ParticipationType table created and seeded with:';
SELECT * FROM ParticipationTypes;
PRINT '========================================';
