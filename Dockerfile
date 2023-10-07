FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["telegram-pythonized-bot/telegram-pythonized-bot.csproj", "telegram-pythonized-bot/"]
RUN dotnet restore "telegram-pythonized-bot/telegram-pythonized-bot.csproj"
COPY . .
WORKDIR "/src/telegram-pythonized-bot"
RUN dotnet build "telegram-pythonized-bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "telegram-pythonized-bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "telegram-pythonized-bot.dll"]
