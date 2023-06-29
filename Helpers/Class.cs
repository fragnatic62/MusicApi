using Azure.Storage.Blobs;
using MusicApi.Models;

namespace MusicApi.Helpers
{
    public static class FileHelper
    {
        public static async Task<string> UploadToBlobStorage(string containerName, IFormFile? file)
        {
            if (file != null)
            {
                string blobConnectionString = @"DefaultEndpointsProtocol=https;AccountName=demomusicstorageaccount;AccountKey=IwZ9aGjya+ilQZRKlaw+cuD59PZCZz8blXz0nYpS/GTml3t7GiH4OJXlwI3/ZAh8SG8DV17Yme37+ASt0EyHUg==;EndpointSuffix=core.windows.net";

                BlobContainerClient blobContainerClient = new BlobContainerClient(blobConnectionString, containerName);
                await blobContainerClient.CreateIfNotExistsAsync();

                BlobClient blobClient;
                string uniqueFileName = GetUniqueFileName(file.FileName);
                bool blobExists = await blobContainerClient.GetBlobClient(uniqueFileName).ExistsAsync();

                if (blobExists)
                {
                    uniqueFileName = GetUniqueFileName(uniqueFileName);
                }

                blobClient = blobContainerClient.GetBlobClient(uniqueFileName);

                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await blobClient.UploadAsync(memoryStream);

                return blobClient.Uri.AbsoluteUri;
            }

            return "";
        }

        private static string GetUniqueFileName(string fileName)
        {
            string uniqueName = $"{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{fileName}";
            return uniqueName;
        }


    }
}
