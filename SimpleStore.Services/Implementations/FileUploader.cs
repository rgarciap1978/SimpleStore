using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SimpleStore.Entities;
using SimpleStore.Services.Interfaces;

namespace SimpleStore.Services.Implementations
{
    public class FileUploader : IFileUploader
    {
        private readonly AppConfig _appConfig;
        private readonly ILogger<FileUploader> _logger;

        public FileUploader(IOptions<AppConfig> options, ILogger<FileUploader> logger)
        {
            _appConfig = options.Value;
            _logger = logger;
        }
        

        public async Task<string> UploadFileAsync(string? base64String, string? fileName)
        {
            if (string.IsNullOrWhiteSpace(base64String) || string.IsNullOrWhiteSpace(fileName))
            {
                return string.Empty;
            }

            try
            {
                var bytes = Convert.FromBase64String(base64String);
                var path = Path.Combine(_appConfig.StorageConfiguration.Path, fileName);
                await using var fileStream = new FileStream(path, FileMode.Create);
                await fileStream.WriteAsync(bytes, 0, bytes.Length);

                return $"{_appConfig.StorageConfiguration.PublicUrl}{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al subir el archivo {fileName} {ex.Message}");
                return string.Empty;
            }


        }
        
    }
}
