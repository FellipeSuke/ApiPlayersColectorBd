using MySql.Data.MySqlClient;
using Rest.API.RequestPlayers.Util;

namespace Rest.API.RequestPlayers.Repository
{
    public abstract class BaseRepository<T> : Settings
    {
        private string _connectionString;
        protected string ConnectionString => _connectionString;


        public BaseRepository()
        {
            if (devTest)
            {
                _connectionString = "Server=201.14.75.202;Database=db-palworld-pvp-insiderhub;User ID=PalAdm;Password=sukelord;Port=3306;SslMode=None;";

            }
            else
            {
                _connectionString =
                    $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +                    
                    $"Database={Environment.GetEnvironmentVariable("DB_DATABASE")};" +
                    $"User={Environment.GetEnvironmentVariable("DB_USER")};" +
                    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                    $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                    $"SslMode={Environment.GetEnvironmentVariable("DB_SSHMODE")};";
            }            
        }

    }
}

