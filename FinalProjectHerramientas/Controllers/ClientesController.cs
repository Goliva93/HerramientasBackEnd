using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProjectHerramientas.Model;
using FinalProjectHerramientas.Data;

namespace FinalProjectHerramientas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : Controller
    {

        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        // Get: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        //get Especific id
        [HttpGet("{id}")]
        public async Task<ActionResult<Clientes>> GetCliente(int id)
        {
            //await for context to find the id
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null) {
                return NotFound();
            }
            return clientes;
        }

        // Post: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostCliente(Clientes client)
        {
            //add client to the context
            _context.Clientes.Add(client);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCliente), new { id = client.Id }, client);
        }

        //Put: api/Clientes
        [HttpPut("{id}")]
        public async Task<ActionResult<Clientes>> PutCliente(int id, Clientes client)
        {
            if (id != client.Id) { return BadRequest(); }
            _context.Entry(client).State = EntityState.Modified;
            try
            {

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return client;

        }

        //delete : api/Clientes
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clientes>> DeleteCliente(int id)
        {
            var client = await _context.Clientes.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(client);
            await _context.SaveChangesAsync();
            return client;
        }

        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }

        private bool ClienteDuplicate(string name)
        {
            return _context.Clientes.Any(e => e.Name == name);
        }

    }

}
