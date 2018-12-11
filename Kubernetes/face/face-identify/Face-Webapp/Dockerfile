FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR /src
COPY * ./
COPY Face-Webapp/ ./Face-Webapp/
RUN dotnet publish Face-Webapp.sln -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Face-Webapp.dll"]
