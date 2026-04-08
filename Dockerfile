# Multi-stage build for .NET 10 web app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY ["TaskManagerPortfolio/TaskManagerPortfolio.csproj", "TaskManagerPortfolio/"]
RUN dotnet restore "TaskManagerPortfolio/TaskManagerPortfolio.csproj"

# copy everything else and publish
COPY . .
WORKDIR "/src/TaskManagerPortfolio"
RUN dotnet publish "TaskManagerPortfolio.csproj" -c Release -o /app/publish /p:UseAppHost=false

# runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "TaskManagerPortfolio.dll"]
