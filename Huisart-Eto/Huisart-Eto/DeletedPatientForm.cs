using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using MySql.Data.MySqlClient;

namespace Huisart_Eto
{
    public class DeletedPatientForm : Form
    {
        private GridView gridView;

        public DeletedPatientForm()
        {
            Title = "Verwijderde patienten";
            BackgroundColor = Color.FromArgb(50, 50, 50);
            Maximize();

            //haal de data op uit de database waar de patienten in de table DeletedPatients staan
            var patientenService = new PatientenService();
            var patienten = patientenService.GetPatienten();
            var deletedPatienten = patienten.FindAll(p => p.isDeleted);

            //haal nu de datetime op van de deleted_at en zet deze in de deletedPatienten
            var connectString = "Server=localhost;Database=Huisarts;Uid=root;Pwd=;";
            var connection = new MySqlConnection(connectString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT patient_id, deleted_at FROM deleted_patients WHERE isDeleted = 1";
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var patientId = reader.GetInt32("patient_id");
                var deletedAt = reader.GetDateTime("deleted_at").ToString();

                var patient = deletedPatienten.Find(p => p.patient_id == patientId);
                if (patient != null)
                {
                    patient.deleted_at = deletedAt;
                }
            }
            connection.Close();

            //datagridview waar de patienten in komen te staan die op isDeleted = 1 staan
            gridView = new GridView() { AllowMultipleSelection = false };
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Voornaam",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(r => r.first_name) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Achternaam",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(r => r.last_name) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Deleted At",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(r => r.deleted_at) }
            });

            //knop om de patienten te herstellen
            var restoreButton = new Button
            {
                Text = "Herstel patient",
                Command = new Command((sender, e) =>
                {
                    var selectedPatient = gridView.SelectedItem as Patienten;
                    if (selectedPatient == null)
                    {
                        return;
                    }

                    var connectString = "Server=localhost;Database=Huisarts;Uid=root;Pwd=;";
                    var connection = new MySqlConnection(connectString);
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "UPDATE person SET isDeleted = 0 WHERE patient_id = @patient_id";
                    command.Parameters.AddWithValue("@patient_id", selectedPatient.patient_id);
                    command.ExecuteNonQuery();
                    connection.Close();

                    //refresh de data in de applicatieform
                    var applicatieForm = Application.Instance.MainForm as ApplicatieForm;
                    applicatieForm?.RefreshData();

                    //refresh de data in de deletedpatientform
                    RefreshData();

                    MessageBox.Show("Patient hersteld");
                })
            };

            // Layout
            Content = new StackLayout
            {
                Spacing = 5,
                Items =
                {
                    gridView,
                    restoreButton
                }
            };

            // Initial data load
            RefreshData();
        }

        //refresh de datagridview
        private void RefreshData()
        {
            //refresh de data in de applicatieform
            var applicatieForm = Application.Instance.MainForm as ApplicatieForm;
            applicatieForm?.RefreshData();

            //refresh de data in het data grid view
            var patientenService = new PatientenService();
            var patienten = patientenService.GetPatienten();
            var deletedPatienten = patienten.FindAll(p => p.isDeleted);
            gridView.DataStore = deletedPatienten;
        }
    }
}