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
           try{
            if(_context==null)
                return Ok("context");
           var userStore = new UserStore<User>(_context);
           if(userStore==null)
                return Ok("Store");
            var userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);
            if(userManager==null)
                return Ok("Manager");
            var user = new User { UserName = newUser.ime, Email = newUser.ime };
            if(user==null)
                return Ok("user");
            var result = await userManager.CreateAsync(user, newUser.geslo);
            if(result==null)
                return Ok("result");
            return Ok(result.ToString());

           }catch(Exception ex){
            return Ok(ex.Message);
           }
        }
    }
    
}
