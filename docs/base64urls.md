## Base64UrlCore

Converting to, and from, [base64url](https://en.wikipedia.org/wiki/Base64#RFC_4648)

[base64urls](https://www.nuget.org/packages/base64urls) offer you to run [Base64UrlCore](https://www.nuget.org/packages/Base64UrlCore) on the CLI.

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

## License

MIT
