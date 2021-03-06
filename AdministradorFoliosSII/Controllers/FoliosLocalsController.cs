﻿using System;
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
    public class FoliosLocalsController : ControllerBase
    {
        private readonly ContextoMySql _context=new ContextoMySql();

        

        // GET: api/FoliosLocals
        [HttpGet]
        public IEnumerable<FoliosLocal> GetFoliosLocal()
        {
            return _context.FoliosLocal;
        }

        // GET: api/FoliosLocals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFoliosLocal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foliosLocal = await _context.FoliosLocal.FindAsync(id);

            if (foliosLocal == null)
            {
                return NotFound();
            }

            return Ok(foliosLocal);
        }

        // PUT: api/FoliosLocals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoliosLocal([FromRoute] int id, [FromBody] FoliosLocal foliosLocal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != foliosLocal.ID)
            {
                return BadRequest();
            }

            _context.Entry(foliosLocal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoliosLocalExists(id))
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

        // POST: api/FoliosLocals
        [HttpPost]
        public async Task<IActionResult> PostFoliosLocal([FromBody] FoliosLocal foliosLocal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FoliosLocal.Add(foliosLocal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoliosLocal", new { id = foliosLocal.ID }, foliosLocal);
        }

        // DELETE: api/FoliosLocals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoliosLocal([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foliosLocal = await _context.FoliosLocal.FindAsync(id);
            if (foliosLocal == null)
            {
                return NotFound();
            }

            _context.FoliosLocal.Remove(foliosLocal);
            await _context.SaveChangesAsync();

            return Ok(foliosLocal);
        }

        private bool FoliosLocalExists(int id)
        {
            return _context.FoliosLocal.Any(e => e.ID == id);
        }
    }
}