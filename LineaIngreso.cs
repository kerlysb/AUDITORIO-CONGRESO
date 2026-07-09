using System.Collections.Generic;

namespace GuiaPractica02_PilasColas
{
    // Administra las DOS colas (FIFO) que representan la fila
    // doble de ingreso: una cola por cada persona que registra.
    public class LineaIngreso
    {
        private Queue<Asistente> ventanilla1;
        private Queue<Asistente> ventanilla2;

        public LineaIngreso()
        {
            ventanilla1 = new Queue<Asistente>();
            ventanilla2 = new Queue<Asistente>();
        }

        public void RegistrarAsistente(Asistente a)
        {
            if (a.Ventanilla == 1)
                ventanilla1.Enqueue(a);
            else
                ventanilla2.Enqueue(a);
        }

        public bool HayAsistentesEnVentanilla1 => ventanilla1.Count > 0;
        public bool HayAsistentesEnVentanilla2 => ventanilla2.Count > 0;

        public Asistente AtenderVentanilla1() => ventanilla1.Dequeue();
        public Asistente AtenderVentanilla2() => ventanilla2.Dequeue();

        public int CantidadVentanilla1 => ventanilla1.Count;
        public int CantidadVentanilla2 => ventanilla2.Count;

        // ---- REPORTERÍA ----
        public void MostrarColas()
        {
            System.Console.WriteLine("\n--- COLA VENTANILLA 1 (FIFO) ---");
            if (ventanilla1.Count == 0) System.Console.WriteLine("(vacía)");
            foreach (var a in ventanilla1) System.Console.WriteLine(a);

            System.Console.WriteLine("\n--- COLA VENTANILLA 2 (FIFO) ---");
            if (ventanilla2.Count == 0) System.Console.WriteLine("(vacía)");
            foreach (var a in ventanilla2) System.Console.WriteLine(a);
        }
    }
}