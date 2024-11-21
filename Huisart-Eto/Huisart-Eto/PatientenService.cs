// Huisart-Eto/Huisart-Eto/PatientenService.cs
using Eto.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Huisart_Eto
{
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
                    var patient = new Patienten
                    {
                        patient_id = reader.GetInt32("patient_id"),
                        first_name = reader.GetString("first_name"),
                        last_name = reader.GetString("last_name"),
                        email = reader.GetString("email"),
                        phone = reader.GetString("phone"),
                        postcode = reader.GetString("postcode"),
                        adress = reader.GetString("adress"),
                        place = reader.GetString("place"),
                        notitie = reader.GetString("notitie"),
                        isDeleted = reader.GetBoolean("isDeleted")
                    };
                    patienten.Add(patient);
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

        public void AddPatient(Patienten patient)
        {
            var connectString = "Server=localhost;Database=Huisarts;Uid=root;Pwd=;";
            var connection = new MySqlConnection(connectString);

            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO person (first_name, last_name, email, phone, postcode, adress, place, notitie, isDeleted) VALUES (@first_name, @last_name, @Email, @Phone, @Postcode, @Adress, @Place, @Notitie, @IsDeleted)";
                command.Parameters.AddWithValue("@first_name", patient.first_name);
                command.Parameters.AddWithValue("@last_name", patient.last_name);
                command.Parameters.AddWithValue("@Email", patient.email);
                command.Parameters.AddWithValue("@Phone", patient.phone);
                command.Parameters.AddWithValue("@Postcode", patient.postcode);
                command.Parameters.AddWithValue("@Adress", patient.adress);
                command.Parameters.AddWithValue("@Place", patient.place);
                command.Parameters.AddWithValue("@Notitie", patient.notitie);
                command.Parameters.AddWithValue("@IsDeleted", patient.isDeleted);

                command.ExecuteNonQuery();
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
        }
    }
}