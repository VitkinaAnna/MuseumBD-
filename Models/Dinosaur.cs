using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuseunBD
{
    public partial class Dinosaur
    {
        public int DinosaurId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва")]
        public string Name { get; set; } = null!;

        public int HallId { get; set; }

        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Період існування")]
        public string Lifetime { get; set; } = null!;
        [Display(Name = "Назва зали")]

        public virtual Hall Hall { get; set; } = null!;
    }
}
