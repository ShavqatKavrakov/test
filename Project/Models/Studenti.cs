using System;
using System.Collections.Generic;

namespace Project.Models
{
    public partial class Studenti
    {
        public int Id { get; set; }
        public string StudName { get; set; } = null!;
        public int Kurs { get; set; }
        public int SpecId { get; set; }
    }
}
