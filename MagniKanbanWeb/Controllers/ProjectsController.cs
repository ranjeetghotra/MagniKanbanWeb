using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using MagniKanbanWeb.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace MagniKanbanWeb.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public IQueryable<Object> GetProjects()
        {
            var project = _context.Projects
            .Include(a => a.Boards)
            .Select(a =>
                new
                {
                    Id = a.Id,
                    Title = a.Title.ToString(),
                    Boards = a.Boards.Where((b) => b.ProjectId == a.Id)
                    .Select(d =>
                        new
                        {
                            Id = d.Id,
                            Title = d.Title.ToString(),
                            Cards = d.Cards.Where((e) => e.BoardId == d.Id).ToList()
                        }
                    ).ToList()
                }
                );

            if (project == null)
            {
                return (IQueryable<object>)NotFound();
            }

            return project;
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetProject(int id)
        {
            var boardModel = _context.Projects
            .Include(a => a.Boards)
            .Select(a =>
                new
                {
                    Id = a.Id,
                    Title = a.Title.ToString(),
                    Cards = a.Boards.Where((b) => b.ProjectId == a.Id).ToList()
                }
                ).Where(a => a.Id == id).ToList();

            if (boardModel == null || boardModel.Count == 0)
            {
                return NotFound();
            }

            return boardModel[0];
        }

        // GET: api/Projects/5
        [HttpGet("{id}/Tags")]
        public List<string> GetProjectTags(int id)
        {
            var boards = _context.Boards
            .Where(a => a.ProjectId == id)
            .Include(a => a.Cards)
            .Select(a => a.Cards.Select(b => b.Tags)).ToArray();

            List<string> tags = new List<string> { };

            foreach(var board in boards)
            {
                foreach (var tag in board)
                {
                    if (tag != "")
                    {
                        tags.AddRange(tag.Split(";"));
                    }
                }
            }

            return tags.Distinct().ToList();
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(ProjectRequest projectReq)
        {
            var project = new Project { Title = projectReq.Title };
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            string[] boardNames = new string[] { "Backlog", "In-Progress", "In-Review", "Done" };
            int order = 0;
            foreach (string boardName in boardNames)
            {
                _context.Boards.Add(new Board { Title = boardName, ProjectId = project.Id, Order = order });
                order++;
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }
    }
}
