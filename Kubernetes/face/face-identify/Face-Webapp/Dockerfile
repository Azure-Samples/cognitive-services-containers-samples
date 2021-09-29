FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS publish
WORKDIR /src
COPY * ./
COPY Face-Webapp/ ./Face-Webapp/
RUN dotnet publish Face-Webapp.sln -c Release -o /app

FROM  mcr.microsoft.com/dotnet/aspnet:2.1 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Face-Webapp.dll"]
