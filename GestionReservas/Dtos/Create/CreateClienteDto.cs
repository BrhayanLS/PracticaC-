using System.ComponentModel.DataAnnotations;

namespace GestionReservas.Dtos.Create
{
    public class CreateClienteDto
    {
        [Required(ErrorMessage = "Tiene que especificar el nombre")]
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El email no es correcto")]
        public string correo { get; set; }
        public string dni { get; set; }
    }
}