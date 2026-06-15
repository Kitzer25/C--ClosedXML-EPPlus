# Estructura de compilación (SDK de .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Copiar archivos de proyecto y restaurar dependencias
COPY *.sln ./
COPY */*.csproj ./
# Nota: Si tu estructura es simple y solo hay un .csproj, puedes usar: COPY *.csproj ./
RUN dotnet restore

# Copiar todo lo demás y compilar
COPY . ./
RUN dotnet publish -c Release -o out

# Imagen de producción (Runtime de .NET 10)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configurar el puerto para Render (Render asigna uno dinámicamente mediante la variable PORT)
ENV ASPNETCORE_URLS=http://+:10000

# Comando para arrancar la app (Cambia 'TuProyecto.dll' por el nombre real de tu archivo de salida)
ENTRYPOINT ["dotnet", "TuProyecto.dll"]