using System;
using System.Collections.Generic;

namespace GuiaPractica02_PilasColas
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
