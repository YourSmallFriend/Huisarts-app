using System;
using Eto.Drawing;
using Eto.Forms;
using MySql.Data.MySqlClient;

namespace Huisart_Eto;

public class PatientForm : Form
{
    public PatientForm(Patienten selectedPatient)
    {
        Title = "Patienten";
        BackgroundColor = Color.FromArgb(50, 50, 50);
        Maximize();

        //laat de naam van de patient zien
        var naamLabel = new Label
        {
            Text = selectedPatient.first_name + " " + selectedPatient.last_name,
            TextColor = Colors.White,
            Font = new Font("Arial", 20, FontStyle.Bold)
        };

        //text label waarin ik noties kan zetten over de patient
        var notitieLabel = new TextBox()
        {
            TextColor = Colors.Black,
            Font = new Font("Arial", 16),
            Text = selectedPatient.notitie,
            Size = new Size(500, 500)
        };

        //button om de notitie op te slaan in de database
        var saveButton = new Button
        {
            Text = "Save",
            Command = new Command((sender, e) =>
            {
                var connectString = "Server=localhost;Database=Huisarts;Uid=root;Pwd=;";
                var connection = new MySqlConnection(connectString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE person SET notitie = @notitie WHERE patient_id = @patient_id";
                command.Parameters.AddWithValue("@notitie", notitieLabel.Text);
                command.Parameters.AddWithValue("@patient_id", selectedPatient.patient_id);
                command.ExecuteNonQuery();
                connection.Close();
                
                //  voeg de notitie ook toe aan de patient in de lijst
                selectedPatient.notitie = notitieLabel.Text;
                
                MessageBox.Show("Notitie opgeslagen");
                
                // Refresh the data in ApplicatieForm
                var applicatieForm = Application.Instance.MainForm as ApplicatieForm;
                applicatieForm?.RefreshData();
            })
        };
        
        
        
        //verwijder de patient in form van een soft delete
        var deleteButton = new Button
        {
            Text = "Delete patient",
            Command = new Command((sender, e) =>
            {
                try
                {
                    var connectString = "Server=localhost;Database=Huisarts;Uid=root;Pwd=;";
                    var connection = new MySqlConnection(connectString);
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE person SET isDeleted = 1 WHERE patient_id = @patient_id";
                    command.Parameters.AddWithValue("@patient_id", selectedPatient.patient_id);
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO deleted_patients (patient_id, deleted_at) VALUES (@patient_id, @deleted_at)";
                    command.Parameters.AddWithValue("@deleted_at", DateTime.Now);
                    command.ExecuteNonQuery();

                    connection.Close();

                    MessageBox.Show("Patient verwijderd");

                    // Refresh the data in ApplicatieForm
                    var applicatieForm = Application.Instance.MainForm as ApplicatieForm;
                    applicatieForm?.RefreshData();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            })
        };
        Content = new StackLayout
        {
            Items =
            {
                naamLabel,
                notitieLabel,
                saveButton,
                deleteButton
            }
        };

        var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
        quitCommand.Executed += (sender, e) => Application.Instance.Quit();
        Menu = new MenuBar
        {
            ApplicationItems =
            {
                new ButtonMenuItem { Text = "&Preferences..." },
            },
            QuitItem = quitCommand,
        };
    }
}