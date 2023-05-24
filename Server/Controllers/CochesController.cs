using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad18.Server.Contexto;
using Actividad18.Shared.Models;

namespace Actividad18.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CochesController : ControllerBase
    {
        private readonly ContextoAutos _context;

        public CochesController(ContextoAutos context)
        {
            _context = context;
        }

        // GET: api/Coches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coches>>> GetCoches()
        {
            if (_context.Coches == null)
            {
                return NotFound();
            }

            return await _context.Coches.Include(c => c.Alquiler).ToListAsync();
        }

        // GET: api/Coches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Coches>> GetCoches(int id)
        {
            if (_context.Coches == null)
            {
                return NotFound();
            }

            var coches = await _context.Coches.Include(c => c.Alquiler).FirstOrDefaultAsync(c => c.Id == id);

            if (coches == null)
            {
                return NotFound();
            }

            return coches;
        }

        // PUT: api/Coches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoches(int id, Coches coches)
        {
            if (id != coches.Id)
            {
                return BadRequest();
            }

            _context.Entry(coches).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CochesExists(id))
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

        // POST: api/Coches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coches>> PostCoches(Coches coches, int? alquilerId)
        {
            if (_context.Coches == null)
            {
                return Problem("Entity set 'ContextoAutos.Coches' is null.");
            }

            if (alquilerId.HasValue)
            {
                // Obtener el alquiler correspondiente al ID proporcionado en AlquilerId
                var alquiler = await _context.Alquilers.FindAsync(alquilerId);
                if (alquiler == null)
                {
                    return NotFound("El alquiler no existe.");
                }

                coches.Alquiler = alquiler;
            }

            _context.Coches.Add(coches);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoches", new { id = coches.Id }, coches);
        }

        // DELETE: api/Coches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoches(int id)
        {
            if (_context.Coches == null)
            {
                return NotFound();
            }

            var coches = await _context.Coches.FindAsync(id);
            if (coches == null)
            {
                return NotFound();
            }

            _context.Coches.Remove(coches);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CochesExists(int id)
        {
            return (_context.Coches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
