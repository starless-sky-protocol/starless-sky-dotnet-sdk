# Starless Sky .NET SDK

The Starless Sky .NET SDK allows easy communication with Starless Sky networks without having to write a lot of API-integration code. We primarily chose .NET because it is widely supported by the developer community and it is cross-platform.

To start developing with the .NET SDK, install Visual Studio (recommended) or Visual Studio Code. You can download them [here](https://visualstudio.microsoft.com/).

> The .NET SDK is not included in the Visual Studio Code installation. You can download .NET [here](https://dotnet.microsoft.com/en-us/download).

Currently the project is targeting **.NET 5** and **.NET 6**. Both of this versions are supported by the library.

## Installation

The repository is not available in the nuget manager (yet) but you can clone the latest stable version of the SDK and reference it in your project.

After cloning and refering it on your project, you will have access to `StarlessSky.Core` classes.

```csharp
using System;
using StarlessSky.Core.API;
using StarlessSky.Core.Module;
using StarlessSky.Core.Entity;

var networkAddress = new Uri("https://localhost:8080/");
var networkInstance = new StarlessSkyNetworkInstance(networkAddress);

// Send a message
string privateKeyFrom = "xxxxxxx";
string publicKeyTo = "0xabc123456";

MessageProvider msgProvider = new MessageProvider(networkInstance);
MessageBody msgContent = new MessageBody(content: "Hello, world!", subject: "Open this");
var result = msgProvider.SendMessage(privateKeyFrom, publicKeyTo, msgContent);

if(result.Success) {
    Console.WriteLine("Message sent!");
}
```