using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core31.Library.Exceptions;
using Core31.Library.Models.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebNet6.Controllers.Api
{
    public class EFCoreController : ApiControllerBase
    {
        private readonly TodoItemContext _context;

        public EFCoreController(TodoItemContext context)
        {
            _context = context;
        }

        [HttpGet("TodoItems")]
        public Task<List<TodoItem>> GetTodoItems()
        {
            return _context.TodoItem.ToListAsync();
        }

        [HttpGet("TodoItems/{id}")]
        public async Task<TodoItem> GetTodoItem([FromRoute] int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            return todoItem ?? throw new NotFoundException();
        }

        [HttpPost("TodoItems")]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItem.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        [HttpPut("TodoItems/{id}")]
        public async Task PutTodoItem([FromRoute] int id, TodoItem item)
        {
            if (id != item.Id) throw new BadRequestException("bad id.");

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        [HttpDelete("TodoItems/{id}")]
        public async Task DeleteTodoItem([FromRoute] int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null) throw new NotFoundException();

            _context.TodoItem.Remove(todoItem);
            await _context.SaveChangesAsync();
        }
    }


}
