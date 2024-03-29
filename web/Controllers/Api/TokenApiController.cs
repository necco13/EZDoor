using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.Models;
using Newtonsoft.Json;

using Microsoft.AspNetCore.Identity;

namespace web.Controllers_Api
{
    public class LoginUser{
    public string ime{get;set;}
    public string geslo{get;set;}
}
    [Route("api/token")]
    [ApiController]
    public class TokenApiController : ControllerBase
    {
        private readonly Azuredb _context;

        public TokenApiController(Azuredb context)
        {
            _context = context;
        }

        static Token currentToken = new Token();

        // GET: api/TokenApi
        [HttpPost]
        public async Task<IActionResult> PostToken([FromBody]LoginUser user)
        {
            var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
            User us = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.ime);
            if(us==null)
                return Ok("{\"cifra\":0,\"cas\":-1}");
            if(hasher.VerifyHashedPassword(us,us.PasswordHash,user.geslo) == PasswordVerificationResult.Failed)
                return Ok("{\"cifra\":0,\"cas\":-1}");
            else if(hasher.VerifyHashedPassword(us,us.PasswordHash,user.geslo) == PasswordVerificationResult.Success)
                {
            if(currentToken.IsValid()){
                return Ok(JsonConvert.SerializeObject(new CifraCas(currentToken)));
            }
            currentToken = new Token();
            return Ok(JsonConvert.SerializeObject(new CifraCas(currentToken)));
                }
            return Ok("{\"cifra\":0,\"cas\":-1}");
        }

        // GET: api/token/poljubna_cifra //veljavnost
        [HttpGet("{id}")]
        public string GetToken(int id)
        {
            if(id == currentToken.GetCifra())
                return JsonConvert.SerializeObject(new Veljavnost(currentToken));
            else
                return JsonConvert.SerializeObject(new Veljavnost());
        }

    }
}