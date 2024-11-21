public class Patienten
{
    public int patient_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string postcode { get; set; }
    public string adress { get; set; }
    public string place { get; set; }
    public string notitie { get; set; }
    public bool isDeleted { get; set; }
    public string deleted_at { get; set; } // Add this property
}