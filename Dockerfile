FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EvidenceKnih/EvidenceKnih.csproj", "EvidenceKnih/"]
RUN dotnet restore "EvidenceKnih/EvidenceKnih.csproj"
COPY . .
WORKDIR "/src/EvidenceKnih"
RUN dotnet build "EvidenceKnih.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EvidenceKnih.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EvidenceKnih.dll"]
