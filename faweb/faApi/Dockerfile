#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


USER root
RUN apt-get update && apt-get install -y curl  
RUN curl -LO "https://storage.googleapis.com/kubernetes-release/release/$(curl -s https://storage.googleapis.com/kubernetes-release/release/stable.txt)/bin/linux/amd64/kubectl"
RUN chmod +x ./kubectl  
RUN mv ./kubectl /usr/local/bin 
COPY config .
RUN mkdir ~/.kube 
WORKDIR /root/.kube
COPY config .


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["faApi/faApi.csproj", "faApi/"]
RUN dotnet restore "faApi/faApi.csproj"
COPY . .
WORKDIR "/src/faApi"
RUN dotnet build "faApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "faApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "faApi.dll"]