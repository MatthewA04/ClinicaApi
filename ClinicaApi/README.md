# 🏥 ClinicaApi - Sistema de Gestión de Citas Médicas (API RESTful)

API RESTful desarrollada en **C# con .NET (ASP.NET Core)** diseñada para automatizar el registro, consulta, actualización, cambio de estado y eliminación de citas médicas de una clínica privada, reemplazando el control manual en agendas físicas por una arquitectura en capas desacoplada y escalable.

---

## 👥 Integrantes y División de Roles del Equipo

El desarrollo del proyecto se organizó de manera colaborativa entre los tres integrantes del equipo:

| Integrante | Rol Asignado | Responsabilidades Técnicas |
| :--- | :--- | :--- |
| **Matthew Ameri Davila** | **Arquitectura Backend & Lógica de Negocio** | Diseño de la solución en capas (`Models`, `Services`, `Controllers`), implementación de la interfaz `ICitaService`, simulación de persistencia en memoria (`CitaMemoryService` como `Singleton`) y configuración del repositorio en GitHub. |
| **Isabel Banda** | **Contratos DTO, Validaciones & Manejo de Errores** | Definición de `CitaCreacionDto` y `CitaRespuestaDto`, implementación de validaciones con `DataAnnotations` (`[Required]`, `[StringLength]`, `[RegularExpression]`) y configuración del manejador global de excepciones (`GlobalExceptionHandler`) bajo el estándar **RFC 7807 (`ProblemDetails`)**. |
| **Viviana Ríos** | **Documentación OpenAPI/Scalar & Pruebas QA en Postman** | Configuración de la interfaz interactiva **Scalar**, diseño de la colección de pruebas funcionales y negativas en **Postman** con scripts automatizados (`pm.test`), redacción del `README.md` e informe técnico. |

---

## 🏗️ Arquitectura del Proyecto

La solución sigue las buenas prácticas de ingeniería de software, manteniendo controladores delgados (*Thin Controllers*) y aplicando inyección de dependencias:

```text
ClinicaApi/
├── Controllers/
│   └── CitasController.cs          # Endpoints RESTful y códigos de estado HTTP semánticos
├── DTOs/
│   ├── CitaCreacionDto.cs          # Contrato de entrada con validaciones DataAnnotations
│   └── CitaRespuestaDto.cs         # Contrato de salida limpio hacia el cliente
├── Models/
│   ├── Cita.cs                     # Entidad principal de cita médica
│   ├── Medico.cs                   # Entidad médico y especialidad
│   └── Paciente.cs                 # Entidad paciente y documento de identidad
├── Services/
│   ├── ICitaService.cs             # Interfaz de contrato de negocio
│   └── CitaMemoryService.cs        # Persistencia simulada en memoria con List<Cita>
├── Exceptions/
│   └── GlobalExceptionHandler.cs   # Middleware centralizado de errores RFC 7807 (ProblemDetails)
├── Program.cs                      # Configuración de servicios, DI (Singleton) y Scalar
└── README.md                       # Documentación general del repositorio
```

---

## ⚙️ Prerrequisitos e Instalación

Antes de ejecutar el proyecto en un entorno local, asegúrate de contar con las siguientes herramientas instaladas:

* **.NET SDK 8.0 o superior** (desarrollado y probado en **.NET 10**).
  * *Verificación en consola:* `dotnet --version`
* **IDE recomendado:** Visual Studio 2022 (con la carga de trabajo *Desarrollo de ASP.NET y web*) o Visual Studio Code (con la extensión *C# Dev Kit*).
* **Cliente API:** Postman Desktop (para ejecutar la colección de pruebas automatizadas).
* **Control de versiones:** Git.

---

## 🚀 Pasos para Ejecutar el Proyecto Localmente

1. **Clonar el repositorio desde GitHub:**
   ```bash
   git clone [https://github.com/TU_USUARIO/ClinicaApi.git](https://github.com/TU_USUARIO/ClinicaApi.git)
   ```

2. **Navegar hacia la carpeta del proyecto donde se encuentra el archivo `.csproj`:**
   ```bash
   cd ClinicaApi/ClinicaApi
   ```
   *(Nota: Si clonaste directamente sobre la carpeta raíz del proyecto, usa `cd ClinicaApi`)*.

3. **Restaurar dependencias y compilar la solución:**
   ```bash
   dotnet restore
   dotnet build
   ```

4. **Ejecutar el servidor local mediante consola:**
   ```bash
   dotnet run
   ```

5. **Verificar los puertos de escucha en la terminal:**
   Una vez iniciada la aplicación, la consola mostrará las direcciones activas en entorno `Development`:
   * **HTTPS:** `https://localhost:7069`
   * **HTTP:** `http://localhost:5070`

---

## 📖 Cómo Llegar a la Documentación Interactiva (Scalar)

Este proyecto incorpora **Scalar** conectado al esquema nativo de OpenAPI para visualizar los contratos de datos (`DTOs`) y ejecutar peticiones en vivo.

1. Mantén el servidor activo ejecutándose con `dotnet run` (o desde Visual Studio).
2. Abre tu navegador web de preferencia (Chrome, Edge, Firefox).
3. Ingresa directamente a la siguiente ruta:
   ```text
   https://localhost:7069/scalar/v1
   ```
4. En el panel izquierdo de Scalar selecciona el grupo **Citas**, elige cualquier endpoint (`GET`, `POST`, `PUT`, `PATCH`, `DELETE`), completa los parámetros o el cuerpo JSON y haz clic en **Send** para ver la respuesta en tiempo real.

---

## 🔌 Endpoints de la API (`api/citas`)

| Método | Ruta | Descripción | Código Éxito | Código Error |
| :--- | :--- | :--- | :--- | :--- |
| `GET` | `/api/citas` | Obtiene la lista completa de citas registradas. | `200 OK` | - |
| `GET` | `/api/citas/{id}` | Consulta una cita específica por su identificador. | `200 OK` | `404 Not Found` |
| `POST` | `/api/citas` | Registra una nueva cita médica previa validación. | `201 Created` | `400 Bad Request` |
| `PUT` | `/api/citas/{id}` | Actualiza todos los datos de una cita existente. | `200 OK` | `400` / `404` |
| `PATCH` | `/api/citas/{id}/estado` | Modifica únicamente el estado (`Confirmada`, `Cancelada`). | `200 OK` | `404 Not Found` |
| `DELETE` | `/api/citas/{id}` | Elimina permanentemente una cita de la lista en memoria. | `204 No Content` | `404 Not Found` |

### Ejemplos de Petición y Respuesta (JSON)

#### 1. Registrar nueva cita (`POST /api/citas`)
**Request Body:**
```json
{
  "nombrePaciente": "Valeria Gómez",
  "dniPaciente": "45678912",
  "nombreMedico": "Dr. Luis Paredes",
  "especialidadMedico": "Dermatología",
  "fechaHora": "2026-12-01T15:30:00",
  "motivoConsulta": "Evaluación dermatológica"
}
```

**Response (`201 Created`):**
```json
{
  "id": 2,
  "paciente": "Valeria Gómez",
  "dni": "45678912",
  "medico": "Dr. Luis Paredes",
  "especialidad": "Dermatología",
  "fechaHora": "2026-12-01T15:30:00",
  "estado": "Pendiente",
  "motivoConsulta": "Evaluación dermatológica"
}
```

#### 2. Manejo de Error por Recurso Inexistente (`GET /api/citas/999`)
**Response (`404 Not Found` - RFC 7807 ProblemDetails):**
```json
{
  "title": "Recurso no encontrado",
  "status": 404,
  "detail": "No se encontró la cita con ID 999.",
  "instance": "/api/citas/999"
}
```

---

## 🧪 Pruebas Automatizadas en Postman (`pm.test`)

La colección de Postman valida tanto los flujos exitosos como los escenarios de error controlado (`400 Bad Request` y `404 Not Found`) mediante scripts de aserción en la pestaña **Scripts -> Post-response**:

### 1. Listar todas las citas (`GET /api/citas`)
```javascript
pm.test("Estado HTTP es 200 OK", function () {
    pm.response.to.have.status(200);
});

pm.test("La respuesta es un arreglo (lista) de citas", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData).to.be.an("array");
});
```

### 2. Registrar nueva cita (`POST /api/citas`)
```javascript
pm.test("Estado HTTP es 201 Created", function () {
    pm.response.to.have.status(201);
});

pm.test("Retorna el ID generado y estado Pendiente", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.id).to.be.above(0);
    pm.expect(jsonData.estado).to.eql("Pendiente");
});
```

### 3. Validación de DTO inválido (`POST /api/citas` - Prueba Negativa)
```javascript
pm.test("Estado HTTP es 400 Bad Request por validación", function () {
    pm.response.to.have.status(400);
});
```

### 4. Consultar cita inexistente (`GET /api/citas/999` - Prueba ProblemDetails)
```javascript
pm.test("Estado HTTP es 404 Not Found", function () {
    pm.response.to.have.status(404);
});

pm.test("Respuesta cumple formato ProblemDetails (RFC 7807)", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.status).to.eql(404);
    pm.expect(jsonData.title).to.eql("Recurso no encontrado");
});
```

### 5. Actualizar cita existente (`PUT /api/citas/1`)
```javascript
pm.test("Estado HTTP es 200 OK", function () {
    pm.response.to.have.status(200);
});

pm.test("El nombre del paciente fue actualizado", function () {
    var jsonData = pm.response.json();
    pm.expect(jsonData.paciente).to.eql("Juan Pérez Actualizado");
});
```

### 6. Eliminar cita (`DELETE /api/citas/2`)
```javascript
pm.test("Estado HTTP es 204 No Content", function () {
    pm.response.to.have.status(204);
});
```