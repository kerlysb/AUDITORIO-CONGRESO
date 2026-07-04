using System;

namespace Auditorio_Congreso
{
    // Representa a cada persona que ingresa por una de las dos
    // ventanillas de registro del congreso.
    public class Asistente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public int Ventanilla { get; set; } // 1 o 2
        public DateTime HoraRegistro { get; set; }

        public Asistente(int id, string nombre, string cedula, int ventanilla)
        {
            Id = id;
            Nombre = nombre;
            Cedula = cedula;
            Ventanilla = ventanilla;
            HoraRegistro = DateTime.Now;
        }

        public override string ToString()
        {
            return $"ID:{Id} | {Nombre} | Cédula:{Cedula} | Ventanilla:{Ventanilla} | Hora:{HoraRegistro:HH:mm:ss}";
        }
    }
}