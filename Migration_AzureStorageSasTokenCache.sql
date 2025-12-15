-- ========================================
-- Azure Storage SAS Token Cache Migration
-- Date: 2025-12-15
-- Description: Adds SasTokenCache table and updates Organizations table
-- ========================================

BEGIN TRANSACTION;

-- ========================================
-- 1. Add new columns to Organizations table
-- ========================================

-- Add StorageContainerNamePublic column (nullable)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('Organizations')
    AND name = 'StorageContainerNamePublic'
)
BEGIN
    ALTER TABLE Organizations
    ADD StorageContainerNamePublic NVARCHAR(63) NULL;

    PRINT '✅ Added StorageContainerNamePublic column to Organizations table';
END
ELSE
BEGIN
    PRINT '⚠️  StorageContainerNamePublic column already exists';
END

-- Add ContainerCreatedDate column (nullable DateTimeOffset)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('Organizations')
    AND name = 'ContainerCreatedDate'
)
BEGIN
    ALTER TABLE Organizations
    ADD ContainerCreatedDate DATETIMEOFFSET NULL;

    PRINT '✅ Added ContainerCreatedDate column to Organizations table';
END
ELSE
BEGIN
    PRINT '⚠️  ContainerCreatedDate column already exists';
END

-- Add ContainerCreatedBy column (nullable int)
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('Organizations')
    AND name = 'ContainerCreatedBy'
)
BEGIN
    ALTER TABLE Organizations
    ADD ContainerCreatedBy INT NULL;

    PRINT '✅ Added ContainerCreatedBy column to Organizations table';
END
ELSE
BEGIN
    PRINT '⚠️  ContainerCreatedBy column already exists';
END

-- ========================================
-- 2. Create SasTokenCache table
-- ========================================

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SasTokenCache')
BEGIN
    CREATE TABLE SasTokenCache (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ContainerName NVARCHAR(255) NOT NULL,
        SasToken NVARCHAR(MAX) NOT NULL,
        Permissions NVARCHAR(50) NOT NULL,
        ExpiresOn DATETIME NOT NULL,
        IsPublicContainer BIT NOT NULL,
        CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSUTCDATETIME(),
        UpdatedAt DATETIMEOFFSET NULL
    );

    -- Create index for faster token lookups
    CREATE INDEX IX_SasTokenCache_ContainerName_Permissions
    ON SasTokenCache (ContainerName, Permissions, IsPublicContainer, ExpiresOn);

    PRINT '✅ Created SasTokenCache table with index';
END
ELSE
BEGIN
    PRINT '⚠️  SasTokenCache table already exists';
END

-- ========================================
-- 3. Update existing organization (ID=1) with public container name
-- ========================================

-- Check if organization with ID=1 exists and update it
IF EXISTS (SELECT 1 FROM Organizations WHERE Id = 1)
BEGIN
    -- Only update if StorageContainerNamePublic is NULL
    UPDATE Organizations
    SET StorageContainerNamePublic = CASE
        WHEN StorageContainerNamePublic IS NULL
        THEN StorageContainerName + '-public'
        ELSE StorageContainerNamePublic
    END
    WHERE Id = 1 AND StorageContainerNamePublic IS NULL;

    -- Verify the update
    DECLARE @publicContainer NVARCHAR(63);
    SELECT @publicContainer = StorageContainerNamePublic FROM Organizations WHERE Id = 1;

    PRINT '✅ Updated organization ID=1 with public container: ' + ISNULL(@publicContainer, 'NULL');
END
ELSE
BEGIN
    PRINT '⚠️  Organization with ID=1 not found (may not exist yet)';
END

-- ========================================
-- 4. Verification queries
-- ========================================

PRINT '';
PRINT '========================================';
PRINT 'MIGRATION VERIFICATION';
PRINT '========================================';

-- Verify Organizations columns
SELECT
    name AS ColumnName,
    TYPE_NAME(system_type_id) AS DataType,
    max_length AS MaxLength,
    is_nullable AS IsNullable
FROM sys.columns
WHERE object_id = OBJECT_ID('Organizations')
AND name IN ('StorageContainerName', 'StorageContainerNamePublic', 'ContainerCreatedDate', 'ContainerCreatedBy');

PRINT 'Organizations table columns verified';

-- Verify SasTokenCache table exists
IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'SasTokenCache')
BEGIN
    PRINT 'SasTokenCache table exists ✅';

    -- Show table structure
    SELECT
        name AS ColumnName,
        TYPE_NAME(system_type_id) AS DataType,
        max_length AS MaxLength,
        is_nullable AS IsNullable
    FROM sys.columns
    WHERE object_id = OBJECT_ID('SasTokenCache')
    ORDER BY column_id;
END

-- Verify organization ID=1 update
IF EXISTS (SELECT 1 FROM Organizations WHERE Id = 1)
BEGIN
    SELECT
        Id,
        Name,
        StorageContainerName,
        StorageContainerNamePublic,
        ContainerCreatedDate,
        ContainerCreatedBy
    FROM Organizations
    WHERE Id = 1;

    PRINT 'Organization ID=1 verified ✅';
END

PRINT '';
PRINT '========================================';
PRINT 'MIGRATION COMPLETED SUCCESSFULLY ✅';
PRINT '========================================';

COMMIT TRANSACTION;
