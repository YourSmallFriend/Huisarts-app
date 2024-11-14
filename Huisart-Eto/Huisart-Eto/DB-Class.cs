namespace Huisart_Eto;
using MySql.Data.MySqlClient;
public class DB_Class
{
    //hier maak ik een connectie aan met de database
    public MySqlConnection Connect()
    {
        var connectString = "Server=localhost;Database=Huisarts;Uid=root;Pwd=;";
        var connection = new MySqlConnection(connectString);
        return connection;
    }
}