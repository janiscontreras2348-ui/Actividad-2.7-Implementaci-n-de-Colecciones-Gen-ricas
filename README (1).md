# Gestión de Biblioteca Escolar

Aplicación de escritorio en C# (Windows Forms) para administrar los datos de una biblioteca escolar: libros, autores, editoriales, usuarios, préstamos, reservas y multas. Es el proyecto de la materia **Programación Orientada a Eventos** de la Universidad de Guadalajara, Centro Universitario de Tonalá.

## Integrantes (Equipo 3)

| # | Nombre |
|---|--------|
| 1 | CONTRERAS Rodríguez Janis Isabel |
| 2 | MACÍAS Cruz Meredith Miranda |
| 3 | TORRES Barrera José Ángel |

## Tecnologías

- C# con .NET 10 (`net10.0-windows`)
- Windows Forms
- Visual Studio con la carga de trabajo de desarrollo de escritorio de .NET

## Estructura del proyecto

```
Models.slnx
Models/
├── Biblioteca.csproj
├── Program.cs
├── Models/                 Capa de modelos (lógica de negocio)
│   ├── Administrador.cs
│   ├── Autores.cs
│   ├── Editorial.cs
│   ├── Genero.cs
│   ├── Libros.cs
│   ├── Multa.cs
│   ├── Persona.cs
│   ├── Prestamos.cs
│   ├── Reserva.cs
│   └── Usuarios.cs
└── (formularios)           FrmPrincipal, FrmBase, FrmLibros, FrmEditorial, etc.
```

Los modelos heredan de `EntidadBase` e implementan la interfaz `IAlmacenamientoCRUD` (insertar, consultar, actualizar y eliminar). Los formularios heredan de `FrmBase` e implementan `IPanelCRUD`, de modo que cada pantalla reutiliza el mismo panel de captura. La persistencia se simula en memoria (RAM) con colecciones.

## Actividad 2.7: colecciones con `List<T>`

Esta actividad agrega una relación maestro-detalle **Editorial 1 → \* Libros**. La clase `Editorial` guarda sus libros en una colección genérica y el formulario `FrmEditorial` los muestra en un `DataGridView` sincronizado con el modelo.

- Campo privado `List<Libros> _listaLibros`, inicializado en cada constructor para evitar `NullReferenceException`.
- Propiedad pública `ListaLibros` de solo lectura (`get`), de modo que la lista no se puede reemplazar desde fuera.
- Constructor parametrizado que recibe la colección capturada en el `DataGridView`; `EjecutarGuardar()` empaqueta los datos del formulario junto con la colección.
- Sin expresiones lambda ni predicados: solo ciclos `foreach` / `for` y condicionales `if`.

### Las 7 funciones de la colección (`Editorial.cs`)

| # | Función | Método | Qué hace |
|---|---------|--------|----------|
| 1 | Registrar elemento | `RegistrarLibro(Libros libro)` | Inserta al final de la lista con `Add`. |
| 2 | Contar elementos | `ContarLibros()` | Regresa la cantidad con `Count`. |
| 3 | Buscar por ID | `BuscarLibroPorId(string id)` | Recorre con `foreach` y regresa el objeto, o `null` si no existe. |
| 4 | Validar existencia | `ValidarExistencia(string id)` | Regresa `true` si el ID ya está en la lista (evita duplicados). |
| 5 | Eliminar por ID | `EliminarLibroPorId(string id)` | Busca la posición y extrae el elemento con `RemoveAt`. |
| 6 | Filtrar por estado | `FiltrarLibrosActivos()` | Regresa una nueva `List<Libros>` solo con los libros activos. |
| 7 | Vaciar lista | `VaciarLibros()` | Limpia la colección con `Clear()`; el contador vuelve a cero. |

### Interfaz del detalle (`FrmEditorial`)

- `ComboBox` con los libros registrados y botón **Agregar Detalle**.
- `DataGridView` que muestra en tiempo real la lista interna del modelo, enlazado con un `BindingSource`.
- Botones **Quitar Detalle**, **Buscar Libro**, **Filtrar activos / Ver todos** y **Vaciar Lista**.
- Etiqueta contador (`Libros: N`) que se actualiza con cada operación.

## Cómo ejecutarlo

Requisitos: Windows y el SDK de .NET 10.

1. Clona el repositorio:
   ```bash
   git clone <URL-DEL-REPOSITORIO>
   cd <carpeta-del-repositorio>
   ```
2. Abre `Models.slnx` en Visual Studio.
3. Compila y ejecuta con `F5`, o desde la terminal:
   ```bash
   dotnet run --project Models/Biblioteca.csproj
   ```

## Uso básico

1. En la pantalla principal elige el módulo (Libros, Editorial, etc.) con los botones superiores.
2. Captura los datos y usa **Guardar**, **Actualizar**, **Eliminar** o el campo **ID a buscar**.
3. En **Editorial**, agrega libros al detalle desde el `ComboBox` y guarda la editorial para empaquetar la colección.

## Estado del proyecto

Proyecto académico en desarrollo. Los datos viven solo en memoria y se pierden al cerrar la aplicación.
