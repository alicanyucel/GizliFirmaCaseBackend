FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gizli.WebAPI/Gizli.WebAPI.csproj", "Gizli.WebAPI/"]
COPY ["../Gizli.Application/Gizli.Application.csproj", "Gizli.Application/"]
COPY ["../Gizli.Infrastructure/Gizli.Infrastructure.csproj", "Gizli.Infrastructure/"]
COPY ["../Gizli.Domain/Gizli.Domain.csproj", "Gizli.Domain/"]
RUN dotnet restore "Gizli.WebAPI/Gizli.WebAPI.csproj"
COPY . .
WORKDIR /src/Gizli.WebAPI
RUN dotnet publish "Gizli.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Gizli.WebAPI.dll"]
