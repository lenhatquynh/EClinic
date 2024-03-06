#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Project.IdentityService/Project.IdentityService.csproj", "Project.IdentityService/"]
COPY ["Project.Core/Project.Core.csproj", "Project.Core/"]
COPY ["Project.Common/Project.Common.csproj", "Project.Common/"]
COPY ["Project.Data/Project.Data.csproj", "Project.Data/"]
RUN dotnet restore "Project.IdentityService/Project.IdentityService.csproj"
COPY . .
WORKDIR "/src/Project.IdentityService"
RUN dotnet build "Project.IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.IdentityService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.IdentityService.dll"]

