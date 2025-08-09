
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Копируем все файлы проекта сразу345
COPY . ./
RUN dotnet restore

# Компилируем проект
RUN dotnet publish -c Release -o out

# Образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

# Замените 'YourProjectName' на имя вашего проекта
ENTRYPOINT ["dotnet", "web_api_postgres/web_api_postgres.dll"]