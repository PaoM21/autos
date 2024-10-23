using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Autos.Models;
using Autos.Data;

[Route("api/[controller]")]
[ApiController]
public class MarcasAutosController : ControllerBase
{
    private readonly DataBase _context;

    public MarcasAutosController(DataBase context)
    {
        _context = context;
    }

    // GET: api/marcasautos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarcasAutos>>> GetMarcasAutos()
    {
        return await _context.MarcasAutos.ToListAsync();
    }

    // GET: api/marcasautos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MarcasAutos>> GetMarcasAutos(int id)
    {
        var marcaAuto = await _context.MarcasAutos.FindAsync(id);

        if (marcaAuto == null)
        {
            return NotFound();
        }

        return marcaAuto;
    }

    // POST: api/marcasautos
    [HttpPost]
    public async Task<ActionResult<MarcasAutos>> PostMarcasAutos(MarcasAutos marcaAuto)
    {
        _context.MarcasAutos.Add(marcaAuto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMarcasAutos), new { id = marcaAuto.Id }, marcaAuto);
    }

    // PUT: api/marcasautos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMarcasAutos(int id, MarcasAutos marcaAuto)
    {
        if (id != marcaAuto.Id)
        {
            return BadRequest();
        }

        _context.Entry(marcaAuto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MarcasAutosExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/marcasautos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMarcasAutos(int id)
    {
        var marcaAuto = await _context.MarcasAutos.FindAsync(id);
        if (marcaAuto == null)
        {
            return NotFound();
        }

        _context.MarcasAutos.Remove(marcaAuto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MarcasAutosExists(int id) => _context.MarcasAutos.Any(e => e.Id == id);
}
