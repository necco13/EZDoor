namespace web.Models;
public class Landlord{
    public int ID {get;set;}
    public String Name {get;set;}
    public String Surname {get;set;}
    public String EMail {get;set;}
    public String Password {get;set;}

    public ICollection<Property> Properties {get;set;}
}