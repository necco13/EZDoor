namespace web.Models;
public class Property{
    public int ID {get;set;}
    public String Name {get;set;}
    public String Address {get;set;}
    public String Notes {get;set;}

    ICollection<Rent> Rents {get;set;}
    public User Landlord {get;set;}
}