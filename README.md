
# Docker: Angular 17 - Dotnet 8 (Entity framework) - Postgres & PgAdmin  

Proyecto con tecnologias que se buscan correr directamente desde docker 

## Instrucciones Backend y Frontend
* Antes de correr el archivo docker compose es importante eliminar los volumenes del front y del backend (esto para que se puedan generar los archivos necesarios dentro de los contenedores)

demtro del Docker-compose comentar: 

    volumes:
      - ./frontend:/app/${APP_NAME}-fe
    
    volumes:
      - ./backend:/app/Slamdunk.WebApi
    
* Ejecutar el comando docker compose

* Luego es importante Copiar desde el contenedor de docker hacia el host los archivos con el comando: 
    * (Dotnet) docker cp slamdunk-webapi:/app/Slamdunk.WebApi/. /backend
    * (Angular) docker cp slamdunk-fe:/app/slamdunk-fe/. /frontend
    * IMPORTANTE Slamdunk ES UN NOMBRE DEFINIDO EN EL .VAR

* Una vez copiado los archivos se deve volver a agregar el volumen de los contenedores de backende y frontend

## Instrucciones Postgres
* Aceeder al PgAdmin desde el puerto localhost:80
* Credenciales:
    * User: admin@admin.com
    * Pass: admin
* Credenciales de conexion pgdb:
    * HOSTNAME: postgres (el nombre es ya que viene desde un contenedor con ese nombre)
    * PORT: 5432
    * USER: admin
    * PASS: admin

## Creacion de tablas
Como se usa Entity framework se usan las migraciones: 

    exec Slamdunk.WebApi dotnet ef database update
    