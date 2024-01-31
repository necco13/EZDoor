using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Identity;



namespace web.Controllers_Api
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly Azuredb _context;

        public RegisterController(Azuredb context)
        {
            _context = context;
        }

        // POST: api/UserApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody]User newUser)
        {
           var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            newUser.PasswordHash = hasher.HashPassword(newUser,newUser.PasswordHash);
            try{
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
            }catch{
                return Forbid();
            }
            return Ok();
        }
    }
    
}
