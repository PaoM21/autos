# autos
1. Clonar repositorio con el siguiente comando ```git clone https://github.com/PaoM21/autos.git```.
2. Después de abrir el proyecto en el entorno de desarrollo preferido, descargue los siguientes NuGet: _Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.Design, Npgsql.EntityFrameworkCore.PostgreSQL, Microsoft.EntityFrameworkCore.InMemory, FakeItEasy, FluentAssertions_.
3. Cree una base de datos PostgreSQL en pgAdmin con nombre autos.
4. Ejecute en visual Studio el comando _dotnet ef migrations add InitialMigration --project Autos.csproj_ para crear la carpeta migración.
5. Ejecute en visual Studio el comando _dotnet ef database update InitialMigration --project Autos.csproj_ para ver los cambios en la base de datos.
6. Ejecute finalmente el proyecto y encontrará el CRUD.
