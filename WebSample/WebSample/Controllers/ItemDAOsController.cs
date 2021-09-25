using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSample.Models;

namespace WebSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemDAOsController : ControllerBase
    {
        private readonly DataContext _context;

        public ItemDAOsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ItemDAOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDAO>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/ItemDAOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDAO>> GetItemDAO(int id)
        {
            var itemDAO = await _context.Items.FindAsync(id);

            if (itemDAO == null)
            {
                return NotFound();
            }

            return itemDAO;
        }

        // PUT: api/ItemDAOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemDAO(int id, ItemDAO itemDAO)
        {
            if (id != itemDAO.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemDAO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemDAOExists(id))
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

        // POST: api/ItemDAOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemDAO>> PostItemDAO(ItemDAO itemDAO)
        {
            _context.Items.Add(itemDAO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemDAO", new { id = itemDAO.Id }, itemDAO);
        }

        // DELETE: api/ItemDAOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemDAO(int id)
        {
            var itemDAO = await _context.Items.FindAsync(id);
            if (itemDAO == null)
            {
                return NotFound();
            }

            _context.Items.Remove(itemDAO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemDAOExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
