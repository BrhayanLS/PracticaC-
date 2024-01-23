namespace GestionReservas.Dtos
{
    public class CreateReservaDto
    {
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdCliente { get; set; }
        public int IdHabitacion { get; set; }
    }
}