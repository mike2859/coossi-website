# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY Coossi.Blazor.csproj ./
RUN dotnet restore Coossi.Blazor.csproj
COPY . .
RUN dotnet publish Coossi.Blazor.csproj -c Release -o /app /p:UseAppHost=false

# Runtime (utilisateur non-root "app" fourni par l'image, écoute sur 8080)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
# Clés Data Protection (antiforgery, circuits Blazor) : dossier monté en volume
# pour survivre aux redémarrages du conteneur
RUN mkdir -p /home/app/.aspnet/DataProtection-Keys && chown -R app:app /home/app/.aspnet
COPY --from=build /app .
USER app
ENTRYPOINT ["dotnet", "Coossi.Blazor.dll"]
