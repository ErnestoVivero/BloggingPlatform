# Blogging Platform API

Este proyecto es una API REST robusta para una plataforma de Blogging, desarrollada con un enfoque de ingeniería de software empresarial. El objetivo principal es demostrar la implementación de patrones de diseño avanzados, desacoplamiento tecnológico y automatización de pruebas.

## 🚀 Arquitectura y Patrones de Diseño

La solución está estructurada bajo los principios de **Clean Architecture** e impulsada por **Domain-Driven Design (DDD)**, dividida en las siguientes capas:

- **Domain:** Contiene las entidades de negocio con un modelo rico (encapsulamiento y validación de reglas de negocio internas) e interfaces de repositorios. Libre de dependencias externas.
- **Application:** Implementa los Casos de Uso del sistema (Servicios), DTOs de entrada/salida y mapeos abstractos.
- **Infrastructure:** Resuelve la persistencia de datos utilizando **Entity Framework Core (Code-First)** sobre **SQL Server**. Implementa de forma estricta los patrones *Repository* y *Unit of Work* para garantizar la atomicidad en las transacciones.
- **API (Presentation):** Capa delgada de controladores expuestos mediante endpoints REST, configurada con soporte nativo para **OpenAPI / Swagger**.

## 🛠️ Tecnologías y Herramientas

- **Backend:** .NET 10 / C#
- **ORM:** Entity Framework Core (con configuraciones mapeadas por Ensamblado `IEntityTypeConfiguration`)
- **Base de Datos:** SQL Server
- **Testing:** xUnit & Moq (Pruebas unitarias de servicios y de lógica pura de dominio)
- **DevOps:** GitHub Actions (Pipeline de Integración Continua para validación automática de compilación y pruebas)

## 🧪 Estrategia de Testing

El proyecto cuenta con una suite de pruebas automatizadas enfocada en la calidad del software:
- **Pruebas de Comportamiento:** Validación de la orquestación de servicios y el flujo correcto de llamadas hacia la capa de persistencia simulada con *Moq*.
- **Pruebas de Dominio:** Validación asilada y de alta velocidad de las reglas de consistencia de las entidades ricas, garantizando que el estado del negocio sea inmutable ante fallos.

## 🏃‍♂️ Cómo ejecutar el proyecto localmente

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/ErnestoVivero/BloggingPlatform.git
2. Configurar la cadena de conexión en el archivo appsettings.json de la capa BloggingPlatform.API.
3. Ejecutar las migraciones desde la raíz de la solución para crear la base de datos:
   ```bash
   dotnet ef database update --project BloggingPlatform.Infrastructure --startup-project BloggingPlatform.API
4. Iniciar la aplicación:
   ```bash
   dotnet run --proyect BloggingPlatform.API
