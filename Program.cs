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

            // Datos simulados iniciales (se pueden borrar si no se necesitan)
            GenerarDatosSimulados(20);

            while (!salir)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": RegistrarAsistenteManual(); break;
                    case "2": ProcesarColasYAsignar(); break;
                    case "3": auditorio.MostrarTodosLosAsientos(); break;
                    case "4": ConsultarAsientoMenu(); break;
                    case "5": lineaIngreso.MostrarColas(); break;
                    case "6": auditorio.MostrarEstadisticas(); break;
                    case "7": LiberarAsientoMenu(); break;
                    case "8": auditorio.MostrarPilaLiberados(); break;
                    case "0": salir = true; break;
                    default: Console.WriteLine("Opción no válida."); break;
                }
            }
        }
