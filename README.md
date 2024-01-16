# BaeAPI

The backend API for the Bae AI Scanner solution.

## How to develop

To start developing, ensure you are located on the `develop` branch (or a feature branch).

When this is done, simply open `BaeAPI.sln` in Visual Studio to get started. After this, make sure you have an appropriate `appsettings.json` file for BaeServer.

## Getting started

In order to run the project you must build the `BaeServer` project like so:

```cs
dotnet build --project BaeServer
```

Running the project requires an `appsettings.json` to be provided in the same directory as the resulting binary.
A minimal `appsettings.json` looks like so:

```json
{
  "AIScannerDatabase": {
    "ConnectionString": "[DB CONNECTION STRING]",
    "DatabaseName": "[DB NAME]"
  },
  "BaeAuthentication" : {
    "EncryptionKey": "[TOKEN NENCRYPTION KEY]",
    "TokenLifetimeMinutes": [TOKEN LIFETIME],
    // Ideally should be the server itself
    "Issuer": "[VALID ISSUER]",
    "Audience": "[VALID AUDIENCE]"
  },
}

```
