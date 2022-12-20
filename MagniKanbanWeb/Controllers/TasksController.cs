using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;

namespace MagniKanbanWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksModel>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksModel>> GetTasksModel(int id)
        {
            var tasksModel = await _context.Tasks.FindAsync(id);

            if (tasksModel == null)
            {
                return NotFound();
            }

            return tasksModel;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasksModel(int id, TasksModel tasksModel)
        {
            if (id != tasksModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(tasksModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksModelExists(id))
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

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TasksModel>> PostTasksModel(TasksModel tasksModel)
        {
            _context.Tasks.Add(tasksModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTasksModel", new { id = tasksModel.Id }, tasksModel);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasksModel(int id)
        {
            var tasksModel = await _context.Tasks.FindAsync(id);
            if (tasksModel == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(tasksModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksModelExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
