# CRM-API

EF Notes:
Run from the Infrastructure Project
dotnet ef migrations add InitialMigration -s ../CRM-API.WebApi/CRM-API.WebApi.csproj
dotnet ef database update -s ../CRM-API.WebAPI/CRM-API.WebApi.csproj