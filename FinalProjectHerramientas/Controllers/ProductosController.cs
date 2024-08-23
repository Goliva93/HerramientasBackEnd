using FinalProjectHerramientas.Data;
using FinalProjectHerramientas.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectHerramientas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // Get all products: api/Productos 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productos>>> GetProducts() {
            return await _context.Productos.ToListAsync();
        }

        //Get specific product by id : api/Productos/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Productos>> GetProduct(int id) {
            var product = await _context.Productos.FindAsync(id);
            if (product == null) {
                return NotFound();
            }
            return product;
        }

        // Post: insert Products api/Products
        [HttpPost]
        public async Task<ActionResult<Productos>> PostProduct(Productos product)
        {
            _context.Productos.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        //Put: update Product api/Products
        [HttpPut("{id}")]
        public async Task<ActionResult<Productos>> PutProduct(int id, Productos product)
        {
            if (id != product.Id) { return BadRequest(); }
            
            try {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException) {
                if(!ProductExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return product;
        }

        //Delete: delete product of id api/Products
        [HttpDelete("{id}")]
        public async Task<ActionResult<Productos>> DeleteProduct(int id)
        {
            var product = await _context.Productos.FindAsync(id);
            if (product == null) {
                return NotFound();
            }
            _context.Productos.Remove(product);
            await _context.SaveChangesAsync();

            return product;

        }

        private bool ProductExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }

    }
}
