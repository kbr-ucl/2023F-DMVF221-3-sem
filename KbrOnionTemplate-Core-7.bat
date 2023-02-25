set /p "app=Enter application name: "
echo Creating %app%
pause 
if not exist %app% mkdir %app%
cd %app%
rem dotnet new mvc -f net7.0 -au Individual -uld -o %app%.Mvc
dotnet new webapi -f net7.0 -au None -o %app%.Api
dotnet new classlib -f net7.0 -o %app%.Application
dotnet new classlib -f net7.0 -o %app%.Domain
dotnet new xunit -f net7.0 -o %app%.Domain.Test
dotnet new classlib -f net7.0 -o %app%.Infrastructure
dotnet new classlib -f net7.0 -o %app%.DatabaseMigration

dotnet add %app%.Api reference %app%.Application
dotnet add %app%.Api reference %app%.Domain
dotnet add %app%.Api reference %app%.Infrastructure
dotnet add %app%.Api reference %app%.DatabaseMigration
dotnet add %app%.Domain.Test reference %app%.Domain
dotnet add %app%.Application reference %app%.Domain
dotnet add %app%.Infrastructure reference %app%.Domain
dotnet add %app%.Infrastructure reference %app%.Application
dotnet add %app%.DatabaseMigration reference %app%.Infrastructure

dotnet new sln -n %app%
rem dotnet sln %app%.sln add %app%.Mvc/%app%.Mvc.csproj
dotnet sln %app%.sln add %app%.Api/%app%.Api.csproj
dotnet sln %app%.sln add %app%.Application/%app%.Application.csproj
dotnet sln %app%.sln add %app%.Domain/%app%.Domain.csproj
dotnet sln %app%.sln add %app%.Domain.Test/%app%.Domain.Test.csproj
dotnet sln %app%.sln add %app%.Infrastructure/%app%.Infrastructure.csproj
dotnet sln %app%.sln add %app%.DatabaseMigration/%app%.DatabaseMigration.csproj


dotnet add %app%.Api/%app%.Api.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add %app%.Api/%app%.Api.csproj package Microsoft.EntityFrameworkCore
dotnet add %app%.Api/%app%.Api.csproj package Microsoft.EntityFrameworkCore.SqlServer
dotnet add %app%.Api/%app%.Api.csproj package Microsoft.EntityFrameworkCore.Tools

dotnet add %app%.DatabaseMigration/%app%.DatabaseMigration.csproj package Microsoft.EntityFrameworkCore
dotnet add %app%.DatabaseMigration/%app%.DatabaseMigration.csproj package Microsoft.EntityFrameworkCore.SqlServer

dotnet add %app%.Infrastructure/%app%.Infrastructure.csproj package AutoMapper
dotnet add %app%.Infrastructure/%app%.Infrastructure.csproj package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add %app%.Infrastructure/%app%.Infrastructure.csproj package Microsoft.EntityFrameworkCore
dotnet add %app%.Infrastructure/%app%.Infrastructure.csproj package Microsoft.EntityFrameworkCore.Relational
pause
