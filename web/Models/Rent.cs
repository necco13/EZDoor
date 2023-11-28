using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models;

public class Rent{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID {get;set;}
    public DateTime Start {get;set;}
    public DateTime End {get;set;}
    public User User {get;set;}
    public Property Property {get;set;}
}