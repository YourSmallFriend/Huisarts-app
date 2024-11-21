using Eto.Forms;

namespace Huisart_Eto;

public class AddPatientenForm : Form
{
    
    
    //hier maak ik de form aan voor het toevoegen van een patient aan de database
    public AddPatientenForm()
    {
        Title = "Voeg Patient Toe";
        
        Maximize();
        var layout = new StackLayout { Padding = 10, Spacing = 5 };
        var firstName = new TextBox { PlaceholderText = "Voornaam" };
        var lastName = new TextBox { PlaceholderText = "Achternaam" };
        var email = new TextBox { PlaceholderText = "Email" };
        var phone = new TextBox { PlaceholderText = "Telefoon" };
        var postcode = new TextBox { PlaceholderText = "Postcode" };
        var address = new TextBox { PlaceholderText = "Adres" };
        var place = new TextBox { PlaceholderText = "Plaats" };

        var addButton = new Button { Text = "Voeg Toe" };
        addButton.Click += (sender, e) =>
        {
            var patient = new Patienten
            {
                first_name = firstName.Text,
                last_name = lastName.Text,
                email = email.Text,
                phone = phone.Text,
                postcode = postcode.Text,
                adress = address.Text,
                place = place.Text
            };

            var patientenService = new PatientenService();
            patientenService.AddPatient(patient);
        };

        layout.Items.Add(new Label { Text = "Voornaam" });
        layout.Items.Add(firstName);
        layout.Items.Add(new Label { Text = "Achternaam" });
        layout.Items.Add(lastName);
        layout.Items.Add(new Label { Text = "Email" });
        layout.Items.Add(email);
        layout.Items.Add(new Label { Text = "Telefoon" });
        layout.Items.Add(phone);
        layout.Items.Add(new Label { Text = "Postcode" });
        layout.Items.Add(postcode);
        layout.Items.Add(new Label { Text = "Adres" });
        layout.Items.Add(address);
        layout.Items.Add(new Label { Text = "Plaats" });
        layout.Items.Add(place);
        layout.Items.Add(addButton);

        Content = layout;
    }
}