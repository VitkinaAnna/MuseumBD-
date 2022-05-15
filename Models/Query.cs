using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MuseunBD
{
    public partial class Query
    {
        public string QueryName { get; set; }
        public string ErrorName { get; set; }
        public int ErrorFlag { get; set; }

        public int Quantity { get; set; }

        public string DinosaurName { get; set; }
        public string ExhibitionPrice { get; set; }
        public string HallName { get; set; }
        public string ExhibitionName { get; set; }
        public string WorkerName { get; set; }
        public string PositionName { get; set; }
        public string TicketName { get; set; }
        public string DinosaurLifetime { get; set; }
        public int HallId { get; set; }

        public List<string> DinosaurNames { get; set; }
        public List<string> HallNames { get; set; }
        public List<string> WorkerNames { get; set; }
        public List<string> ExhibitionPrices { get; set; }
        public List<string> ExhibitionNames { get; set; }
        public List<string> PositionNames { get; set; }
        public List<string> TicketNames { get; set; }
        public List<string> HallIds { get; set; }
        public List<string> DinosaurLifetimes { get; set; }

    }
}