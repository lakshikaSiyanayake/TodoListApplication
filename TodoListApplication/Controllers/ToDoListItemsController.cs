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
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListItemsController : ControllerBase
    {
        private readonly ToDoListDbContext _context;    

        public ToDoListItemsController(ToDoListDbContext context)
        {
            _context = context;
        }

      
        [HttpGet("loggedUser")]
        public async Task<List<ToDoListItem>> GetToDoItems(string loggedUser)
        {
            {
                //string loggedUser = IHttpContextAccessor.HttpContext.User.Identity.Name;
                var toDoListItems = await _context.ToDoItems.ToListAsync();
                toDoListItems = toDoListItems.Where(x => x.loggedUser == loggedUser).ToList();
                if (toDoListItems.Count != 0)
                {
                    return toDoListItems;
                }
                else
                {
                    return toDoListItems;
                }
            }

        }

        // GET: api/ToDoListItems/2
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

        // PUT: api/ToDoListItems/2
        [HttpPut("{id}")]
        public async Task<ActionResult<ToDoListItem>> PutToDoListItem(int id, ToDoListItem toDoListItem)
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

            return toDoListItem;
        }

        // POST: api/ToDoListItems
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
