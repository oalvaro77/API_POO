using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_POO.Data;

namespace Proyecto_POO.Controllers;


[ApiController]
[Route("api/[controller]")]

public class UbicacionesController : ControllerBase
{
    private readonly ProjectDbContext _projectDbContext;

    public UbicacionesController(ProjectDbContext projectDbContext)
    {
        _projectDbContext = projectDbContext;
    }

    [HttpGet("Actuales")]
    public async Task<IActionResult> GetUbicacionesActuales()
    {
        var actuales = await _projectDbContext.Persons
            .Include(p => p.Ubicacions)
            .Select(p => new
            {
                PersonaId = p.Id,
                Nombre = p.Pnombre,
                UltimaUbicacion = p.Ubicacions
                .OrderByDescending(u => u.Fecha)
                .Select(u => new
                {
                    u.Direccion,
                    u.Latitud,
                    u.Longitud,
                    u.Fecha,
                }).FirstOrDefault()
            }).ToListAsync();

        return Ok(actuales);
    }

    [HttpGet("historial/{personaID}")]
    public async Task<IActionResult> HistorialPersona(int personaID)
    {
        var historial = await _projectDbContext.Ubicaciones
            .Where(u => u.Idpersona == personaID)
            .OrderByDescending(u => u.Fecha)
            .Select(u => new
            {
                u.Direccion,
                u.Latitud,
                u.Longitud,
                u.Fecha
            }).ToListAsync();

        if (historial == null)
        {
            return NotFound("No se encontró historial para esta persona.");
        }

        return Ok(historial);
    }

    //[HttpPost("Actualizar direccion")]
    //public async Task<IActionResult> ActualizarDireccion(int personaID, [FromBody] string nuevaDireccion)
    //{
    //    var persona = await _projectDbContext.Persons.Include(p => p.Ubicacions).FirstOrDefaultAsync(p => p.Id == personaID);

    //    if (persona == null)
    //    {
    //        return NotFound("Persona no encontrada");
    //    }

    //    var nuevaUbicacion = new Ubicacion
    //    {
    //        Idpersona = personaID,
    //        Direccion = nuevaDireccion,
    //        Fecha = DateTime.UtcNow,
    //    };

    //    _projectDbContext.Ubicaciones.Add(nuevaUbicacion);
    //    await _projectDbContext.SaveChangesAsync();

    //    return Ok();

    //}
}
