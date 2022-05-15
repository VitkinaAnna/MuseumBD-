using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuseunBD
{
    public partial class Worker
    {
        public Worker()
        {
            Exhibitions = new HashSet<Exhibition>();
            Halls = new HashSet<Hall>();
        }

        public int WorkerId { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Ім`я")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Зарплата")]
        public string Salary { get; set; } = null!;

        public int PositionId { get; set; }
        [Display(Name = "Посада")]

        public virtual Position Position { get; set; } = null!;
        public virtual ICollection<Exhibition> Exhibitions { get; set; }
        public virtual ICollection<Hall> Halls { get; set; }
    }
}
