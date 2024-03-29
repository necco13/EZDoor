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


public class LoginUser{
    public string ime{get;set;}
    public string geslo{get;set;}
}

namespace web.Controllers_Api
{
    [Route("api/overi")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly Azuredb _context;

        public UserApiController(Azuredb context)
        {
            _context = context;
        }
        public bool isValid(LoginUser usr){
            Console.Write(usr.ime);
            if(string.Equals(usr.ime, "Cefizelj")){
                return true;
            }
            return false;
        }

        // POST: api/UserApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody]LoginUser user)
        {
           var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            User us = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.ime);
            if(us==null)
                return Ok(false);
            if(hasher.VerifyHashedPassword(us,us.PasswordHash,user.geslo) == PasswordVerificationResult.Failed)
                return Ok(false);
            else if(hasher.VerifyHashedPassword(us,us.PasswordHash,user.geslo) == PasswordVerificationResult.Success)
                return Ok(true);
            return Ok(false);
        }
    } 
}
