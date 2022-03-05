## SSL Server Demo
An SSL enabled client project and server project in dotnet/C#. Server certification, no client certification.

Built on top of [NetCoreServer](https://github.com/chronoxor/NetCoreServer) and using [BetterConsoleUITemplate](https://github.com/Ink230/BetterConsoleUITemplate).

## Windows / Linux / Dev IDE
- Configure Environment Variables as structured in the AppSettings folder, on your O/S or desired means

## Server Setup
- Change to desired port in SSLServerDemo.cs

  - ```int port = 5555```

- Shouldn't have to touch IPAddress.Any

## Client Setup
- Change to desired port in SSLClientDemo.cs

  - ```int port = 5555```

- Enter the appropriate public facing address (localhost, eth0, public server IP...etc) in SSLClientDemo.cs

  - ```string address = "127.0.0.1"```

## Development Self-Signed .pfx
- Configure OpenSSL in powershell / bash ...etc
- Run

  - ```openssl req -x509 -sha256 -nodes -days 365 -newkey rsa:4096 -keyout server.key -out server.crt```

  - ```openssl pkcs12 -export -out server.pfx -inkey server.key -in server.crt```

- Place the server.pfx file in a filepath reachable by SSLServerDemo.cs 
- Place the password in SSLServerDemo.cs where noted or use secrets.json configuration

## localhost
- Should work on Windows or Linux (file perms needed on Linux potentially)

## Development Testing on Ubuntu 18.04 Remote Server Setup
- Setup ufw
- Run

  - ```ufw allow 5555/tcp any```

  - ```ufw reload```

- Publish SSLServerDemo for linux
- Have dotnet runtime on linux
- Run

  - ```dotnet SSLServerDemo.dll```

- Connect with SSLClientDemo
