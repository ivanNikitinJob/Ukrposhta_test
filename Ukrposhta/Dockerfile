#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ukrposhta/Ukrposhta.Client.csproj", "Ukrposhta/"]
COPY ["Ukrposhta.Models/Ukrposhta.Models.csproj", "Ukrposhta.Models/"]
COPY ["Ukrposhta.Servises/Ukrposhta.Servises.csproj", "Ukrposhta.Servises/"]
COPY ["Ukrposhta.Entities/Ukrposhta.Entities.csproj", "Ukrposhta.Entities/"]
COPY ["Ukrposhta.Repositories/Ukrposhta.Repositories.csproj", "Ukrposhta.Repositories/"]
RUN dotnet restore "./Ukrposhta/Ukrposhta.Client.csproj"
COPY . .
WORKDIR "/src/Ukrposhta"
RUN dotnet build "./Ukrposhta.Client.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ukrposhta.Client.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ukrposhta.Client.dll"]