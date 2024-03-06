#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Project.ForumService/Project.ForumService.csproj", "Project.ForumService/"]
COPY ["Project.Core/Project.Core.csproj", "Project.Core/"]
COPY ["Project.Common/Project.Common.csproj", "Project.Common/"]
COPY ["Project.Data/Project.Data.csproj", "Project.Data/"]
RUN dotnet restore "Project.ForumService/Project.ForumService.csproj"
COPY . .
WORKDIR "/src/Project.ForumService"
RUN dotnet build "Project.ForumService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Project.ForumService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project.ForumService.dll"]