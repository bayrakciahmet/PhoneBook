#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Person/PhoneBook.Services.Person/PhoneBook.Services.Person.csproj", "Services/Person/PhoneBook.Services.Person/"]
COPY ["Shared/PhoneBook.Shared/PhoneBook.Shared.csproj", "Shared/PhoneBook.Shared/"]
RUN dotnet restore "./Services/Person/PhoneBook.Services.Person/./PhoneBook.Services.Person.csproj"
COPY . .
WORKDIR "/src/Services/Person/PhoneBook.Services.Person"
RUN dotnet build "./PhoneBook.Services.Person.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PhoneBook.Services.Person.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneBook.Services.Person.dll"]