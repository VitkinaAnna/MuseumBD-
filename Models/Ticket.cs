using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuseunBD
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ім`я покупця")]
        public string Name { get; set; } = null!;

        public int ExhibitionId { get; set; }
        [Display(Name = "Назва виставки")]
        public virtual Exhibition Exhibition { get; set; } = null!;
    }
}
