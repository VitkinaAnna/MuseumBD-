using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuseunBD
{
    public partial class Exhibition
    {
        public Exhibition()
        {
            Tickets = new HashSet<Ticket>();
        }
        public int ExhibitionId { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва екскурсії")]
        public string Name { get; set; } = null!;
        public int WorkerId { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ціна")]
        public string Price { get; set; } = null!;
        [Display(Name = "Відповідальний")]

        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
