using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web.Models;
public class Door{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID {get;set;}
    public String? Notes {get;set;}

    public Property Property {get;set;}
}