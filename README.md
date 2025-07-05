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
2. Datenbank und Benutzer in PostgreSQL anlegen (siehe `_appsettings.json`)
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

## Produktserver
Für andere Rechner:
```bash
dotnet publish -c Release -o ./publish
dotnet ef migrations script -o update.sql

```
Verzeichnis `publish` auf Zielsystem kopieren und dort starten:
```bash
rsync -avz --delete --exclude 'appsettings.json' ./publish/ user@zielserver:/pfad/zum/zielverzeichnis/
```
> Ersetze `user` und `/pfad/zum/zielverzeichnis/` entsprechend deiner Umgebung.  
> Stelle sicher, dass auf dem Zielsystem .NET installiert ist und die Umgebungsvariablen korrekt gesetzt sind.

Anwendung auf dem Zielsystem starten:
```bash
cd /pfad/zum/zielverzeichnis
dotnet ahrenburg.city.dll
```


> Stelle sicher, dass die Verbindungsdaten in `appsettings.Production.json` oder über Umgebungsvariablen korrekt gesetzt sind und die Datenbank erreichbar ist.
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
 sudo systemctl stop nginx
sudo certbot certonly --standalone -d portal.ahrensburg.city
     ```

3. Status prüfen:
     ```bash
     sudo systemctl status nginx
     ```

4. Beispielkonfiguration für Reverse Proxy (`sudo nano /etc/nginx/conf.d/ahrenburg.conf`):
     ```nginx
   server {
    listen 443 ssl http2;
    listen [::]:443 ssl http2;
    server_name ahrensburg.city;
    ssl_certificate /etc/letsencrypt/live/portal.ahrensburg.city/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/portal.ahrensburg.city/privkey.pem;

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
 
     sudo nginx -t
     sudo systemctl start nginx
     ```

## Weitere Hinweise

- **EF Core Tools:** Stelle sicher, dass die Entity Framework Core Tools installiert sind:
     ```bash
     dotnet tool install --global dotnet-ef
     ```
- **Umgebungsvariablen:** Für produktive Umgebungen empfiehlt es sich, sensible Daten wie Verbindungsstrings über Umgebungsvariablen zu setzen.
- **HTTPS:** Für den produktiven Betrieb sollte ein SSL-Zertifikat (z.B. via Let's Encrypt) für Nginx eingerichtet werden.
- **Backup:** Denke an regelmäßige Backups der Datenbank.

## Lizenz
MIT License

