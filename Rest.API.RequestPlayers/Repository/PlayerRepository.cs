using Dapper;
using MySql.Data.MySqlClient;
using Rest.API.RequestPlayers.Model;
using System.Text;

namespace Rest.API.RequestPlayers.Repository
{
    public class PlayerRepository : BaseRepository<PlayersResponse>
    {
        public async Task addPlayersAsync(PlayersResponse playersResponse)
        {
            Console.WriteLine("1");
            // Construir a consulta SQL
            var query = new StringBuilder();
            Console.WriteLine("2");
            try
            {
                using var connection = new MySqlConnection(ConnectionString);
                Console.WriteLine($"{ConnectionString}");
                Console.WriteLine("3");
                await connection.OpenAsync();
                Console.WriteLine("4");
                query.Append("INSERT INTO `db-palworld-pvp-insiderhub`.player_temp ");
                Console.WriteLine("5");
                query.Append("(player_name, account_name, player_id, gdk, ip, ping, location_x, location_y, player_lvl, created) VALUES ");
                Console.WriteLine("6");
                var parameters = new DynamicParameters();
                Console.WriteLine("7");
                for (int i = 0; i < playersResponse.players.Count; i++)
                {
                 
                    var player = playersResponse.players[i];

                    Console.WriteLine($"Player: {player.accountName} GDK: {player.userId}");
                    
                    query.Append($"(@PlayerName{i}, @AccountName{i}, @PlayerId{i}, @Gdk{i}, @Ip{i}, @Ping{i}, @LocationX{i}, @LocationY{i}, @PlayerLvl{i}, CURRENT_TIMESTAMP),");
                    
                    parameters.Add($"@PlayerName{i}", player.name);
                    parameters.Add($"@AccountName{i}", player.accountName);
                    parameters.Add($"@PlayerId{i}", player.playerId);
                    parameters.Add($"@Gdk{i}", player.userId);
                    parameters.Add($"@Ip{i}", player.ip);
                    parameters.Add($"@Ping{i}", player.ping);
                    parameters.Add($"@LocationX{i}", player.location_x);
                    parameters.Add($"@LocationY{i}", player.location_y);
                    parameters.Add($"@PlayerLvl{i}", player.level);
                }
                Console.WriteLine("8");
                // Remove a última vírgula e adiciona a cláusula ON DUPLICATE KEY UPDATE
                query.Length--;
                query.Append(" ON DUPLICATE KEY UPDATE ");
                query.Append("player_name = VALUES(player_name), ");
                query.Append("account_name = VALUES(account_name), ");
                query.Append("gdk = VALUES(gdk), ");
                query.Append("ip = VALUES(ip), ");
                query.Append("ping = VALUES(ping), ");
                query.Append("location_x = VALUES(location_x), ");
                query.Append("location_y = VALUES(location_y), ");
                query.Append("player_lvl = VALUES(player_lvl), ");
                query.Append("created = VALUES(created);");
                Console.WriteLine("9");
                // Executar a consulta
                await connection.ExecuteAsync(query.ToString(), parameters);
                Console.WriteLine("10");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: Query: {query.ToString()} ==>> {ex.Message}");
            }
           
        }

    }
}
