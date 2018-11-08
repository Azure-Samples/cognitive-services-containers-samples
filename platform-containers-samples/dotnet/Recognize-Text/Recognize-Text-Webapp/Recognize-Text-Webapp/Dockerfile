FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR /src
COPY * ./
COPY Recognize-Text-Webapp/ ./Recognize-Text-Webapp/
RUN dotnet publish Recognize-Text-Webapp.sln -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Recognize-Text-Webapp.dll"]
