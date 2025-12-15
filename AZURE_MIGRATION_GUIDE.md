# Azure Storage Migration Guide - TDMS GKVK
## Migrating from Hardcoded SAS Tokens to Dynamic Backend-Managed Approach

---

## 📋 Current Situation

**Existing Organization:**
- **ID**: 1
- **Name**: SYSTEM
- **Container**: `tdms`
- **Public Container**: Not defined (need to add)
- **Current Frontend Approach**: Hardcoded SAS URL

```javascript
const sasUrl = "https://tdms.blob.core.windows.net/tdms-public?sp=racwdl&st=2025-11-20T05:13:26Z&se=2026-08-31T13:28:26Z...";
const blobServiceClient = new BlobServiceClient(sasUrl);
```

**Problems with Current Approach:**
1. ❌ SAS token expires on **2026-08-31**
2. ❌ Security risk (token exposed in frontend code)
3. ❌ No automatic token refresh
4. ❌ Manual token regeneration required when expired
5. ❌ No user-based file organization

---

## 🎯 Solution: Backend-Managed SAS Tokens (No Background Service)

### Step 1: Database Migration

#### 1.1 Update Organizations Table
```sql
-- Add public container column if not exists
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Organizations') AND name = 'StorageContainerNamePublic')
BEGIN
    ALTER TABLE Organizations
    ADD StorageContainerNamePublic NVARCHAR(63) NULL;
END

-- Add container metadata columns
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Organizations') AND name = 'ContainerCreatedDate')
BEGIN
    ALTER TABLE Organizations
    ADD ContainerCreatedDate DATETIME2 NULL,
        ContainerCreatedBy INT NULL;
END

-- Update existing organization with public container name
UPDATE Organizations
SET StorageContainerNamePublic = 'tdms-public',
    ContainerCreatedDate = GETUTCDATE(),
    ContainerCreatedBy = 1  -- Your superadmin ID
WHERE Id = 1;
```

#### 1.2 Create SAS Token Cache Table
```sql
-- Create table for caching SAS tokens
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SasTokenCache')
BEGIN
    CREATE TABLE SasTokenCache (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ContainerName NVARCHAR(63) NOT NULL,
        SasToken NVARCHAR(MAX) NOT NULL,
        Permissions NVARCHAR(10) NOT NULL,
        ExpiresOn DATETIME2 NOT NULL,
        CreatedOn DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        IsPublicContainer BIT NOT NULL DEFAULT 0,

        CONSTRAINT UQ_Container_Permissions UNIQUE (ContainerName, Permissions, IsPublicContainer)
    );

    CREATE INDEX IX_SasTokenCache_ExpiresOn ON SasTokenCache(ExpiresOn);
    CREATE INDEX IX_SasTokenCache_ContainerName ON SasTokenCache(ContainerName);
END
```

### Step 2: Backend Configuration

#### 2.1 Add to appsettings.json
```json
{
  "AzureStorage": {
    "ConnectionString": "DefaultEndpointsProtocol=https;AccountName=tdms;AccountKey=YOUR_KEY_HERE;EndpointSuffix=core.windows.net",
    "AccountName": "tdms",
    "AccountKey": "YOUR_ACCOUNT_KEY_HERE",
    "SasTokenExpiryHours": 24
  }
}
```

**How to get your Azure Storage credentials:**
1. Go to Azure Portal → Storage Accounts → "tdms"
2. Left menu → Security + networking → Access keys
3. Copy "Key 1" → Connection string (or Key)

#### 2.2 Install NuGet Packages
```bash
dotnet add package Azure.Storage.Blobs --version 12.23.0
dotnet add package Azure.Identity --version 1.14.1
```

### Step 3: No Background Service Required! ✅

**Instead of a background service (which requires additional resources), we use on-demand cleanup:**

```csharp
// In GetSasTokenAsync method:
public async Task<string> GetSasTokenAsync(...)
{
    // 🔥 Cleanup expired tokens ON-DEMAND (runs when generating new tokens)
    await CleanupExpiredTokensAsync();

    // ... rest of token generation logic
}

private async Task CleanupExpiredTokensAsync()
{
    var expiredTokens = await _context.SasTokenCache
        .Where(t => t.ExpiresOn < DateTime.UtcNow)
        .ToListAsync();

    if (expiredTokens.Any())
    {
        _context.SasTokenCache.RemoveRange(expiredTokens);
        await _context.SaveChangesAsync();
    }
}
```

**Benefits of On-Demand Cleanup:**
- ✅ No background service/worker required
- ✅ Works on **basic tier Azure subscription**
- ✅ Zero additional cost
- ✅ Automatic cleanup when tokens are requested
- ✅ Simple and efficient

---

## 🔧 Azure Setup Instructions

### Option 1: Using Existing Storage Account (Recommended)

You already have the "tdms" storage account. Just verify:

1. **Check Existing Containers:**
   - Go to Azure Portal → Storage Account "tdms" → Containers
   - You should see:
     - `tdms` (private container)
     - `tdms-public` (public container - if not, create it)

2. **Create tdms-public Container (if missing):**
   ```bash
   # Using Azure CLI:
   az storage container create \
       --name tdms-public \
       --account-name tdms \
       --public-access blob

   # Or via Azure Portal:
   # Storage Account → Containers → + Container
   # Name: tdms-public
   # Public access level: Blob (anonymous read access for blobs only)
   ```

3. **Verify Access Levels:**
   - `tdms` → Access level: **Private (no anonymous access)**
   - `tdms-public` → Access level: **Blob (anonymous read access for blobs only)**

### Option 2: Create New Storage Account (For New Organizations)

Only needed when creating NEW organizations:

```bash
# Create resource group (if needed)
az group create \
    --name rg-gkvk-storage \
    --location centralindia

# Create storage account
az storage account create \
    --name gkvkstorage \
    --resource-group rg-gkvk-storage \
    --location centralindia \
    --sku Standard_LRS \
    --kind StorageV2
```

---

## 📦 Entity and Interface Setup

### 1. SasTokenCache Entity

```csharp
// Domain/Entities/SasTokenCache.cs
namespace Domain.Entities
{
    public class SasTokenCache : BaseEntity
    {
        public string ContainerName { get; set; } = string.Empty;
        public string SasToken { get; set; } = string.Empty;
        public string Permissions { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPublicContainer { get; set; }
    }
}
```

### 2. IAzureStorageService Interface

```csharp
// Application/Interface/Services/Common/IAzureStorageService.cs
namespace Application.Interface.Services.Common
{
    public interface IAzureStorageService
    {
        Task<(bool Success, string PrivateContainer, string PublicContainer)>
            CreateOrganizationContainersAsync(
                int organizationId,
                string privateContainerName,
                string publicContainerName);

        Task<string> GetSasTokenAsync(
            string containerName,
            bool isPublic,
            string permissions);

        Task<string> UploadFileAsync(
            Stream fileStream,
            string containerName,
            int userId,
            string fileName,
            string folder = null);

        Task<bool> DeleteFileAsync(
            string containerName,
            string blobName);

        Task<List<string>> ListFilesAsync(
            string containerName,
            string prefix);
    }
}
```

### 3. Register DbSet in TdmsDbContext

```csharp
// Infracture/DbContext/TdmsDbContext.cs
public DbSet<SasTokenCache> SasTokenCache { get; set; }
```

### 4. Register Service in Program.cs

```csharp
// WebApi/Program.cs
builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();
```

---

## 🚀 Frontend Migration Strategy

### Before (Hardcoded SAS - ❌ Don't Do This):
```javascript
const sasUrl = "https://tdms.blob.core.windows.net/tdms-public?sp=racwdl&st=...";
const blobServiceClient = new BlobServiceClient(sasUrl);
```

### After (Backend-Managed SAS - ✅ Best Practice):

```typescript
// 1. Get SAS token from backend
const response = await fetch('https://your-api.com/api/Storage/sas-token', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${jwtToken}`
  },
  body: JSON.stringify({
    containerName: 'tdms-public',
    isPublic: true,
    permissions: 'rwdl'
  })
});

const { sasToken, containerName } = await response.json();

// 2. Construct full SAS URL
const sasUrl = `https://tdms.blob.core.windows.net/${containerName}?${sasToken}`;

// 3. Use with Azure SDK
const blobServiceClient = new BlobServiceClient(sasUrl);
```

---

## 🔄 Backward Compatibility

**For existing users using hardcoded SAS tokens:**

1. **Short Term (During Migration):**
   - Keep the hardcoded SAS token working until 2026-08-31
   - Gradually migrate users to backend-managed approach
   - Add a warning message: "Please update your app - this method will be deprecated"

2. **Long Term (After Migration):**
   - Replace all hardcoded SAS URLs with backend API calls
   - Automatic token refresh
   - Better security

**Migration Checklist:**
- [ ] Run database migration scripts
- [ ] Add Azure Storage configuration to appsettings.json
- [ ] Install NuGet packages
- [ ] Implement AzureStorageService with on-demand cleanup
- [ ] Create API endpoints (StorageController)
- [ ] Update frontend to use backend API instead of hardcoded SAS
- [ ] Test file upload/download with dynamic SAS tokens
- [ ] Verify token caching works
- [ ] Verify expired token cleanup works

---

## 📊 Testing the Migration

### Test 1: Generate SAS Token
```bash
curl -X POST https://your-api.com/api/Storage/sas-token \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "containerName": "tdms-public",
    "isPublic": true,
    "permissions": "rwdl"
  }'

# Expected Response:
{
  "sasToken": "sp=racwdl&st=2025-12-15T10:00:00Z&se=2025-12-16T10:00:00Z&...",
  "containerName": "tdms-public",
  "expiresOn": "2025-12-16T10:00:00Z",
  "isPublic": true
}
```

### Test 2: Upload File
```bash
curl -X POST https://your-api.com/api/Storage/upload \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -F "file=@test.pdf" \
  -F "containerName=tdms-public" \
  -F "userId=123" \
  -F "folder=documents"

# Expected Response:
{
  "success": true,
  "blobUrl": "https://tdms.blob.core.windows.net/tdms-public/123/documents/test.pdf",
  "fileName": "test.pdf"
}
```

### Test 3: Verify File Organization
Files should be organized by userId:
```
tdms-public/
├── 1/                    (superadmin)
│   └── logos/
│       └── logo.png
├── 123/                  (trainer)
│   ├── documents/
│   │   └── test.pdf
│   └── profile/
│       └── avatar.jpg
```

---

## 🔒 Security Benefits

**Before (Hardcoded SAS):**
- ❌ Token expires on fixed date (2026-08-31)
- ❌ Token visible in frontend code (security risk)
- ❌ Same token used by all users
- ❌ Manual token regeneration required

**After (Backend-Managed):**
- ✅ Token auto-refreshes every 24 hours
- ✅ Token generated on-demand (never exposed in frontend)
- ✅ User-specific file organization
- ✅ Role-based access control
- ✅ Automatic cleanup of expired tokens

---

## 💡 Key Takeaways

1. **No Background Service Needed**: Use on-demand cleanup during token generation
2. **Backward Compatible**: Existing `tdms` and `tdms-public` containers work as-is
3. **Automatic Token Refresh**: No more manual SAS token updates
4. **User-Based File Organization**: Files stored as `{userId}/{folder}/{file}`
5. **Basic Tier Friendly**: No additional Azure resources required

---

## 📞 Next Steps

1. **Run Database Migrations** (Step 1.1 and 1.2)
2. **Add Azure Configuration** to appsettings.json
3. **Implement AzureStorageService** (I'll provide the code)
4. **Create API Controller** (I'll provide the code)
5. **Update Frontend** to use backend API instead of hardcoded SAS
6. **Test with Existing Organization** (ID: 1, Container: tdms)

Would you like me to provide the complete implementation code for:
- AzureStorageService with on-demand cleanup?
- StorageController API endpoints?
- Frontend service integration example?
