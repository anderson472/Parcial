using Parcial.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Parcial.Shared.Entities
{
    public class EventControl
    {
        [Key]
        public int Id_Boleta { get; set; }

        [Display(Name = "Fecha de Uso")]
        public DateTime? Fecha_De_Uso { get; set; }

        [Display(Name = "Es Usada")]
        public bool? Fue_Usada { get; set; }

        [Display(Name = "Porteria")]
        public string? Porteria { get; set; }

        [NotMapped]
        public GatesType TipoPorteria { get; set; }




    }
}
