#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApp10/WebApp10.csproj", "WebApp10/"]
RUN dotnet restore "WebApp10/WebApp10.csproj"
COPY . .
WORKDIR "/src/WebApp10"
RUN dotnet build "WebApp10.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp10.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApp10.dll"]