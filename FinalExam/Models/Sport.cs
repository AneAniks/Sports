using System;
using System.Collections.Generic;

namespace FinalExam.Models
{
    public partial class Sport
    {
        public Sport()
        {
            Players = new HashSet<Players>();
        }

        public int SportId { get; set; }
        public string SportName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Players> Players { get; set; }
    }
}
