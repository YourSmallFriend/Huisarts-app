using Eto.Forms;

namespace Huisart_Eto
{
    public class MainForm : Form
    {
        private TextBox _gebruikersnaamTextBox;
        private PasswordBox _wachtwoordTextBox;

        public MainForm()
        {
            Title = "My Eto Form";
            Maximize();

            var inloggen = new Command { MenuText = "Inloggen", ToolBarText = "Inloggen" };

            inloggen.Executed += (sender, e) =>
            {
                if (_gebruikersnaamTextBox.Text == "admin" && _wachtwoordTextBox.Text == "admin")
                {
                    // Open de ApplicatieForm als de inloggegevens correct zijn
                    var volgendScherm = new ApplicatieForm();
                    volgendScherm.Show();
                }
                else
                {
                    MessageBox.Show(this, "Ongeldige gebruikersnaam of wachtwoord.", MessageBoxButtons.OK, MessageBoxType.Error);
                }
            };

            _gebruikersnaamTextBox = new TextBox();
            _wachtwoordTextBox = new PasswordBox();

            Content = new StackLayout
            {
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Items =
                {
                    new Label { Text = "Gebruikersnaam" },
                    _gebruikersnaamTextBox,
                    new Label { Text = "Wachtwoord" },
                    _wachtwoordTextBox,
                    new Button { Text = "Inloggen", Command = inloggen },
                }
            };
            
            var quitCommand = new Command { MenuText = "Quit", Shortcut = Application.Instance.CommonModifier | Keys.Q };
            quitCommand.Executed += (sender, e) => Application.Instance.Quit();

            var aboutCommand = new Command { MenuText = "About..." };
            aboutCommand.Executed += (sender, e) => new AboutDialog().ShowDialog(this);

            Menu = new MenuBar
            {
                ApplicationItems =
                {
                    new ButtonMenuItem { Text = "&Preferences..." },
                },
                QuitItem = quitCommand,
                AboutItem = aboutCommand
            };
            
            //dit veroorzaakt een error in de mac versie waardoor de app niet opstart
            
            // ToolBar = new ToolBar { Items = { clickMe } };
            //
            // var formLayout = new StackLayout
            // {
            //     HorizontalContentAlignment = HorizontalAlignment.Center,
            //     Items =
            //     {
            //         new Label { Text = "Gebruikersnaam" },
            //         gebruikersnaamTextBox,
            //         new Label { Text = "Wachtwoord" },
            //         wachtwoordTextBox,
            //         new Button { Text = "Inloggen", Command = inloggen }
            //     }
            // };
            //
            // // Plaats de StackLayout in het midden van een TableLayout
            // Content = new TableLayout
            // {
            //     Rows =
            //     {
            //         new TableRow(null),  // Lege rij boven
            //         new TableRow(new TableCell(formLayout, true)),  // Centraal uitgelijnde StackLayout
            //         new TableRow(null)   // Lege rij onder
            //     }
            // };
        }
    }
}
