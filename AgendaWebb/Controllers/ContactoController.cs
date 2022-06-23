using AgendaWeb.Data;
using AgendaWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaWeb.Controllers
{
    // /api/contacto

    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // /api/contacto/usuario/3

        [HttpGet]
        [Route("usuario/{userId}")]
        public async Task<IActionResult> GetContactosUsuario([FromRoute] int userId)
        {
            List<Contacto> contactos = 
                await _dbContext.Contacto.Where(x => x.UsuarioId == userId).ToListAsync();

            return Ok(contactos);
        }

        // /api/contacto/2
        [HttpGet]
        [Route("{contactoId}")]
        public async Task<IActionResult> GetContactoById([FromRoute] int contactoId)
        {
            var contacto =
                await GetContactoByIdFromDb(contactoId);
            if(contacto != null)
            {
                return Ok(contacto);

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> InsertContacto([FromBody] Contacto contacto)
        {
            var usuario = await _dbContext.Usuario
                .Where(x => x.UsuarioId == contacto.UsuarioId)
                .FirstOrDefaultAsync();

            if(usuario != null)
            {
                if(contacto.ImagenId > 0)
                {
                    var img = await _dbContext.Imagen
                        .Where(x => x.ImagenId == contacto.ImagenId)
                        .FirstOrDefaultAsync();
                    img.Temporal = false;

                    contacto.Imagen = img;
                    contacto.ImagenId = img.ImagenId;

                    _dbContext.Imagen.Update(img);
                }


                contacto.Usuario = usuario;
                await _dbContext.Contacto.AddAsync(contacto);
                await _dbContext.SaveChangesAsync();
                return Ok(contacto);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContacto([FromBody] Contacto contacto)
        {
            var obj =
                await GetContactoByIdFromDb(contacto.ContactoId);

            Imagen oldImage = null;
            if(obj.ImagenId > 0)
            {
                oldImage = await _dbContext.Imagen
                    .Where(x => x.ImagenId == obj.ImagenId)
                    .FirstOrDefaultAsync();
            }
            Imagen newImage = null;
            if (contacto.ImagenId > 0)
            {
                newImage = await _dbContext.Imagen
                    .Where(x => x.ImagenId == contacto.ImagenId)
                    .FirstOrDefaultAsync();
            }

            if(oldImage != null && oldImage.ImagenId != contacto.ImagenId)
            {
                oldImage.Temporal = true;
                _dbContext.Imagen.Update(oldImage);
            }

            if(newImage != null)
            {
                newImage.Temporal = false;
                _dbContext.Imagen.Update(newImage);
                obj.Imagen = newImage;
                obj.ImagenId = newImage.ImagenId;
            }
            else
            {
                obj.Imagen = null;
                obj.ImagenId = 0;
            }

            obj.Email = contacto.Email;
            obj.NombreContacto = contacto.NombreContacto;
            obj.Telefono = contacto.Telefono;            

            _dbContext.Contacto.Update(obj);
            await _dbContext.SaveChangesAsync();
            return Ok(contacto);
        }

        // api/contacto/2

        [HttpDelete]
        [Route("{contactoId}")]
        public async Task<IActionResult> DeleteContacto([FromRoute] int contactoId)
        {
            var contacto = await GetContactoByIdFromDb(contactoId);
            if(contacto != null)
            {
                if(contacto.ImagenId > 0)
                {
                    var img = await _dbContext.Imagen
                        .Where(x => x.ImagenId == contacto.ImagenId)
                        .FirstOrDefaultAsync();
                    img.Temporal = true;

                    _dbContext.Imagen.Update(img);
                }

                _dbContext.Contacto.Remove(contacto);
                await _dbContext.SaveChangesAsync();
                return Ok(true);
            }

            return BadRequest();
        }

        private async Task<Contacto> GetContactoByIdFromDb(int contactoId)
        {
            return await _dbContext.Contacto
                .Where(x => x.ContactoId == contactoId)
                .FirstOrDefaultAsync();
        }

    }
}
