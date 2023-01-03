using MagniKanbanWeb.Models;
using MagniKanbanWeb.Models.Requests;
using MagniKanbanWeb.Models.Responses;
using MagniKanbanWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace MagniKanbanWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _uploadService;
        private readonly ApplicationDbContext _context;

        public FileController(IFileService uploadService, ApplicationDbContext context)
        {
            _uploadService = uploadService;
            _context = context;
        }

        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost("Upload")]
        public async Task<FileResponse> PostSingleFile([FromForm] FileRequest file)
        {
            try
            {
                return await _uploadService.PostFileAsync(file);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Multiple File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// 
        [Authorize]
        [HttpPost("Upload/Multiple")]
        public async Task<ActionResult> PostMultipleFile([FromForm] List<FileRequest> fileDetails)
        {
            if (fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                await _uploadService.PostMultiFileAsync(fileDetails);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Download File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            try
            {
                var stream = _uploadService.GetFileStram(id);
                if(stream == null)
                {
                    return NotFound();
                }
                return stream;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/File/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var project = await _context.File.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.File.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
