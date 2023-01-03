using MagniKanbanWeb.Models;
using MagniKanbanWeb.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MagniKanbanWeb.Models.Responses;

namespace MagniKanbanWeb.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext dbContextClass;

        public FileService(ApplicationDbContext dbContextClass)
        {
            this.dbContextClass = dbContextClass;
        }

        public async Task<FileResponse> PostFileAsync(FileRequest request)
        {
            try
            {
                var fileDetails = new FileDetails()
                {
                    FileName = request.File.FileName,
                    ContentType = request.File.ContentType,
                    CardId = request.CardId,
                };

                using (var stream = new MemoryStream())
                {
                    request.File.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                var result = dbContextClass.File.Add(fileDetails);
                await dbContextClass.SaveChangesAsync();
                return new FileResponse { ID = fileDetails.ID, FileName = fileDetails.FileName, ContentType = fileDetails.ContentType };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task PostMultiFileAsync(List<FileRequest> fileData)
        {
            try
            {
                foreach (FileRequest file in fileData)
                {
                    var fileDetails = new FileDetails()
                    {
                        FileName = file.File.FileName,
                    };

                    using (var stream = new MemoryStream())
                    {
                        file.File.CopyTo(stream);
                        fileDetails.FileData = stream.ToArray();
                    }

                    var result = dbContextClass.File.Add(fileDetails);
                }
                await dbContextClass.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DownloadFileById(int Id)
        {
            try
            {
                var file = dbContextClass.File.Where(x => x.ID == Id).FirstOrDefaultAsync();

                var content = new System.IO.MemoryStream(file.Result.FileData);
                var path = Path.Combine(
                   Directory.GetCurrentDirectory(), "Files",
                   file.Result.FileName);

                var folderName = Path.Combine( Directory.GetCurrentDirectory(), "Files");

                // If directory does not exist, create it
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                await CopyStream(content, path);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic? GetFileStram(int id)
        {
            var file = dbContextClass.File.Where(x => x.ID == id).FirstOrDefaultAsync().Result;
            if(file == null)
            {
                return null;
            }
            return new FileStreamResult(new MemoryStream(file.FileData), file.ContentType);
        }

        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
