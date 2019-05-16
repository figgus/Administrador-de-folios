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
    public class FoliosPController : ControllerBase
    {
        private readonly ContextoMySql _context = new ContextoMySql();

       

        // GET: api/FoliosP
        [HttpGet]
        public IEnumerable<Folios> GetFolios()
        {
            return _context.Folios;
        }

        // GET: api/FoliosP/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var folios = await _context.Folios.FindAsync(id);

            if (folios == null)
            {
                return NotFound();
            }

            return Ok(folios);
        }

        // PUT: api/FoliosP/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFolios([FromRoute] int id, [FromBody] Folios folios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != folios.ID)
            {
                return BadRequest();
            }

            _context.Entry(folios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoliosExists(id))
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

        // POST: api/FoliosP
        [HttpPost]
        public async Task<IActionResult> PostFolios([FromBody] Folios folios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Folios.Add(folios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFolios", new { id = folios.ID }, folios);
        }

        // DELETE: api/FoliosP/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var folios = await _context.Folios.FindAsync(id);
            if (folios == null)
            {
                return NotFound();
            }

            _context.Folios.Remove(folios);
            await _context.SaveChangesAsync();

            return Ok(folios);
        }

        private bool FoliosExists(int id)
        {
            return _context.Folios.Any(e => e.ID == id);
        }
    }
}