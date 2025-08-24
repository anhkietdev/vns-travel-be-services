# Azure Deployment Guide for VNS Travel API

## Issues Fixed

1. ✅ **Swagger now enabled in Production** - Modified `Program.cs` to enable Swagger in all environments
2. ✅ **Added Health Check endpoints** - Created `/api/health` and `/api/health/ping` for testing
3. ✅ **Enhanced Swagger documentation** - Better API descriptions and contact information
4. ✅ **Root URL redirect** - Now redirects to Swagger when accessing the root URL

## Step-by-Step Azure Deployment

### 1. Database Setup (Azure SQL Database)

1. Create an Azure SQL Database in your Azure portal
2. Get the connection string from Azure SQL Database > Connection strings
3. Update `appsettings.Production.json` with your Azure SQL connection string

### 2. Update Production Configuration

Replace the placeholders in `appsettings.Production.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:your-server.database.windows.net,1433;Initial Catalog=your-database;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
    "SqlServerConnection": "Server=tcp:your-server.database.windows.net,1433;Initial Catalog=your-database;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
     "JWT": {
     "ValidAudience": "https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net",
     "ValidIssuer": "https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net",
     "Secret": "your-production-jwt-secret-key-here"
   }
}
```

### 3. Deploy to Azure

#### Option A: Using Azure CLI
```bash
# Install Azure CLI if not already installed
# Login to Azure
az login

# Create a resource group
az group create --name vns-travel-rg --location eastus

# Create an App Service plan
az appservice plan create --name vns-travel-plan --resource-group vns-travel-rg --sku B1

# Create a web app
az webapp create --name your-app-name --resource-group vns-travel-rg --plan vns-travel-plan --runtime "DOTNETCORE:8.0"

# Deploy your code
az webapp deployment source config-local-git --name your-app-name --resource-group vns-travel-rg
```

#### Option B: Using Visual Studio
1. Right-click on the `Presentation` project
2. Select "Publish"
3. Choose "Azure" as the target
4. Select your Azure subscription and create/select an App Service
5. Configure the deployment settings
6. Publish

#### Option C: Using GitHub Actions
Create `.github/workflows/deploy.yml`:

```yaml
name: Deploy to Azure
on:
  push:
    branches: [ main ]

jobs:
  deploy:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v2
    
    - name: Setup .NET
      uses: actions/setup-dotnet@v1
      with:
        dotnet-version: 8.0.x
        
    - name: Build
      run: dotnet build --configuration Release
      
    - name: Publish
      run: dotnet publish -c Release -o ./publish
      
    - name: Deploy to Azure
      uses: azure/webapps-deploy@v2
      with:
        app-name: 'your-app-name'
        publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
        package: ./publish
```

### 4. Configure Environment Variables in Azure

In Azure Portal > Your App Service > Configuration > Application settings, add:

```
ASPNETCORE_ENVIRONMENT = Production
ConnectionStrings__DefaultConnection = your-azure-sql-connection-string
JWT__Secret = your-production-jwt-secret
JWT__ValidAudience = https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net
JWT__ValidIssuer = https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net
```

### 5. Test Your Deployment

After deployment, test these URLs:

1. **Root URL**: `https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/`
   - Should redirect to Swagger

2. **Swagger UI**: `https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/swagger`
   - Should show your API documentation

3. **Health Check**: `https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/health`
   - Should return status information

4. **Ping Test**: `https://vns-travel-services-g6aebrg5brhdfqex.southeastasia-01.azurewebsites.net/api/health/ping`
   - Should return "pong"

## Available API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh-token` - Refresh JWT token
- `POST /api/auth/forgot-password` - Send password reset OTP
- `POST /api/auth/verify-otp` - Verify OTP
- `POST /api/auth/reset-password` - Reset password

### Services
- `GET /api/service` - Get all services
- `POST /api/service` - Create new service
- `GET /api/service/{id}` - Get service by ID
- `PUT /api/service/{id}` - Update service
- `DELETE /api/service/{id}` - Delete service

### Bookings
- `GET /api/booking` - Get all bookings
- `POST /api/booking` - Create new booking
- `GET /api/booking/{id}` - Get booking by ID
- `PUT /api/booking/{id}` - Update booking
- `DELETE /api/booking/{id}` - Delete booking

### Chat
- `GET /api/chat` - Get chat messages
- `POST /api/chat` - Send message

### Health
- `GET /api/health` - Health check
- `GET /api/health/ping` - Simple ping test

## Troubleshooting

### Common Issues:

1. **"No content" error**: 
   - Check if Swagger is enabled in production
   - Verify the root URL redirect is working
   - Check application logs in Azure Portal

2. **Database connection errors**:
   - Verify Azure SQL connection string
   - Check if Azure SQL firewall allows your app service IP
   - Ensure database exists and is accessible

3. **JWT authentication issues**:
   - Verify JWT secret is set in Azure configuration
   - Check ValidAudience and ValidIssuer match your app URL

4. **CORS issues**:
   - The current configuration allows all origins
   - For production, consider restricting to specific domains

### Check Logs:
- Azure Portal > Your App Service > Log stream
- Azure Portal > Your App Service > Logs > Application logs

## Security Recommendations

1. **Use Azure Key Vault** for sensitive configuration
2. **Enable HTTPS only** in Azure App Service
3. **Restrict CORS** to specific domains in production
4. **Use strong JWT secrets** (at least 32 characters)
5. **Enable Azure Application Insights** for monitoring

## Next Steps

1. Set up Azure Application Insights for monitoring
2. Configure custom domain and SSL certificate
3. Set up CI/CD pipeline with GitHub Actions
4. Implement rate limiting
5. Add API versioning
6. Set up automated testing
