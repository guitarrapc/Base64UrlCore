[![CircleCI](https://circleci.com/gh/guitarrapc/Base64UrlCore.svg?style=svg)](https://circleci.com/gh/guitarrapc/Base64UrlCore) [![codecov](https://codecov.io/gh/guitarrapc/Base64UrlCore/branch/master/graph/badge.svg)](https://codecov.io/gh/guitarrapc/Base64UrlCore) [![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) 

## Base64UrlCore 

Converting to, and from, [base64url](https://en.wikipedia.org/wiki/Base64#RFC_4648)

[base64urls](https://www.nuget.org/packages/base64urls) offer you to run [Base64UrlCore](https://www.nuget.org/packages/Base64UrlCore) on the CLI.

This project is using `nullable reference type`.

## Install

### CLI

[![NuGet](https://img.shields.io/nuget/v/base64urls.svg)](https://www.nuget.org/packages/base64urls)

```bash
dotnet tool install -g base64urls
```

### Library

[![NuGet](https://img.shields.io/nuget/v/Base64UrlCore.svg)](https://www.nuget.org/packages/Base64UrlCore)

```bash
dotnet add package Base64UrlCore
```

## CLI Usage

### SYNTAX

`base64urls [-version] [-help] [encode|decode|escape|unescape] [args]`

### Decode

base64url encode input. Input should be a string or a Buffer.

```bash
$ base64urls decode QyMgaXMgYXdlc29tZQ==
C# is awesome
```

### Encode

Convert a base64url encoded string into a raw string. The encoding argument can be used if the input is a string that's not utf8.

```bash
$ base64urls encode "C# is awesome."
QyMgaXMgYXdlc29tZS4=
```

### Escape

Convert a base64 encoded string to a base64url encoded string.

```bash
$ base64urls escape "This+is/goingto+escape=="
This-is_goingto-escape
```

### Unescape

Convert a base64url encoded string to a base64 encoded string.

```bash
$ base64urls unescape "This-is_goingto-escape"
This+is/goingto+escape==
```

## Library Usage

### Decode

base64url encode input. Input should be a string or a Buffer.

```csharp
Base64Url.Decode("QyMgaXMgYXdlc29tZQ==");
// C# is awesome
```

### Encode

Convert a base64url encoded string into a raw string. The encoding argument can be used if the input is a string that's not utf8.

```csharp
Base64Url.Encode("C# is awesome.");
// QyMgaXMgYXdlc29tZS4=
```

### Escape

Convert a base64 encoded string to a base64url encoded string.

```csharp
Base64Url.Escape("This+is/goingto+escape==");
// "This-is_goingto-escape" 
```

### Unescape

Convert a base64url encoded string to a base64 encoded string.

```csharp
Base64Url.Unescape("This-is_goingto-escape");
// "This+is/goingto+escape=="
```

### PadString

Add padding to encoded string.

```csharp
Base64Url.PadString("aG9nZW1vZ2U");
// aG9nZW1vZ2U=
```

### RemovePadding

Remove padding from encoded string.

```csharp
Base64Url.RemovePadding("MQ==");
// MQ
```

## Docker

Library

```
docker build -t base64urlcore . -f Dockerfile.Base64UrlCore
```

global tool

```
docker build -t base64urls . -f Dockerfile.base64urls
```

## CI

Retired appveyor, now using CircleCI.

### Local run

validate ci config.

```
docker run --rm -v $(pwd):/data circleci/circleci-cli:alpine config validate /data/.circleci/config.yml
```

### Code Coverage

Using Coverlet and dotnet-reportgenerator.
Add `coverlet.msbuild` to your csproj, add dotnet global tool `dotnet tool install -g dotnet-reportgenerator-globaltool`, and add environment variables `CODECOV_TOKEN` to CI. (You can find TOKEN at https://codecov.io/gh/guitarrapc/Base64UrlCore)

### NuGet push

Add environment variable `NUGET_KEY` to CI. You can generate it at https://www.nuget.org/account/apikeys.

## License

MIT
