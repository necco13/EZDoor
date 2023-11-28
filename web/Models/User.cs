using Microsoft.AspNetCore.Identity;

namespace web.Models;
public class User : IdentityUser{

    public ICollection<Rent> Rents {get;set;}
}