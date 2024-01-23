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


public class LoginUser{
    public string ime;
    public string geslo;
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
        public async Task<LoginUser> PostUser([FromBody]LoginUser user)
        {
            return user;
        }
    }
}
