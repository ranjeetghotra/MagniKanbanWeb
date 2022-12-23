using MagniKanbanWeb.Models;
using MagniKanbanWeb.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace MagniKanbanWeb.Services
{
    public class FileService : IFileService
    {
        private readonly ApplicationDbContext dbContextClass;

        public FileService(ApplicationDbContext dbContextClass)
        {
            this.dbContextClass = dbContextClass;
        }

        public async Task PostFileAsync(IFormFile fileData)
        {
            try
            {
                var fileDetails = new FileDetails()
                {
                    ID = 0,
                    FileName = fileData.FileName,
                };

                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    fileDetails.FileData = stream.ToArray();
                }

                var result = dbContextClass.File.Add(fileDetails);
                await dbContextClass.SaveChangesAsync();
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

        public async Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}
