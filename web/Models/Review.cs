namespace web.Models;

    public enum Stars{
        A,B,C,D,E,F
    }

public class Review{
    public int ID {get;set;}
    public Stars Stars{get;set;}
    public String Comment {get;set;}
    public Rent Rent {get;set;}
}