using Rest.API.RequestPlayers.Model;
using Rest.API.RequestPlayers.Repository;
using Rest.API.RequestPlayers.Util;
using RestSharp;
using RestSharp.Authenticators;
using System.Text.Json;

namespace Rest.API.RequestPlayers
{
    class Program : Settings
    {
        static async Task Main(string[] args)
        {
            Settings settings = new Settings();

            // Configurar as opções do RestClient, incluindo o autenticador
            var options = new RestClientOptions(url)
            {
                Authenticator = new HttpBasicAuthenticator(username, password)
            };

            // Criar uma instância do cliente RestSharp com as opções
            var client = new RestClient(options);

            // Criar a requisição GET
            var request = new RestRequest(Method.Get.ToString());

            // Loop para realizar requisição a cada 100ms
            while (true)
            {
                try
                {
                    // Executar a requisição de forma assíncrona
                    var response = await client.ExecuteAsync(request);

                    // Verificar o resultado da requisição
                    if (response.IsSuccessful)
                    {
                        // Desserializar o JSON para o modelo
                        var playersResponse = JsonSerializer.Deserialize<PlayersResponse>(response.Content);
                        if (playersResponse != null)
                        {
                            PlayerRepository playerRepository = new PlayerRepository();
                            await playerRepository.addPlayersAsync(playersResponse);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Erro: {response.StatusCode} - {response.ErrorMessage}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exceção: {ex.Message}");
                }

                // Aguardar antes de realizar a próxima requisição
                await Task.Delay(apiTimer);
            }
        }
    }
}