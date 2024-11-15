using Eto.Drawing;
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
            BackgroundColor = Color.FromArgb(50 , 50 , 50);

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
                Items =
                {
                    new Label { Text = "Gebruikersnaam", TextColor = Colors.White },
                    _gebruikersnaamTextBox,
                    new Label { Text = "Wachtwoord", TextColor = Colors.White },
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
        }
    }
}