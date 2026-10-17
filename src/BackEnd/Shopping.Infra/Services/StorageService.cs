using System.Text.RegularExpressions;
using Azure.Storage.Blobs;
using Shopping.Application.Interfaces.Services;
using Shopping.Domain.BackOffice.Commum;
using Shopping.Infra.Configurations;

namespace Shopping.Infra.Services;

internal sealed class StorageService : IStorageService
{
    private readonly StorageConfiguration _storageConfiguration;

    public StorageService(StorageConfiguration storageConfiguration)
    {
        _storageConfiguration = storageConfiguration;
    }

    public async Task<ResultValue<string>> Insert(string base64, string extendsFile)
    {
        var  regex=new Regex(@"^[\w/\:.-]+;base64,");
        var base64File=regex.Replace(base64,string.Empty);

        var bytes = Convert.FromBase64String(base64File);
        var blobClient = new BlobClient(_storageConfiguration.ConnectionString,_storageConfiguration.ContainerName,Guid.NewGuid() + extendsFile);
        using var memoryStream = new MemoryStream(bytes);
        await blobClient.UploadAsync(memoryStream);

        return blobClient.Uri.AbsoluteUri;
    }
}