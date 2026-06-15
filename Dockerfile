# Estructura de compilación (SDK de .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# 1. Copiar la solución global
COPY S13MauricioCV.ReportData.sln ./

# 2. Copiar cada archivo de proyecto (.csproj) recreando sus carpetas respectivas
# Esto es vital para que 'dotnet restore' funcione correctamente en multiproyectos
COPY Application/Application.csproj ./Application/
COPY Domain/Domain.csproj ./Domain/
COPY Infraestructure/Infraestructure.csproj ./Infraestructure/
COPY S13MauricioCV.ReportData/S13MauricioCV.ReportData.csproj ./S13MauricioCV.ReportData/

# 3. Restaurar las dependencias de toda la solución
RUN dotnet restore S13MauricioCV.ReportData.sln

# 4. Copiar todo el resto del código fuente al contenedor
COPY . ./

# 5. Compilar y publicar apuntando específicamente al proyecto ejecutable (el backend)
RUN dotnet publish S13MauricioCV.ReportData/S13MauricioCV.ReportData.csproj -c Release -o out

# Imagen de producción (Runtime de .NET 10)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configurar el puerto para Render
ENV ASPNETCORE_URLS=http://+:10000

# Comando para arrancar la app apuntando a la DLL del proyecto ejecutable
ENTRYPOINT ["dotnet", "S13MauricioCV.ReportData.dll"]