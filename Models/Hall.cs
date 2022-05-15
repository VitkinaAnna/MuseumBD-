using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuseunBD
{
    public partial class Hall
    {
        public Hall()
        {
            Dinosaurs = new HashSet<Dinosaur>();
        }

        public int HallId { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва зали")]
        public string Name { get; set; } = null!;
        public int WorkerId { get; set; }
        [Display(Name = "Відповідальний")]

        public virtual Worker Worker { get; set; } = null!;
        public virtual ICollection<Dinosaur> Dinosaurs { get; set; }
    }
}
