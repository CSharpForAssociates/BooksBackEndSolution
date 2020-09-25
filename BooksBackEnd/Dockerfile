#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BooksBackEnd/BooksBackEnd.csproj", "BooksBackEnd/"]
RUN dotnet restore "BooksBackEnd/BooksBackEnd.csproj"
COPY . .
WORKDIR "/src/BooksBackEnd"
RUN dotnet build "BooksBackEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BooksBackEnd.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BooksBackEnd.dll"]