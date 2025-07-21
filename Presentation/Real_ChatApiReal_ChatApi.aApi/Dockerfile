# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# EF CLI ekle
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY ["Presentation/Real_ChatApiReal_ChatApi.aApi/Real_ChatApiReal_ChatApi.aApi.csproj", "Presentation/Real_ChatApiReal_ChatApi.aApi/"]
COPY ["Core/Real_ChatApi.Domain/Real_ChatApi.Domain.csproj", "Core/Real_ChatApi.Domain/"]
COPY ["Core/Real_ChatApi.Application/Real_ChatApi.Application.csproj", "Core/Real_ChatApi.Application/"]
COPY ["Infrastructure/Real_ChatApi.Infrastructure/Real_ChatApi.Infrastructure.csproj", "Infrastructure/Real_ChatApi.Infrastructure/"]

COPY ["Presentation/Real_ChatApiReal_ChatApi.aApi/Real_ChatApiReal_ChatApi.aApi.csproj", "Presentation/Real_ChatApiReal_ChatApi.aApi/"]
RUN dotnet restore "Presentation/Real_ChatApiReal_ChatApi.aApi/Real_ChatApiReal_ChatApi.aApi.csproj"
COPY . .
WORKDIR "/src/Presentation/Real_ChatApiReal_ChatApi.aApi"
RUN dotnet build "Real_ChatApiReal_ChatApi.aApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Real_ChatApiReal_ChatApi.aApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Real_ChatApiReal_ChatApi.aApi.dll"]