using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApplication.Models;

namespace TodoListApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListItemsController : ControllerBase
    {
        private readonly ToDoListDbContext _context;

        public ToDoListItemsController(ToDoListDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoListItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoListItem>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        // GET: api/ToDoListItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoListItem>> GetToDoListItem(int id)
        {
            var toDoListItem = await _context.ToDoItems.FindAsync(id);

            if (toDoListItem == null)
            {
                return NotFound();
            }

            return toDoListItem;
        }

        // PUT: api/ToDoListItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoListItem(int id, ToDoListItem toDoListItem)
        {
            if (id != toDoListItem.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(toDoListItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoListItemExists(id))
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

        // POST: api/ToDoListItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoListItem>> PostToDoListItem(ToDoListItem toDoListItem)
        {
            _context.ToDoItems.Add(toDoListItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoListItem", new { id = toDoListItem.ItemId }, toDoListItem);
        }

        // DELETE: api/ToDoListItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoListItem(int id)
        {
            var toDoListItem = await _context.ToDoItems.FindAsync(id);
            if (toDoListItem == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoListItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoListItemExists(int id)
        {
            return _context.ToDoItems.Any(e => e.ItemId == id);
        }
    }
}
