using Parcial.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcial.Shared.Entities
{
    public class EventControl
    {
        
        public int Id { get; set; }

        [Display(Name = "Fecha_de_Uso")]
        public DateTime? Fecha_De_Uso { get; set; }

        [Display(Name = "Es_Usada")]
        public bool Fue_Usada { get; set; }

        [Display(Name = "Porteria")]
        public string? Porteria { get; set; }

        [NotMapped]
        public GatesType TipoPorteria { get; set; }




    }
}
