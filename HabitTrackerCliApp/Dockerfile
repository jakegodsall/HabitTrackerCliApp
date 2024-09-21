FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HabitTrackerCliApp/HabitTrackerCliApp.csproj", "HabitTrackerCliApp/"]
RUN dotnet restore "HabitTrackerCliApp/HabitTrackerCliApp.csproj"
COPY . .
WORKDIR "/src/HabitTrackerCliApp"
RUN dotnet build "HabitTrackerCliApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HabitTrackerCliApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HabitTrackerCliApp.dll"]
