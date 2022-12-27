using MagniKanbanWeb.Models.Requests;
using MagniKanbanWeb.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace MagniKanbanWeb.Services
{
    public interface IFileService
    {
        public Task<FileResponse> PostFileAsync(IFormFile fileData);

        public Task PostMultiFileAsync(List<FileRequest> fileData);

        public Task DownloadFileById(int id);

        public dynamic? GetFileStram(int id);
    }
}
