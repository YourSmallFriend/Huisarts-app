using Eto.Forms;

namespace Huisart_Eto
{
    public class ApplicatieForm : Form
    {
        public ApplicatieForm()
        {
            Title = "Applicatie";
            Maximize();

            // Retrieve the list of patients from the service
            var patientenService = new PatientenService();
            var patienten = patientenService.GetPatienten();

            // Create a GridView and bind the list of patients
            var gridView = new GridView
            {
                DataStore = patienten
            };

            // Define columns for the GridView
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "ID",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, int>(p => p.patient_id).Convert(r => r.ToString()) }
                
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "First Name",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(p => p.first_name) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Last Name",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(p => p.last_name) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Email",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(p => p.email) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Phone",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(p => p.phone) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Postcode",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(p => p.postcode) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Address",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(p => p.adress) }
            });
            gridView.Columns.Add(new GridColumn
            {
                HeaderText = "Place",
                DataCell = new TextBoxCell { Binding = Binding.Property<Patienten, string>(p => p.place) }
            });

            Content = new StackLayout
            {
                Items =
                {
                    gridView
                }
            };
        }
    }
}