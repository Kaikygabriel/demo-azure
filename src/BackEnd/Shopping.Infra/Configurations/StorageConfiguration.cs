using System.ComponentModel;

namespace Shopping.Infra.Configurations;

public class StorageConfiguration
{
    private StorageConfiguration()
    {
        
    }
    public StorageConfiguration(string connection,string container)
    {
        ConnectionString = connection;
        ContainerName = container;
    }
    public string ConnectionString { get; private init; } = null!;
    public string ContainerName { get; private init; } = null!;

}