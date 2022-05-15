using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuseunBD
{
    public partial class Position
    {
        public Position()
        {
            Workers = new HashSet<Worker>();
        }

        public int PositionId { get; set; }
        [Required(ErrorMessage = "Поле не повинне бути порожнім")]
        [Display(Name = "Назва посади")]
        public string Name { get; set; } = null!;

        public virtual ICollection<Worker> Workers { get; set; }
    }
}
