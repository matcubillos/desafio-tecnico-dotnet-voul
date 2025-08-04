# Prueba t�cnica API .NET 7 Voul
## Herramientas utilizadas
- **Visual Studio 2022** (Community, Professional o Enterprise)
- **.NET 7.0 SDK**
- **SQL Server localdb**
  - **Database : PurchaseOrderDb**

## Base de datos
- **Nombre de la base de datos**: `PurchaseOrderDb`
- Si tienes el compilador VS instalado , puedes usar el **SQL Server Object Explorer** para crear la base de datos.
- Puede verificar si LocalDB est� instalado ejecutando el siguiente comando en la terminal:
```bash
sqllocaldb info
```

## Configuraci�n y conexion a la base de datos
- La cadena de conexi�n a la base de datos se encuentra en `appsettings.json` bajo la clave `ConnectionStrings:DefaultConnection`.
- Aseg�rate de que la base de datos `PurchaseOrderDb` est� creada en tu instancia de SQL Server localdb.

## Compilar y resolver dependencias iniciales:
Ejecuta los siguientes comandos en la terminal
```bash
dotnet restore
```
Luego de restaurar las dependencias, puedes compilar el proyecto para asegurarte de que todo est� configurado correctamente:
```bash
dotnet build
```

## Inicializaci�n y migraciones de la base de datos
- Si usas Visual Studio, abre la Consola del Administrador de Paquetes (Tools > NuGet Package Manager > Package Manager Console).
- Ejecuta el siguiente comando para aplicar las migraciones:
```bash
Update-Database
```
- Alternativamente, puedes usar la l�nea de comandos:
```bash
dotnet ef database update
```
## Ubicacion y configuracion del puerto (solo dev)
- El proyecto est� configurado para ejecutarse en el puerto `5132`.
- Para cambiar el puerto, editar el archivo `Properties/launchSettings.json` y modificar la propiedad `applicationUrl` (de cualquier perfil)

## C�mo Ejecutar la Aplicaci�n
### Opci�n 1: Visual Studio
1. Abrir el archivo `.sln` con Visual Studio 2022
2. Establecer el proyecto de inicio (si hay m�ltiples proyectos)
3. Presionar `F5` o hacer clic en "Iniciar depuraci�n"

### Opci�n 2: L�nea de Comandos
```bash
dotnet run
```

### Opci�n 3: Visual Studio Code
1. Abrir la carpeta del proyecto en VS Code
2. Instalar la extensi�n "C# Dev Kit"
3. Usar `Ctrl+F5` para ejecutar sin depuraci�n

# URLs
#### Swagger: http://localhost:5132/swagger/index.html
#### Api: http://localhost:5132/api/v1/

### Versiones
- Microsoft.AspNetCore.Mvc.Versioning 5.1.0 (versionado de API)
- Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer 5.1.0 (explorador para versionado en Swagger)
- Microsoft.AspNetCore.OpenApi 7.0.20 (integraci�n con OpenAPI/Swagger)
- Microsoft.EntityFrameworkCore 7.0.20 (ORM para acceso a datos)
- Microsoft.EntityFrameworkCore.SqlServer 7.0.20 (proveedor EF Core para SQL Server)
- Microsoft.EntityFrameworkCore.Tools 7.0.20 (herramientas para migraciones y scaffolding)
