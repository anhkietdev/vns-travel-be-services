# Database Test Controller Guide

## 🎯 **Purpose**
The `DatabaseTestController` helps you diagnose database connection issues in your Azure deployment. It provides detailed error information and connection status.

## 📋 **Available Test Endpoints**

### **1. Entity Framework Connection Test**
```
GET /api/databasetest/ef-test
```
**What it tests:**
- Basic database connectivity
- Entity Framework configuration
- Connection string validation
- Required Azure SQL parameters
- Simple query execution

### **2. Raw SQL Connection Test**
```
GET /api/databasetest/sql-test
```
**What it tests:**
- Direct SQL Server connection
- Server version and database info
- Raw SQL query execution
- Connection string parsing

### **3. Configuration Check**
```
GET /api/databasetest/config
```
**What it shows:**
- Connection string presence
- Environment information
- Configuration sources
- Required parameters list

### **4. Database Operations Test**
```
GET /api/databasetest/operations
```
**What it tests:**
- Table accessibility
- Schema information
- Data querying capabilities

## 🚀 **How to Use**

### **Step 1: Deploy Your Code**
Deploy the updated code with the new `DatabaseTestController` to Azure.

### **Step 2: Test Database Connection**
Visit these URLs in your browser or use a tool like Postman:

```
https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/ef-test
https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/sql-test
https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/config
https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/operations
```

### **Step 3: Interpret Results**

#### **✅ SUCCESS Response Example:**
```json
{
  "timestamp": "2024-01-15T10:30:00Z",
  "testType": "Entity Framework Connection",
  "status": "SUCCESS",
  "connectionString": "Server=vns-travel.database.windows.net;Database=VNS_Travel;User Id=***;Password=***;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
  "details": [
    "Testing basic connection...",
    "✅ Database connection successful",
    "Database Provider: Microsoft.EntityFrameworkCore.SqlServer",
    "Testing if database exists...",
    "Database exists: true",
    "Testing simple query...",
    "Users in database: 5",
    "✅ Connection string found in configuration",
    "✅ Encrypt=True parameter found",
    "✅ TrustServerCertificate=False parameter found"
  ]
}
```

#### **❌ ERROR Response Example:**
```json
{
  "timestamp": "2024-01-15T10:30:00Z",
  "testType": "Entity Framework Connection",
  "status": "ERROR",
  "connectionString": "Server=vns-travel.database.windows.net;Database=VNS_Travel;User Id=***;Password=***;",
  "error": {
    "message": "A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: SSL Provider, error: 0 - The certificate's CN name does not match the passed value.)",
    "type": "SqlException"
  },
  "details": [
    "❌ Database connection test failed",
    "Error Type: SqlException",
    "Error Message: A connection was successfully established with the server, but then an error occurred during the pre-login handshake..."
  ]
}
```

## 🔍 **Common Issues and Solutions**

### **Issue 1: Missing Encrypt=True**
**Error:** `The certificate's CN name does not match the passed value`
**Solution:** Add `Encrypt=True;TrustServerCertificate=False;` to connection string

### **Issue 2: Connection String Not Found**
**Error:** `Connection string not found in configuration`
**Solution:** Configure connection string in Azure App Service settings

### **Issue 3: Authentication Failed**
**Error:** `Login failed for user 'admin123'`
**Solution:** Check username/password in Azure SQL Database

### **Issue 4: Firewall Blocked**
**Error:** `Cannot connect to server`
**Solution:** Allow Azure App Service IP in SQL Database firewall

## 🛠️ **Troubleshooting Steps**

### **1. Check Configuration**
```
GET /api/databasetest/config
```
This will show if your connection string is properly configured.

### **2. Test Basic Connection**
```
GET /api/databasetest/ef-test
```
This will test Entity Framework connectivity and show detailed error messages.

### **3. Test Raw SQL**
```
GET /api/databasetest/sql-test
```
This will test direct SQL connection and show server information.

### **4. Test Operations**
```
GET /api/databasetest/operations
```
This will test if your database tables are accessible.

## 📞 **Quick Commands**

### **Using curl:**
```bash
# Test Entity Framework connection
curl https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/ef-test

# Test raw SQL connection
curl https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/sql-test

# Check configuration
curl https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/config
```

### **Using PowerShell:**
```powershell
# Test Entity Framework connection
Invoke-RestMethod -Uri "https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/ef-test"

# Test raw SQL connection
Invoke-RestMethod -Uri "https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/databasetest/sql-test"
```

## 🔒 **Security Notes**

- Connection strings are masked in responses (passwords hidden)
- No sensitive data is logged or exposed
- Tests are read-only operations
- Controller can be removed after debugging

## 🎯 **Expected Results After Fix**

After fixing your connection string, you should see:
- ✅ All tests return "SUCCESS" status
- ✅ Connection string shows required parameters
- ✅ Database operations work correctly
- ✅ No error messages in details

**This controller will help you quickly identify and fix database connection issues!**
