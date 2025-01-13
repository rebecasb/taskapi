# Guía de Implementación de TaskAPI

# 1. Clonar el repositorio en tu computadora
Abre la terminal o línea de comandos en tu computadora.
Navega hasta la carpeta donde deseas clonar el repositorio.
Ejecuta el siguiente comando:
git clone https://github.com/rebecasb/taskapi.git
Presiona Enter para clonar el repositorio.

# 2. Abrir el proyecto en Visual Studio (o tu IDE preferido)
Navega hasta la carpeta donde clonaste el repositorio.
Haz doble clic en el archivo TaskAPI.sln para abrir el proyecto en Visual Studio.

# 3. Ejecutar script de base de datos
Ejecuta el archivo "script_tareasdb.sql" que se encuentra en el repositorio para crear la base de datos y las tablas del proyecto. 

# 4. Actualizar la cadena de conexión en el proyecto
En Visual Studio, abre el archivo appsettings.json en la carpeta raíz del proyecto.
Actualiza la cadena de conexión en la sección "ConnectionStrings" con la cadena de conexión apropiada para tu base de datos. Aquí te proporciono un ejemplo:
"ConnectionStrings": {
    "dbTarea": "Server=(localdb)\\mssqllocaldb;Database=TAREASDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Asegúrate de reemplazar (localdb)\\mssqllocaldb, y Trusted_Connection=True con los valores apropiados para tu entorno.


# 5. Restaurar paquetes NuGet
En Visual Studio, selecciona "Herramientas" > "Administrador de paquetes NuGet" > "Restaurar".
Esto descargará e instalará todos los paquetes NuGet necesarios para el proyecto.

# 6. Ejecutar la aplicación
En Visual Studio, presiona F5 o haz clic en el botón "Iniciar" para ejecutar la aplicación.
La API debería iniciarse y estar disponible en https://localhost:7078 para HTTPS y http://localhost:5105 para HTTP.

# 7. Postman Collection para pruebas de la API
En el repositorio se encuentra el archivo "TaskAPI.postman_collection.json" para poder realizar prueba de la API y los endpoints desde Postman.
