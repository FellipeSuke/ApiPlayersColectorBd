# Use a imagem base do .NET 8 SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo de solução e restaura dependências conforme o necessário
COPY *.sln ./
COPY Rest.API.RequestPlayers/*.csproj ./Rest.API.RequestPlayers/
RUN dotnet restore

# Copia todo o código e compila a aplicação
COPY . .
WORKDIR /app/Rest.API.RequestPlayers
RUN dotnet publish -c Release -o out

# Use a imagem runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Rest.API.RequestPlayers/out .

# Instala o cliente MySQL (se necessário para testes/debugging)
# Note que você pode remover essa linha se não precisar do cliente MySQL no runtime
RUN apt-get update && \
    apt-get install -y default-mysql-client && \

    apt-get clean

# Define o ponto de entrada da aplicação
ENTRYPOINT ["dotnet", "Rest.API.RequestPlayers.dll"]