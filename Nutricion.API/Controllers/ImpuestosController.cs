using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nutricion.API.Data;
using Nutricion.API.Models;

namespace Nutricion.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
        public class Impuestoscontroller : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public Impuestoscontroller(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpPost("guardar_Impuesto")]
        public async Task<ActionResult<int>> Guardar(Impuesto imp)
        {
            var nuevoImpuesto = imp;
            context.Add(nuevoImpuesto);
            //context.RegistrosIMC.Add(nuevoIMC);
            await context.SaveChangesAsync();
            if (nuevoImpuesto.Id > 0)
            {
                return nuevoImpuesto.Id;
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("solo/{id}")]
        public async Task<ActionResult<Impuesto>> Registros2(int id)
        {
            var solo = new Impuesto();
            solo = await context.Impuestos.Where(i => i.Id == id).FirstOrDefaultAsync();
            if (solo != null)
            {
                return solo;
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet("listaImpuesto")]
        public async Task<ActionResult<List<Impuesto>>> Registros3()
        {
            var lista = new List<Impuesto>();
            lista = await context.Impuestos.ToListAsync();
            if (lista.Count > 0)
            {
                return lista;
            }
            else
            {
                return NotFound();
            }
        }
    }
}
