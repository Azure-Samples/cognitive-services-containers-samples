FROM microsoft/dotnet:2.1-sdk AS publish
WORKDIR /src
COPY * ./
COPY FrontendService/ ./FrontendService/
RUN dotnet publish FrontendService.sln -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FrontendService.dll"]
