Converting to, and from, [base64url](https://en.wikipedia.org/wiki/Base64#RFC_4648)

[base64urls](https://www.nuget.org/packages/base64urls) offer you to run [Base64UrlCore](https://www.nuget.org/packages/Base64UrlCore) on the CLI.

## Install

```bash
Install-Package Base64UrlCore
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

## License

MIT
