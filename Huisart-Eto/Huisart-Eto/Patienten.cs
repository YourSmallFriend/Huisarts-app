namespace Huisart_Eto;
public class Patienten
{
    
    //hier sla ik de gegevens van de patienten op in een lijst patient_id first_name last_name email phone postcode adress place
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
}
