using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models;

    public enum Stars{
        A,B,C,D,E,F
    }

public class Review{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID {get;set;}
    public Stars Stars{get;set;}
    public String Comment {get;set;}
    public Rent Rent {get;set;}
}