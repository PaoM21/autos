# Usa una imagen base de .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establece el directorio de trabajo
WORKDIR /app

# Copia los archivos del proyecto y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto de los archivos y construye la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Usa una imagen base para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
COPY --from=build /app/out .

# Expone el puerto 80
EXPOSE 80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "Autos.dll"]
