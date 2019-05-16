using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdministradorFoliosSII.DAL;
using AdministradorFoliosSII.Models;

namespace AdministradorFoliosSII.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsersionFoliosController : ControllerBase
    {
        private readonly ContextoMySql _context = new ContextoMySql();


        // GET: api/InsersionFolios
        [HttpGet]
        public IEnumerable<InsersionFolios> GetInsersionFolios()
        {
            return _context.InsersionFolios;
        }

        // GET: api/InsersionFolios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInsersionFolios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insersionFolios = await _context.InsersionFolios.FindAsync(id);

            if (insersionFolios == null)
            {
                return NotFound();
            }

            return Ok(insersionFolios);
        }

        // PUT: api/InsersionFolios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsersionFolios([FromRoute] int id, [FromBody] InsersionFolios insersionFolios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != insersionFolios.ID)
            {
                return BadRequest();
            }

            _context.Entry(insersionFolios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsersionFoliosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InsersionFolios
        [HttpPost]
        public async Task<IActionResult> PostInsersionFolios([FromBody] InsersionFolios insersionFolios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            insersionFolios.fecha = DateTime.Now;
            int cantidad= insersionFolios.numFolioHasta - insersionFolios.numFolioDesde;
            insersionFolios.foliosDisponibles = cantidad;

            for (int i = 0; i < cantidad; i++)
            {
                _context.Folios.Add(new Folios {NumFolio=(i+1),EstaAsignado=0 });
            }
            _context.InsersionFolios.Add(insersionFolios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsersionFolios", new { id = insersionFolios.ID }, insersionFolios);
        }

        // DELETE: api/InsersionFolios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsersionFolios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var insersionFolios = await _context.InsersionFolios.FindAsync(id);
            if (insersionFolios == null)
            {
                return NotFound();
            }

            _context.InsersionFolios.Remove(insersionFolios);
            await _context.SaveChangesAsync();

            return Ok(insersionFolios);
        }

        private bool InsersionFoliosExists(int id)
        {
            return _context.InsersionFolios.Any(e => e.ID == id);
        }
    }
}