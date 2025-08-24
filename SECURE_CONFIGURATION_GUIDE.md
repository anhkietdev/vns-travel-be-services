# Secure Configuration Management Guide

## 🔒 **Why Not Put Connection Strings in appsettings.Production.json?**

- **Security Risk**: Connection strings in source code are visible to anyone with access to your repository
- **Version Control**: Sensitive data gets stored in Git history
- **Deployment Issues**: Different environments need different connection strings
- **Compliance**: Many security standards require secrets to be managed separately

## 🛡️ **Secure Configuration Options**

### **Option 1: Azure App Service Configuration (Recommended)**

This is the most common and secure approach for Azure deployments.

#### **Step 1: Configure in Azure Portal**

1. Go to your Azure App Service
2. Navigate to **Configuration** > **Application settings**
3. Add these settings:

```
Name: ConnectionStrings__DefaultConnection
Value: Server=tcp:your-server.database.windows.net,1433;Initial Catalog=your-database;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

Name: ConnectionStrings__SqlServerConnection  
Value: Server=tcp:your-server.database.windows.net,1433;Initial Catalog=your-database;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

Name: JWT__Secret
Value: your-very-long-and-secure-jwt-secret-key-here

Name: ASPNETCORE_ENVIRONMENT
Value: Production
```

#### **Step 2: Using Azure CLI**

```bash
# Set connection string
az webapp config appsettings set --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group --settings ConnectionStrings__DefaultConnection="Server=tcp:your-server.database.windows.net,1433;Initial Catalog=your-database;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Set JWT secret
az webapp config appsettings set --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group --settings JWT__Secret="your-very-long-and-secure-jwt-secret-key-here"

# Set environment
az webapp config appsettings set --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group --settings ASPNETCORE_ENVIRONMENT="Production"
```

### **Option 2: Azure Key Vault (Enterprise Level)**

For enhanced security, use Azure Key Vault to store secrets.

#### **Step 1: Create Key Vault**

```bash
# Create Key Vault
az keyvault create --name vns-travel-keyvault --resource-group your-resource-group --location southeastasia

# Store connection string as secret
az keyvault secret set --vault-name vns-travel-keyvault --name "ConnectionStrings--DefaultConnection" --value "Server=tcp:your-server.database.windows.net,1433;Initial Catalog=your-database;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Store JWT secret
az keyvault secret set --vault-name vns-travel-keyvault --name "JWT--Secret" --value "your-very-long-and-secure-jwt-secret-key-here"
```

#### **Step 2: Configure App Service to Access Key Vault**

```bash
# Get the managed identity principal ID
az webapp identity assign --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group

# Grant access to Key Vault
az keyvault set-policy --name vns-travel-keyvault --object-id <managed-identity-principal-id> --secret-permissions get list
```

#### **Step 3: Update Program.cs**

Add this to your `Program.cs`:

```csharp
// Add Azure Key Vault configuration
if (builder.Environment.IsProduction())
{
    var keyVaultUrl = builder.Configuration["KeyVault:Url"];
    if (!string.IsNullOrEmpty(keyVaultUrl))
    {
        var credential = new DefaultAzureCredential();
        var secretClient = new SecretClient(new Uri(keyVaultUrl), credential);
        builder.Configuration.AddAzureKeyVault(secretClient, new Azure.KeyVault.Configuration.AzureKeyVaultConfigurationOptions());
    }
}
```

### **Option 3: User Secrets (Development Only)**

For local development, use User Secrets:

```bash
# Initialize user secrets
dotnet user-secrets init --project Presentation

# Add secrets
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-local-connection-string" --project Presentation
dotnet user-secrets set "JWT:Secret" "your-local-jwt-secret" --project Presentation
```

## 📋 **Current Configuration Status**

### **✅ What's Already Secure:**

1. **appsettings.Production.json** - Connection strings and secrets are now empty
2. **JWT Audience/Issuer** - Set to your Azure domain
3. **Environment Variables** - Will be set in Azure App Service

### **🔧 What You Need to Configure:**

1. **Azure App Service Settings** - Add your actual connection string and JWT secret
2. **Database Connection** - Ensure your Azure SQL Database is accessible
3. **Firewall Rules** - Allow your App Service to connect to SQL Database

## 🚀 **Deployment Checklist**

### **Before Deploying:**

- [ ] Remove any sensitive data from source code
- [ ] Set up Azure App Service Configuration
- [ ] Configure Azure SQL Database firewall
- [ ] Test connection string locally (if possible)

### **After Deploying:**

- [ ] Test health endpoint: `/api/health`
- [ ] Test Swagger UI: `/swagger`
- [ ] Verify database connection
- [ ] Check application logs for errors

## 🔍 **Testing Your Configuration**

### **Test Connection String:**

```bash
# Test from Azure CLI
az webapp config appsettings list --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group --query "[?name=='ConnectionStrings__DefaultConnection']"
```

### **Test Health Endpoint:**

```bash
curl https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/health
```

## 🛡️ **Security Best Practices**

1. **Never commit secrets to source control**
2. **Use strong, unique secrets** (at least 32 characters for JWT)
3. **Rotate secrets regularly**
4. **Use managed identities** when possible
5. **Enable Azure Key Vault** for enterprise applications
6. **Monitor access logs** for suspicious activity

## 📞 **Quick Commands**

### **Set Connection String:**
```bash
az webapp config appsettings set --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group --settings ConnectionStrings__DefaultConnection="your-connection-string"
```

### **Set JWT Secret:**
```bash
az webapp config appsettings set --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group --settings JWT__Secret="your-jwt-secret"
```

### **View Current Settings:**
```bash
az webapp config appsettings list --name vns-travel-services-g6aebrg5brhdfqex --resource-group your-resource-group
```

## 🆘 **Troubleshooting**

### **Common Issues:**

1. **Connection String Format**: Ensure proper escaping of special characters
2. **Firewall Rules**: Azure SQL Database must allow your App Service IP
3. **Authentication**: Use SQL Authentication, not Windows Authentication
4. **SSL**: Ensure `Encrypt=True` is set for Azure SQL

### **Check Logs:**
- Azure Portal > App Service > Log stream
- Azure Portal > App Service > Logs > Application logs
