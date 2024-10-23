# Usa una imagen base de .NET SDK para la fase de construcción
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Establece el directorio de trabajo
WORKDIR /app

# Copia el archivo de proyecto y restaura las dependencias
COPY Autos/*.csproj ./Autos/
RUN dotnet restore ./Autos/Autos.csproj

# Copia el resto de los archivos del proyecto
COPY . .

# Construye el proyecto
RUN dotnet publish ./Autos/Autos.csproj -c Release -o out

# Usa una imagen de .NET Runtime para la fase de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Define el comando de entrada
ENTRYPOINT ["dotnet", "Autos.dll"]
