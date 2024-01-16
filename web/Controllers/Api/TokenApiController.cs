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

namespace web.Controllers_Api
{
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
        [HttpGet]
        public string GetToken()
        {
            if(currentToken.IsValid()){
                return JsonConvert.SerializeObject(new CifraCas(currentToken));
            }
            currentToken = new Token();
            return JsonConvert.SerializeObject(new CifraCas(currentToken));
        }

        // GET: api/token/poljubna_cifra //veljavnost
        [HttpGet("{id}")]
        public string GetToken(int id)
        {
            return JsonConvert.SerializeObject(new Veljavnost(currentToken));
        }

        // PUT: api/TokenApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        // POST: api/TokenApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Token>> PostToken(Token token)
        {
            _context.Token.Add(token);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToken", new { id = currentToken.GetCifra() }, token);
        }

        // DELETE: api/TokenApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToken(int id)
        {
            var token = await _context.Token.FindAsync(id);
            if (token == null)
            {
                return NotFound();
            }

            _context.Token.Remove(token);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
