# DeezNET
[![Version](https://img.shields.io/nuget/v/DeezNET.svg)](https://nuget.org/packages/DeezNET)

A .NET Deezer API wrapper and track downloading library. There's a CLI tool in there as well.

This library hasn't been super tested, but it's not that complex so it should be fine.

## Dear Deezer
If you would like this repository to be taken down, please send me a cease and desist.<br>
You may e-mail it to me here: [me@trev.app](mailto:me@trev.app).

Or go through the standard GitHub DMCA procedure, but that isn't as fun.

## Dependencies
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json) for every API call
- [BouncyCastle.Cryptography](https://www.nuget.org/packages/BouncyCastle.Cryptography) for decrypting track data (`client.Downloader.GetRawTrackBytes()`)
- [TagLibSharp](https://www.nuget.org/packages/TagLibSharp) for applying metadata to decrypted track data (`client.Downloader.ApplyMetadataToTrackBytes()`)

## Overview
DeezNET is built around a core class, `DeezerClient`. That class itself does very little, under it is `Downloader`, `GWApi`, and `PublicApi`.
- `Downloader` provides functions for downloading tracks by their ID, as well as applying metadata. It requires an ARL supplied to `DeezerClient` to function.
- `GWApi` is a wrapper for the backend API used by Deezer. It requires an ARL supplied to `DeezerClient` to function.
- `PublicApi` is a wrapper for the public Deezer API. It does not require an ARL.

All API calls return a Newtonsoft.JSON JToken as I did not want to deal with parsing everything into model classes since there are many different API endpoints.

In addition to `DeezerClient`, there is `DeezerURL` which is a class for parsing Deezer URLs into their entity type and ID. It also handles unshortening the standard Deezer share URLs (deezer.page.link).

## Examples

### Getting Track Info (`PublicApi`)
```cs
var client = new DeezerClient();
var trackData = await client.PublicApi.GetTrack(1903638027);
Console.WriteLine($"{trackData["title"]!} by {trackData["contributors"]!.First()["name"]!}");
// Output: Let You Down by Dawid Podsiadło
```

### Getting Track Info (`GWApi`)
```cs
var client = new DeezerClient();
await client.SetARL("[ARL]");
var trackData = await client.GWApi.GetTrack(1903638027);
Console.WriteLine($"{trackData["SNG_TITLE"]!} by {trackData["ART_NAME"]!}");
// Output: Let You Down by Dawid Podsiadło
```

### Downloading a Track by ID
```cs
var client = new DeezerClient();
await client.SetARL("[ARL]");
var trackBytes = await client.Downloader.GetRawTrackBytes(1903638027, DeezNET.Data.Bitrate.FLAC);
trackBytes = await client.Downloader.ApplyMetadataToTrackBytes(1903638027, trackBytes); // if you want metadata
File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, "LYD.flac"), trackBytes);
// Saves a metadata-applied FLAC of Let You Down by Dawid Podsiadło to your current working directory
```

### Downloading an Album by URL
```cs
var client = new DeezerClient();
await client.SetARL("[ARL]");
var urlData = DeezerURL.Parse("https://deezer.page.link/uwdUFsjkJbGkngSm7"); // this is a short URL, can also be a full one like "https://www.deezer.com/us/album/548556802"
var tracksInAlbum = await urlData.GetAssociatedTracks(client);

foreach (var track in tracksInAlbum)
{
    var trackBytes = await client.Downloader.GetRawTrackBytes(track, DeezNET.Data.Bitrate.FLAC);
    trackBytes = await client.Downloader.ApplyMetadataToTrackBytes(track, trackBytes); // if you want metadata
    File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, $"{track}.flac"), trackBytes);
}
// Saves metadata-applied FLACs of every track in GLOOM DIVISION by I DONT KNOW HOW BUT THEY FOUND ME to your current working directory
```

## DeezCLI
```
USAGE
  DeezCLI <url> [options]

DESCRIPTION
  Downloads the given URL.

PARAMETERS
* url               The URL of the item to download. Can be a shortened URL.

OPTIONS
  -b|--bitrate      The preferred bitrate when downloading. Falls back to a lower quality if unavailable. Choices: "MP3_128", "MP3_320", "FLAC". Default: "FLAC".
  -m|--add-metadata  Whether to attach metadata to the downloaded audio file. Default: "True".
  -o|--output       The directory to save downloaded media to. Default: Current Working Directory.
  -a|--arl          The account ARL to download with. A paid plan allows for higher quality downloads. Some regions do not have full tracks available without a premium account. Default: "".
  -l|--top-limit    The max amount of tracks to download. Only applicable when downloading an artist's top tracks. Default: "100".
  -c|--concurrent   The max amount of allowed concurrent track downloads. Default: "3".
  -d|--folder-template  The folder path template to use when saving tracks to file. Default: "%albumartist%/%album%/".
  -f|--file-template  The file path template to use when saving tracks to file. Default: "%track% - %title%.%ext%".
  -h|--help         Shows help text.
  --version         Shows version information.
```

```
AVAILABLE TEMPLATE VARIABLES
  %title%
  %album%
  %albumartist%
  %artist%
  %albumartists%
  %artists%
  %track%
  %trackcount%
  %trackid%
  %albumid%
  %artistid%
  %ext%
  %year%
```

## Credits
- This project is heavily based on [deemix-py](https://gitlab.com/RemixDev/deemix-py) and [deezer-py](https://gitlab.com/RemixDev/deezer-py), both under the [GNU GPLv3 License](https://gitlab.com/RemixDev/deemix-py/-/blob/main/LICENSE.txt).
