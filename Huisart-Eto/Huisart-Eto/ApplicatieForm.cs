using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;

namespace Huisart_Eto
{
    public class ApplicatieForm : Form
    {
        private const int ItemsPerPage = 25;
        private int currentPage = 0;
        private List<Patienten> allPatienten;
        private GridView gridView;
        private Button openPatientFormButton;

        public ApplicatieForm()
        {
            Title = "Applicatie";
            Maximize();
            BackgroundColor = Color.FromArgb(50, 50, 50);

            // Haal patiënten op
            var patientenService = new PatientenService();
            allPatienten = patientenService.GetPatienten().ToList();

            // Maak een GridView
            gridView = new GridView() { AllowMultipleSelection = false };
            gridView.SelectionChanged += GridView_SelectionChanged;
            UpdateGridView();

            // Zoekbalk
            var search = new TextBox();
            search.TextChanged += (sender, e) =>
            {
                var searchText = search.Text.ToLower();
                var searchResult = allPatienten
                    .Where(p => p.first_name.ToLower().Contains(searchText) ||
                                p.last_name.ToLower().Contains(searchText) ||
                                p.email.Contains(searchText) ||
                                p.phone.Contains(searchText) ||
                                p.postcode.Contains(searchText) ||
                                p.adress.Contains(searchText) ||
                                p.place.Contains(searchText))
                    .ToList();
                allPatienten = searchResult;
                currentPage = 0;
                UpdateGridView();
            };

            //als de zoekbalk leeg is dan worden alle patienten getoond
            search.TextChanged += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(search.Text))
                {
                    allPatienten = patientenService.GetPatienten().ToList();
                    currentPage = 0;
                    UpdateGridView();
                }
            };

            // Navigatieknoppen
            var previousButton = new Button { Text = "Vorige" };
            previousButton.Click += (sender, e) =>
            {
                if (currentPage > 0)
                {
                    currentPage--;
                    UpdateGridView();
                }
            };

            var nextButton = new Button { Text = "Volgende" };
            nextButton.Click += (sender, e) =>
            {
                if ((currentPage + 1) * ItemsPerPage < allPatienten.Count)
                {
                    currentPage++;
                    UpdateGridView();
                }
            };

            // Button to open PatientForm
            openPatientFormButton = new Button { Text = "Open Patient Form", Visible = false };
            openPatientFormButton.Click += (sender, e) =>
            {
                var patient = (Patienten)gridView.SelectedItem;
                var patientForm = new PatientForm(patient);
                patientForm.Show();
            };

            // Layout
            Content = new StackLayout
            {
                Spacing = 5,
                Items =
                {
                    search,
                    gridView,
                    openPatientFormButton,
                    new StackLayout
                    {
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        Orientation = Orientation.Horizontal,
                        Spacing = 10,
                        Items =
                        {
                            previousButton,
                            nextButton
                        }
                    }
                }
            };
        }

        private void GridView_SelectionChanged(object sender, EventArgs e)
        {
            openPatientFormButton.Visible = gridView.SelectedItem != null;
        }

        private void UpdateGridView()
        {
            var pageData = allPatienten
                .Skip(currentPage * ItemsPerPage)
                .Take(ItemsPerPage)
                .ToList();

            gridView.DataStore = pageData;

            // Columns opnieuw definiëren
            gridView.Columns.Clear();
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
        
        public void RefreshData()
        {
            var patientenService = new PatientenService();
            var allPatienten = patientenService.GetPatienten().ToList();
            currentPage = 0;
            UpdateGridView();
        }
    }
}