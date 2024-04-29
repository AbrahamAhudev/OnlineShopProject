namespace OnlineShopProject.Server
{
    public class MySQLConfiguration
    {

        private string DB;

        private string Server;

        private string Port;

        private string User;

        private string Password;

        public string ConnectionString;

        public MySQLConfiguration()
        {
            DB = "onlineshop";

            Server = "localhost";

            Port = "3306";

            User = "root";

            Password = "toor";

            ConnectionString = $"server={Server};port={Port};user={User};password={Password};database={DB}";
            

        }
    }
}
