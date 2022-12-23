using MagniKanbanWeb.Models.Requests;

namespace MagniKanbanWeb.Services
{
    public interface IFileService
    {
        public Task PostFileAsync(IFormFile fileData);

        public Task PostMultiFileAsync(List<FileRequest> fileData);

        public Task DownloadFileById(int fileName);
    }
}
