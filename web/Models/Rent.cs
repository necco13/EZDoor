namespace web.Models;

public class Rent{
    public int ID {get;set;}
    public DateTime Start {get;set;}
    public DateTime End {get;set;}
    public User User {get;set;}
    public Property Property {get;set;}
    public Review Review {get;set;}
}