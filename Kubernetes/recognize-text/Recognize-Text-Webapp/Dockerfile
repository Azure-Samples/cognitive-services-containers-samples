# In the build stage, build the image with the full dotnet sdk
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS publish
WORKDIR /src
COPY * ./
COPY Recognize-Text-Webapp/ ./Recognize-Text-Webapp/
RUN dotnet publish Recognize-Text-Webapp.sln -c Release -o /app

# In the final stage, copy over only the necessary binaries from the build stage to be run on the reduced asp.net core runtime
FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Recognize-Text-Webapp.dll"]
