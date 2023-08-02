FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80 443 5000 5001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . .

FROM build as publish
WORKDIR Presentation/KanbanBackend/KanbanBackend
RUN dotnet publish "KanbanBackend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "KanbanBackend.dll"]
