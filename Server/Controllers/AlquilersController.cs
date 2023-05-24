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
    public class AlquilersController : ControllerBase
    {
        private readonly ContextoAutos _context;

        public AlquilersController(ContextoAutos context)
        {
            _context = context;
        }

        // GET: api/Alquilers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alquiler>>> GetAlquilers()
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }
            return await _context.Alquilers.ToListAsync();
        }

        // GET: api/Alquilers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alquiler>> GetAlquiler(int id)
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquilers
                .Include(a => a.Cliente) // Cargar el cliente asociado
                .Include(a => a.Coches) // Cargar los coches asociados
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alquiler == null)
            {
                return NotFound();
            }

            return alquiler;
        }

        // PUT: api/Alquilers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlquiler(int id, Alquiler alquiler)
        {
            if (id != alquiler.Id)
            {
                return BadRequest();
            }

            _context.Entry(alquiler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlquilerExists(id))
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

        // POST: api/Alquilers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alquiler>> PostAlquiler(Alquiler alquiler)
        {
            if (_context.Alquilers == null)
            {
                return Problem("Entity set 'ContextoAutos.Alquilers' is null.");
            }

            // Verificar si el Cliente con el ID proporcionado existe en la base de datos
            var cliente = await _context.Cliente.FindAsync(alquiler.ClientesId);
            if (cliente == null)
            {
                return NotFound("El cliente no existe.");
            }

            // Verificar si el alquiler ya tiene un coche vinculado
            if (alquiler.Coches != null)
            {
                return BadRequest("El alquiler ya tiene un coche vinculado.");
            }

            // Asignar el cliente correspondiente al alquiler utilizando su ID
            alquiler.Cliente = cliente;

            _context.Alquilers.Add(alquiler);
            cliente.Alquilers.Add(alquiler);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlquiler", new { id = alquiler.Id }, alquiler);
        }

        // DELETE: api/Alquilers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlquiler(int id)
        {
            if (_context.Alquilers == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquilers
                .Include(a => a.Coches)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alquiler == null)
            {
                return NotFound();
            }

            if (alquiler.Coches != null)
            {
                _context.Coches.RemoveRange(alquiler.Coches);
            }

            _context.Alquilers.Remove(alquiler);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlquilerExists(int id)
        {
            return (_context.Alquilers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
