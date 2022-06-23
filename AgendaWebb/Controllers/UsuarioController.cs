using AgendaWeb.Data;
using AgendaWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaWeb.Controllers
{   
    // api/user
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;

        public UsuarioController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //  GET /api/user
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            ICollection<Usuario> users = await _dbContext.Usuario.ToListAsync();

            return Ok(users);
        }

        //  POST /api/user
        [HttpPost]
        public async Task<IActionResult> InsertUsuario(Usuario user)
        {
            


            await _dbContext.Usuario.AddAsync(user);

            await _dbContext.SaveChangesAsync();
            return Ok(user.UsuarioId);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Usuario user)
        {
            string username = user.UserName.ToLower().Trim();
            Usuario theUser = await _dbContext.Usuario
                .Where(x => x.UserName.ToLower().Equals(username))
                .FirstOrDefaultAsync();

            if (theUser != null && theUser.Password.Equals(user.Password))
            {
                theUser.Password = "";
                return Ok(theUser);
            }


            return Unauthorized();
        }
    }

}
