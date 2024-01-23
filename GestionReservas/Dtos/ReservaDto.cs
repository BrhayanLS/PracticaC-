namespace GestionReservas.Dtos
{
    public class ReservaDto : CreateReservaDto
    {
        public int IdReserva { get; set; }
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int IdCliente { get; set; }
        public int IdHabitacion { get; set; }
    }
}
