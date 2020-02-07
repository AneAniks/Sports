using System;
using System.Collections.Generic;

namespace FinalExam.Models
{
    public partial class Players
    {
        public int PlayerId { get; set; }
        public string FullName { get; set; }
        public int? Age { get; set; }
        public string Contry { get; set; }
        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
