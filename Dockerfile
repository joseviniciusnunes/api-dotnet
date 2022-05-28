FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder

WORKDIR /app

COPY . .

RUN dotnet restore && \
    export PATH="$PATH:/root/.dotnet/tools" && \
    dotnet tool install --global dotnet-ef && \
    dotnet ef database update && \
    dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=builder /app/out .
COPY --from=builder /app/app.db .

ENTRYPOINT ["dotnet", "api-dotnet.dll"]