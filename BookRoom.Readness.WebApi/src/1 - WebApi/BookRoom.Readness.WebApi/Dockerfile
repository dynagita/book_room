#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BookRoom.Readness.WebApi/src/1 - WebApi/BookRoom.Readness.WebApi/BookRoom.Readness.WebApi.csproj", "BookRoom.Readness.WebApi/src/1 - WebApi/BookRoom.Readness.WebApi/"]
RUN dotnet restore "BookRoom.Readness.WebApi/src/1 - WebApi/BookRoom.Readness.WebApi/BookRoom.Readness.WebApi.csproj"
COPY . .
WORKDIR "/src/BookRoom.Readness.WebApi/src/1 - WebApi/BookRoom.Readness.WebApi"
RUN dotnet build "BookRoom.Readness.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookRoom.Readness.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookRoom.Readness.WebApi.dll"]