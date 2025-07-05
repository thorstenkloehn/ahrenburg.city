# ahrenburg.city

Ein ASP.NET Core MVC-Projekt mit PostgreSQL-Datenbank und Blog-Funktion.

## Features
- Benutzerverwaltung mit ASP.NET Identity
- Blogsystem: Nur Admin kann Artikel erstellen/bearbeiten, Nutzer können lesen
- Modernes, responsives Layout (Bootstrap)
- Datenbank: PostgreSQL

## Voraussetzungen
- .NET 9 SDK
- PostgreSQL-Datenbank

## Installation
1. Repository klonen:
   ```bash
   git clone https://github.com/DEIN-BENUTZERNAME/ahrenburg.city.git
   cd ahrenburg.city
   ```
2. Datenbank und Benutzer in PostgreSQL anlegen (siehe `appsettings.json`)
3. Migrationen anwenden:
   ```bash
   dotnet ef database update
   ```
4. Anwendung starten:
   ```bash
   dotnet run
   ```

## Konfiguration
- Datenbankverbindung und Admin-Mail in `appsettings.json` anpassen.

## Deployment
Für andere Rechner:
```bash
dotnet publish -c Release -o ./publish
```
Verzeichnis `publish` auf Zielsystem kopieren und dort starten:
```bash
Systemd-Service-Datei erstellen (optional):

1. Neue Service-Datei anlegen:
    ```bash
    sudo nano /etc/systemd/system/ahrenburg.city.service
    ```

2. Beispielinhalt für die Service-Datei:
    ```ini
    [Unit]
    Description=ASP.NET Core ahrenburg.city
    After=network.target

    [Service]
    WorkingDirectory=/pfad/zum/publish
    ExecStart=/usr/bin/dotnet ahrenburg.city.dll
    Restart=always
    RestartSec=10
    SyslogIdentifier=ahrenburg.city
    User=www-data
    Environment=ASPNETCORE_ENVIRONMENT=Production

    [Install]
    WantedBy=multi-user.target
    ```

    > Passe `WorkingDirectory` und ggf. `User` an deine Umgebung an.

3. Service aktivieren und starten:
    ```bash
    sudo systemctl daemon-reload
    sudo systemctl enable ahrenburg.city
    sudo systemctl start ahrenburg.city
    sudo systemctl status ahrenburg.city
    ```
```
## Nginx als Reverse Proxy installieren (Ubuntu)

1. Nginx installieren:
    ```bash
    sudo apt update
    sudo apt install nginx
    ```

2. Nginx starten und für den Systemstart aktivieren:
    ```bash
    sudo systemctl start nginx
    sudo systemctl enable nginx
    ```

3. Status prüfen:
    ```bash
    sudo systemctl status nginx
    ```

4. Beispielkonfiguration für Reverse Proxy (`/etc/nginx/sites-available/ahrenburg.city`):
    ```nginx
    server {
         listen 80;
         server_name deine-domain.de;

         location / {
              proxy_pass         http://localhost:5000;
              proxy_http_version 1.1;
              proxy_set_header   Upgrade $http_upgrade;
              proxy_set_header   Connection keep-alive;
              proxy_set_header   Host $host;
              proxy_cache_bypass $http_upgrade;
              proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
              proxy_set_header   X-Forwarded-Proto $scheme;
         }
    }
    ```

5. Konfiguration aktivieren und Nginx neu laden:
    ```bash
    sudo ln -s /etc/nginx/sites-available/ahrenburg.city /etc/nginx/sites-enabled/
    sudo nginx -t
    sudo systemctl reload nginx
    ```
## Lizenz
MIT License
