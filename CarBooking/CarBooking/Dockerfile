# was automatically crewated when you select Docker Support in project creation

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CarBooking/CarBooking.csproj", "CarBooking/"]
RUN dotnet restore "CarBooking/CarBooking.csproj"
COPY . .
WORKDIR "/src/CarBooking"
RUN dotnet build "CarBooking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarBooking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarBooking.dll"]