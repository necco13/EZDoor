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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



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
        public async Task<IActionResult> PostUser([FromBody]LoginUser newUser)
        {
           var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
           var userStore = new UserStore<User>(_context);
            var userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);
            var user = new User { UserName = newUser.ime, Email = newUser.ime };
            var result = await userManager.CreateAsync(user, newUser.geslo);

            if (result.Succeeded)
                return Ok();
            return Forbid();
        }
    }
    
}
