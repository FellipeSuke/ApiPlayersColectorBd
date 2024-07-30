namespace Rest.API.RequestPlayers.Util
{
    public class Settings
    {
        // Ler variáveis de ambiente
        public static bool devTest = false;
        public static string url;
        public static int apiTimer;
        public static string username;
        public static string password;
        public Settings()
        {   // Ler variáveis de ambiente
            if (devTest)
            {
                url = "http://201.14.75.202:8212/v1/api/players";
                apiTimer = 300;
                username = "admin";
                password = "unreal";
            }
            else
            {
                url = Environment.GetEnvironmentVariable("API_URL");
                apiTimer = Int32.Parse(Environment.GetEnvironmentVariable("API_TIMER_REQUEST") ?? "300");
                username = Environment.GetEnvironmentVariable("USERNAME");
                password = Environment.GetEnvironmentVariable("PASSWORD");
            }
            

            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || apiTimer == null)
            {
                Console.WriteLine("Variáveis de ambiente não configuradas corretamente.");
                return;
            }

        }
    }
}
