# Uses the official .NET SDK image to build the solution
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copies the main solution file
COPY ["BaeAPI.sln", "./"]

# Copies the .csproj files
COPY ["BaeServer/BaeServer.csproj", "BaeServer/"]
COPY ["BaeAiSystem/BaeAiSystem.csproj", "BaeAiSystem/"]
COPY ["BaeAuthentication/BaeAuthentication.csproj", "BaeAuthentication/"]
COPY ["BaeDB/BaeDB.csproj", "BaeDB/"]
COPY ["BeaIntegrations/BaeIntegrations.csproj", "BeaIntegrations/"]
COPY ["Integrations/BaeOpenAiIntegration/BaeOpenAiIntegration.csproj", "Integrations/BaeOpenAiIntegration/"]


# Clears NuGet cache
RUN dotnet nuget locals all --clear

# Restores the packages for the entire solution
RUN dotnet restore --verbosity normal

# Copies the rest of the solution's files
COPY . .

# Builds the solution
RUN dotnet build "BaeAPI.sln" -c Release --no-restore

# Publishes the API project
FROM build AS publish
RUN dotnet publish "BaeServer/BaeServer.csproj" -c Release --no-restore -o /app/publish

# Uses the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BaeAPI.dll"]