# Use a imagem base do .NET 8 SDK para compilar a aplica��o
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo de solu��o e restaura depend�ncias conforme o necess�rio
COPY *.sln ./
COPY Rest.API.RequestPlayers/*.csproj ./Rest.API.RequestPlayers/
RUN dotnet restore

# Copia todo o c�digo e compila a aplica��o
COPY . .
WORKDIR /app/Rest.API.RequestPlayers
RUN dotnet publish -c Release -o out

# Use a imagem runtime para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/Rest.API.RequestPlayers/out .

# Instala o cliente MySQL (se necess�rio para testes/debugging)
# Note que voc� pode remover essa linha se n�o precisar do cliente MySQL no runtime
RUN apt-get update && \
    apt-get install -y default-mysql-client && \

    apt-get clean

# Define o ponto de entrada da aplica��o
ENTRYPOINT ["dotnet", "Rest.API.RequestPlayers.dll"]