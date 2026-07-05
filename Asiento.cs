namespace Auditorio_Congreso
{
    // Representa cada uno de los 100 asientos del auditorio.
    public class Asiento
    {
        public int Numero { get; set; }
        public bool Ocupado { get; set; }
        public Asistente AsistenteAsignado { get; set; }

        public Asiento(int numero)
        {
            Numero = numero;
            Ocupado = false;
            AsistenteAsignado = null;
        }
    }
}