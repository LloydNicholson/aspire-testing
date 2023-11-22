var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("rediscache");
var sqlDb = builder
    .AddSqlServerContainer("sqlserver", password: "Strong_!_Password1", 1433)
    .AddDatabase("testdb")
    .WithVolumeMount("sqldata", "/var/opt/mssql", VolumeMountType.Named);

var azuriteEmulator = builder.AddAzureStorage("azurite")
    .UseEmulator(blobPort: 1234, queuePort: 1235, tablePort: 1236);
var blobs = azuriteEmulator.AddBlobs("azure-blobs");
var tables = azuriteEmulator.AddTables("azure-tables");
var queues = azuriteEmulator.AddQueues("azure-queues");

var backend = builder.AddProject<Projects.Aspire_WebApi>("backend")
    .WithReference(sqlDb)
    .WithReference(blobs)
    .WithReference(tables)
    .WithReference(queues);

builder.AddProject<Projects.Aspire_BlazorUI>("frontend")
    .WithReference(backend)
    .WithReference(cache);

builder.Build().Run();
