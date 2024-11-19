using Eto.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace Huisart_Eto;

public class PatientenService
{
    public List<Patienten> GetPatienten()
    {
        var connectString = "Server=localhost;Database=Huisarts;Uid=root;Pwd=;";
        var connection = new MySqlConnection(connectString);
        var patienten = new List<Patienten>();
        

        try
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM person";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                patienten.Add(new Patienten
                {
                    patient_id = reader.GetInt32("patient_id"),
                    first_name = reader.GetString("first_name"),
                    last_name = reader.GetString("last_name"),
                    email = reader.GetString("email"),
                    phone = reader.GetString("phone"),
                    postcode = reader.GetString("postcode"),
                    adress = reader.GetString("adress"),
                    place = reader.GetString("place"),
                    notitie = reader.GetString("notitie")
                });
            }
        }
        catch (MySqlException ex)
        {
            // Log or handle the exception as needed
            MessageBox.Show("Database error: " + ex.Message);
        }
        finally
        {
            connection.Close();
        }
        
        return patienten;
    }
}