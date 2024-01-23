using System.ComponentModel.DataAnnotations;

namespace GestionReservas.Dtos.Create
{
    public class CreateHabitacionDto
    {
        [Required(ErrorMessage = "Tiene que especificar el nombre de la habitacion")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El nombre debe contener solo números.")]
        public string NombreHabitacion { get; set; }

        [Required(ErrorMessage = "Tiene que especificar la capaciad de la habitacion")]
        public int Capacidad { get; set; }
        public int IdTipoHabitacion { get; set; }
    }
}