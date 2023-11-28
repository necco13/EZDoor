using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models;
public class Property{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID {get;set;}
    public String Name {get;set;}
    public String Address {get;set;}
    public String? Notes {get;set;}

    ICollection<Rent>? Rents {get;set;}
    public User Landlord {get;set;}
}