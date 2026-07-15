using System;

namespace Auditorio_Congreso
{
    class Program
    {
        static Auditorio auditorio = new Auditorio();
        static LineaIngreso lineaIngreso = new LineaIngreso();
        static int contadorId = 1;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool salir = false;

            GenerarDatosSimulados(20);

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1": RegistrarAsistenteManual(); break;
                    case "2": ProcesarColasYAsignar(); break;
                    case "3": auditorio.MostrarTodosLosAsientos(); Pausa(); break;
                    case "4": ConsultarAsientoMenu(); break;
                    case "5": lineaIngreso.MostrarColas(); Pausa(); break;
                    case "6": auditorio.MostrarEstadisticas(); Pausa(); break;
                    case "7": LiberarAsientoMenu(); break;
                    case "8": auditorio.MostrarPilaLiberados(); Pausa(); break;
                    case "0": salir = true; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n===== SISTEMA DE ASIGNACIÓN DE ASIENTOS - CONGRESO =====");
            Console.WriteLine("1. Registrar nuevo asistente (ventanilla 1 o 2)");
            Console.WriteLine("2. Procesar colas y asignar asientos");
            Console.WriteLine("3. Ver mapa completo de asientos");
            Console.WriteLine("4. Consultar asiento (por número o cédula)");
            Console.WriteLine("5. Ver colas de ventanillas (reportería)");
            Console.WriteLine("6. Ver estadísticas");
            Console.WriteLine("7. Liberar un asiento");
            Console.WriteLine("8. Ver pila de asientos liberados");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static void RegistrarAsistenteManual()
        {
            Console.Write("Nombre del asistente: ");
            string nombre = Console.ReadLine() ?? "Sin nombre";
            
            Console.Write("Cédula: ");
            string cedula = Console.ReadLine() ?? "0";

            int ventanilla;
            Console.Write("Ventanilla (1 o 2): ");
            // Validación para evitar cierres inesperados si no es un número
            while (!int.TryParse(Console.ReadLine(), out ventanilla) || (ventanilla != 1 && ventanilla != 2))
            {
                Console.WriteLine("Entrada inválida. Ingrese 1 o 2.");
                Console.Write("Ventanilla (1 o 2): ");
            }

            Asistente a = new Asistente(contadorId++, nombre, cedula, ventanilla);
            lineaIngreso.RegistrarAsistente(a);
            Console.WriteLine($"{nombre} registrado en ventanilla {ventanilla}.");
        }

        static void Pausa()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        // Simula el ingreso por fila doble: alterna la atención entre
        // ventanilla 1 y ventanilla 2, respetando el orden real de llegada.
        static void ProcesarColasYAsignar()
        {
            while (lineaIngreso.HayAsistentesEnVentanilla1 || lineaIngreso.HayAsistentesEnVentanilla2)
            {
                if (lineaIngreso.HayAsistentesEnVentanilla1)
                    auditorio.AgregarAColaAsignacion(lineaIngreso.AtenderVentanilla1());

                if (lineaIngreso.HayAsistentesEnVentanilla2)
                    auditorio.AgregarAColaAsignacion(lineaIngreso.AtenderVentanilla2());
            }
            auditorio.AsignarAsientos();
        }

        static void ConsultarAsientoMenu()
        {
            Console.WriteLine("1. Buscar por número de asiento");
            Console.WriteLine("2. Buscar por cédula de asistente");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("Número de asiento: ");
                int num = int.Parse(Console.ReadLine());
                auditorio.ConsultarAsientoPorNumero(num);
            }
            else if (opcion == "2")
            {
                Console.Write("Cédula: ");
                string cedula = Console.ReadLine();
                auditorio.ConsultarAsientoPorAsistente(cedula);
            }
        }

        static void LiberarAsientoMenu()
        {
            Console.Write("Número de asiento a liberar: ");
            int num = int.Parse(Console.ReadLine());
            auditorio.LiberarAsiento(num);
        }

        static void GenerarDatosSimulados(int cantidad)
        {
            string[] nombres = { "Ana Torres", "Luis Pérez", "María Gómez", "Carlos Ruiz", "Sofía León",
                                  "Diego Vega", "Lucía Ramos", "Pedro Salas", "Elena Cruz", "Iván Rojas" };
            Random rnd = new Random();
            for (int i = 0; i < cantidad; i++)
            {
                string nombre = nombres[rnd.Next(nombres.Length)] + " " + (i + 1);
                string cedula = "17" + rnd.Next(10000000, 99999999);
                int ventanilla = (i % 2) + 1; // alterna 1 y 2, simulando la fila doble
                Asistente a = new Asistente(contadorId++, nombre, cedula, ventanilla);
                lineaIngreso.RegistrarAsistente(a);
            }
            Console.WriteLine($"{cantidad} asistentes simulados generados en las dos ventanillas.");
        }
    }
}