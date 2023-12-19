#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IdentityServer/PhoneBook.IdentityServer/PhoneBook.IdentityServer.csproj", "IdentityServer/PhoneBook.IdentityServer/"]
COPY ["Shared/PhoneBook.Shared/PhoneBook.Shared.csproj", "Shared/PhoneBook.Shared/"]
RUN dotnet restore "./IdentityServer/PhoneBook.IdentityServer/./PhoneBook.IdentityServer.csproj"
COPY . .
WORKDIR "/src/IdentityServer/PhoneBook.IdentityServer"
RUN dotnet build "./PhoneBook.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PhoneBook.IdentityServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBook.IdentityServer.dll"]