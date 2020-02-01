![FeedlySharp](https://raw.githubusercontent.com/Zettersten/FeedlySharp/master/FeedlySharp/feedly.png "FeedlySharp")
# FeedlySharp

![Build history](https://buildstats.info/azurepipelines/chart/nenvy/FeedlySharp/8)

An HTTP Client that interfaces with Feedly Cloud API. Millions of users depend on their feedly for inspiration, information, and to feed their passions. But one size does not fit all. Individuals have different workflows, different habits, and different devices. In our efforts to evolve feedly from a product to a platform, we have therefore decided to open up the feedly API. Developers are welcome to deliver new applications, experiences, and innovations via the feedly cloud. We feel strongly that this will help to accelerate innovation and better serve our users.

[![Build Status](https://dev.azure.com/nenvy/FeedlySharp/_apis/build/status/Zettersten.FeedlySharp?branchName=master)](https://dev.azure.com/nenvy/FeedlySharp/_build/latest?definitionId=8&branchName=master) [![NuGet Badge](https://buildstats.info/nuget/FeedlySharp)](https://www.nuget.org/packages/FeedlySharp/)


## Installation

```bash
dotnet add package FeedlySharp
```

or from the VS Package Manager

```powershell
Install-Package FeedlySharp
```

## Adding FeedlySharp to your project

### Instatiate client by providing `FeedlyOptions`

```csharp
var feedlySharp = new FeedlySharpHttpClient(FeedlyOptions); 
```

### Instatiate client by using the `IServiceCollection` (default ASP.NET Core DI)

```csharp
services.AddFeedlySharp();
```

*Note: By default, FeedlySharp will look at `IConfiguration` for `FeedlyOptions`*

```
configuration[$"Feedly:{nameof(FeedlyOptions.AccessToken)}"]
configuration[$"Feedly:{nameof(FeedlyOptions.RefreshToken)}"]
configuration[$"Feedly:{nameof(FeedlyOptions.UserID)}"]
configuration[$"Feedly:{nameof(FeedlyOptions.Domain)}"]
```

#### FeedlyOptions

```csharp
public class FeedlyOptions
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }

    public string UserID { get; set; }

    public string Domain { get; set; } = "https://cloud.feedly.com/";
}
```

*Note: You can also provide `IOptions<FeedlyOptions>`*

## Supported Features

*See https://developer.feedly.com/ for more details*

- [x] Entries
- [x] Streams
- [x] Profile
- [x] Collections
- [ ] Feeds
- [ ] Markers
- [ ] Mixes
- [ ] Preferences
- [ ] Priorties
- [ ] Notes & Highlights
- [ ] Tags
- [ ] Subscriptions
- [ ] Search
