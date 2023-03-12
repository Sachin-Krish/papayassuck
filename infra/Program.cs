using AzureNative = Pulumi.AzureNative;

return await Pulumi.Deployment.RunAsync(() =>
{
    var rg = new AzureNative.Resources.ResourceGroup("papayassuck", new()
    {
        Location = "australiacentral",
        ResourceGroupName = "papayassuck",
    });

    SetUpCosmosDb();

    
});


 void SetUpCosmosDb(){
    var papaya = new AzureNative.DocumentDB.DatabaseAccount("papaya", new()
    {
        AccountName = "papaya",
        BackupPolicy = new AzureNative.DocumentDB.Inputs.PeriodicModeBackupPolicyArgs
        {
            PeriodicModeProperties = new AzureNative.DocumentDB.Inputs.PeriodicModePropertiesArgs
            {
                BackupIntervalInMinutes = 240,
                BackupRetentionIntervalInHours = 8,
            },
            Type = "Periodic",
        },
        ConsistencyPolicy = new AzureNative.DocumentDB.Inputs.ConsistencyPolicyArgs
        {
            DefaultConsistencyLevel = AzureNative.DocumentDB.DefaultConsistencyLevel.Session,
            MaxIntervalInSeconds = 5,
            MaxStalenessPrefix = 100,
        },
        DatabaseAccountOfferType = AzureNative.DocumentDB.DatabaseAccountOfferType.Standard,
        DefaultIdentity = "FirstPartyIdentity",
        DisableKeyBasedMetadataWriteAccess = false,
        EnableAnalyticalStorage = false,
        EnableAutomaticFailover = false,
        EnableFreeTier = true,
        EnableMultipleWriteLocations = false,
        Identity = new AzureNative.DocumentDB.Inputs.ManagedServiceIdentityArgs
        {
            Type = AzureNative.DocumentDB.ResourceIdentityType.None,
        },
        IsVirtualNetworkFilterEnabled = false,
        Kind = "GlobalDocumentDB",
        Location = "Australia Central",
        Locations = new[]
        {
            new AzureNative.DocumentDB.Inputs.LocationArgs
            {
                FailoverPriority = 0,
                IsZoneRedundant = false,
                LocationName = "Australia Central",
            },
        },
        NetworkAclBypass = AzureNative.DocumentDB.NetworkAclBypass.None,
        PublicNetworkAccess = "Enabled",
        ResourceGroupName = "papayassuck",
        Tags =
        {
            { "defaultExperience", "Core (SQL)" },
            { "hidden-cosmos-mmspecial", "" },
        },
    });

    var production = new AzureNative.DocumentDB.SqlResourceSqlDatabase("production", new()
    {
        AccountName = papaya.Name,
        DatabaseName = "production",
        Resource = new AzureNative.DocumentDB.Inputs.SqlDatabaseResourceArgs
        {
            Id = "production",
        },
        ResourceGroupName = "papayassuck",
    });
}
    
