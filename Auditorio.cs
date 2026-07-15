using System;
using System.Collections.Generic;

namespace Auditorio_Congreso
{
    // Administra los 100 asientos, la cola general de asignación
    // (orden real de llegada) y una PILA (LIFO) para reasignar
    // los asientos que se liberan/cancelan.
    public class Auditorio
    {
        private const int TOTAL_ASIENTOS = 100;
        private Asiento[] asientos;
        private Queue<Asistente> colaAsignacion;       // Cola: orden de llegada
        private Stack<int> pilaAsientosLiberados;       // Pila: últimos liberados primero

        public Auditorio()
        {
            asientos = new Asiento[TOTAL_ASIENTOS];
            for (int i = 0; i < TOTAL_ASIENTOS; i++)
                asientos[i] = new Asiento(i + 1);

            colaAsignacion = new Queue<Asistente>();
            pilaAsientosLiberados = new Stack<int>();
        }

        public void AgregarAColaAsignacion(Asistente a)
        {
            colaAsignacion.Enqueue(a);
        }

        // Asigna asientos siguiendo estrictamente el orden FIFO
        // en que fueron atendidos en las ventanillas.
        public void AsignarAsientos()
        {
            while (colaAsignacion.Count > 0)
            {
                Asistente actual = colaAsignacion.Dequeue();
                int numeroAsiento;

                if (pilaAsientosLiberados.Count > 0)
                    numeroAsiento = pilaAsientosLiberados.Pop();
                else
                    numeroAsiento = ObtenerSiguienteAsientoLibre();

                if (numeroAsiento == -1)
                {
                    Console.WriteLine($"AUDITORIO LLENO. No se pudo asignar asiento a {actual.Nombre}");
                    continue;
                }

                Asiento asiento = asientos[numeroAsiento - 1];
                asiento.Ocupado = true;
                asiento.AsistenteAsignado = actual;

                Console.WriteLine($"Asiento #{numeroAsiento:D3} asignado a {actual.Nombre} (Ventanilla {actual.Ventanilla})");
            }
        }

        private int ObtenerSiguienteAsientoLibre()
        {
            foreach (var asiento in asientos)
                if (!asiento.Ocupado) return asiento.Numero;
            return -1;
        }

        public void LiberarAsiento(int numero)
        {
            if (numero < 1 || numero > TOTAL_ASIENTOS)
            {
                Console.WriteLine("Número de asiento inválido.");
                return;
            }

            Asiento asiento = asientos[numero - 1];
            if (asiento.Ocupado)
            {
                asiento.Ocupado = false;
                asiento.AsistenteAsignado = null;
                pilaAsientosLiberados.Push(numero);
                Console.WriteLine($"Asiento #{numero} liberado y apilado para reasignación (LIFO).");
            }
            else
            {
                Console.WriteLine("El asiento ya estaba libre.");
            }
        }

        // ================= REPORTERÍA =================

        public void MostrarTodosLosAsientos()
        {
            Console.WriteLine("\n=== MAPA DE ASIENTOS DEL AUDITORIO ===");
            for (int i = 0; i < TOTAL_ASIENTOS; i++)
            {
                var a = asientos[i];
                string estado = a.Ocupado ? $"OCUPADO ({a.AsistenteAsignado.Nombre})" : "LIBRE";
                Console.WriteLine($"Asiento {a.Numero:D3}: {estado}");
                if ((i + 1) % 10 == 0) Console.WriteLine("----------------------------------------");
            }
        }

        public void ConsultarAsientoPorNumero(int numero)
        {
            if (numero < 1 || numero > TOTAL_ASIENTOS)
            {
                Console.WriteLine("Número de asiento fuera de rango.");
                return;
            }
            var a = asientos[numero - 1];
            Console.WriteLine(a.Ocupado
                ? $"Asiento {numero}: OCUPADO por {a.AsistenteAsignado}"
                : $"Asiento {numero}: LIBRE");
        }

        public void ConsultarAsientoPorAsistente(string cedula)
        {
            foreach (var a in asientos)
            {
                if (a.Ocupado && a.AsistenteAsignado.Cedula == cedula)
                {
                    Console.WriteLine($"El asistente con cédula {cedula} tiene asignado el asiento #{a.Numero}");
                    return;
                }
            }
            Console.WriteLine("No se encontró un asiento asignado para esa cédula.");
        }

        public void MostrarEstadisticas()
        {
            int ocupados = 0;
            foreach (var a in asientos) if (a.Ocupado) ocupados++;

            Console.WriteLine("\n=== ESTADÍSTICAS DEL AUDITORIO ===");
            Console.WriteLine($"Asientos ocupados: {ocupados}/{TOTAL_ASIENTOS}");
            Console.WriteLine($"Asientos libres:   {TOTAL_ASIENTOS - ocupados}/{TOTAL_ASIENTOS}");
            Console.WriteLine($"Asientos en pila de reasignación: {pilaAsientosLiberados.Count}");
        }

        public void MostrarPilaLiberados()
        {
            Console.WriteLine("\n--- PILA DE ASIENTOS LIBERADOS (LIFO) ---");
            if (pilaAsientosLiberados.Count == 0)
            {
                Console.WriteLine("(vacía)");
                return;
            }
            foreach (var numero in pilaAsientosLiberados)
                Console.WriteLine($"Asiento #{numero}");
        }
    }
}