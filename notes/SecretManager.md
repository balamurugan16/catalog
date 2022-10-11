## .Net Secret manager
In .Net the secrets like API keys, db passwords can be managed using the Secret manager.
To Activate the secrets manager, run the following command
```
dotnet user-secrets init
```

This will create an entry in the csproj file with a UserSecretId

Then to add secrets, run the following command
```
dotnet user-secrets set MongoDbSettings:Password Password123
```
The above command will take the `appsettings.json` as reference and add the secrets as a key value pair.
To use an existing property from the file use, `propertyName:key value`