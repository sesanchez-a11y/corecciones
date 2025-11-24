using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using TutoriasDeClases.Modelos;
using TutoriasDeClases.Interfaces;
using TutoriasDeClases.Strategies;
using TutoriasDeClases.Observers;
using TutoriasDeClases.Factories;

namespace TutoriasDeClases
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Configuración de MongoDB
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("EduMentor");
            var serviciosCollection = database.GetCollection<Servicio>("Servicios");
            var reservasCollection = database.GetCollection<Reserva>("Reservas");

            Console.WriteLine("Conectado a MongoDB.");

            // Insertar servicios de prueba (opcional, solo la primera vez)
            // await serviciosCollection.DeleteManyAsync(_ => true); // Para borrar todo y empezar de nuevo
            var servicio1 = new Servicio { Titulo = "Matemáticas Básicas", PrecioBase = 15.0 };
            var servicio2 = new Servicio { Titulo = "Programación en C#", PrecioBase = 25.0 };
            var servicio3 = new Servicio { Titulo = "Programación Orientada a Objetos", PrecioBase = 30.0 };
            var servicio4 = new Servicio { Titulo = "Inglés Avanzado", PrecioBase = 20.0 };

            await serviciosCollection.InsertOneAsync(servicio1);
            await serviciosCollection.InsertOneAsync(servicio2);
            await serviciosCollection.InsertOneAsync(servicio3);
            await serviciosCollection.InsertOneAsync(servicio4);

            Console.WriteLine("Servicios de prueba creados.");

            // Menú principal
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n=== MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Explorar Catálogo de Servicios");
                Console.WriteLine("2. Buscar Servicio por Especialidad");
                Console.WriteLine("3. Crear Reserva");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        await MostrarCatalogo(serviciosCollection);
                        break;
                    case "2":
                        await BuscarServicioPorEspecialidad(serviciosCollection);
                        break;
                    case "3":
                        await CrearReserva(serviciosCollection, reservasCollection);
                        break;
                    case "4":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }

            Console.WriteLine("\nGracias por usar EduMentor. ¡Hasta luego!");
        }

        // --- Métodos estáticos ---

        static async Task MostrarCatalogo(IMongoCollection<Servicio> serviciosCollection)
        {
            Console.WriteLine("\n=== CATÁLOGO DE SERVICIOS ===");
            var servicios = await serviciosCollection.Find(_ => true).ToListAsync();
            if (servicios.Count == 0)
            {
                Console.WriteLine("No hay servicios disponibles.");
                return;
            }

            foreach (var servicio in servicios)
            {
                Console.WriteLine($"ID: {servicio.Id} | Título: {servicio.Titulo} | Precio Base: ${servicio.PrecioBase:F2}");
            }
        }

        static async Task BuscarServicioPorEspecialidad(IMongoCollection<Servicio> serviciosCollection)
        {
            Console.Write("\nIngrese la especialidad a buscar (ej: 'Programación'): ");
            string especialidad = Console.ReadLine();

            Console.WriteLine($"\n=== RESULTADOS PARA '{especialidad}' ===");
            var filtro = Builders<Servicio>.Filter.Regex(s => s.Titulo, $".*{especialidad}.*");
            var servicios = await serviciosCollection.Find(filtro).ToListAsync();

            if (servicios.Count == 0)
            {
                Console.WriteLine("No se encontraron servicios con esa especialidad.");
                return;
            }

            foreach (var servicio in servicios)
            {
                Console.WriteLine($"ID: {servicio.Id} | Título: {servicio.Titulo} | Precio Base: ${servicio.PrecioBase:F2}");
            }
        }

        static async Task CrearReserva(IMongoCollection<Servicio> serviciosCollection, IMongoCollection<Reserva> reservasCollection)
        {
            Console.WriteLine("\n=== CREAR RESERVA ===");

            // Paso 1: Buscar y seleccionar un servicio
            Console.Write("Ingrese el ID del servicio que desea reservar: ");
            string servicioId = Console.ReadLine();

            var servicio = await serviciosCollection.Find(s => s.Id == servicioId).FirstOrDefaultAsync();
            if (servicio == null)
            {
                Console.WriteLine("Servicio no encontrado.");
                return;
            }

            Console.WriteLine($"\nHa seleccionado el servicio: {servicio.Titulo} - Precio Base: ${servicio.PrecioBase:F2}");

            // Paso 2: Ingresar datos del alumno
            Console.Write("Ingrese su ID de alumno: ");
            string alumnoId = Console.ReadLine();

            // Paso 3: Aplicar estrategia de precio
            IPrecioStrategy estrategia = new PrecioConDescuento(); // O puedes permitir elegir estrategia
            double total = estrategia.Calcular(servicio.PrecioBase);

            // Paso 4: Crear la reserva usando la fábrica
            var reserva = ReservaFactory.CrearReservaIndividual(servicio, alumnoId, total);

            // Paso 5: Agregar observadores de notificación
            reserva.AgregarObservador(new EmailNotificacion());
            reserva.AgregarObservador(new SmsNotificacion());

            // Paso 6: Confirmar la reserva (esto dispara las notificaciones)
            reserva.Confirmar();

            // Paso 7: Guardar la reserva en la base de datos
            await reservasCollection.InsertOneAsync(reserva);

            Console.WriteLine($"\n¡Reserva creada exitosamente! Total: ${total:F2}");
            Console.WriteLine($"ID de la reserva: {reserva.Id}");
        }
    }
}