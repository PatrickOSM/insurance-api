##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install cultures (same approach as Alpine SDK image)
RUN apk add --no-cache icu-libs

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["src/Insurance.Api/Insurance.Api.csproj", "src/Insurance.Api/"]
COPY ["src/Insurance.Api.Application/Insurance.Api.Application.csproj", "src/Insurance.Api.Application/"]
COPY ["src/Insurance.Api.Domain/Insurance.Api.Domain.csproj", "src/Insurance.Api.Domain/"]
COPY ["src/Insurance.Api.Domain.Core/Insurance.Api.Domain.Core.csproj", "src/Insurance.Api.Domain.Core/"]
COPY ["src/Insurance.Api.Infrastructure/Insurance.Api.Infrastructure.csproj", "src/Insurance.Api.Infrastructure/"]
COPY . .
WORKDIR "/src/src/Insurance.Api"

FROM build AS publish
RUN dotnet publish "Insurance.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insurance.Api.dll"]