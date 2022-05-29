using System;
using System.Collections.Generic;

namespace backendAg.Models
{
    public partial class Farmer
    {
        public Farmer()
        {
            Crops = new HashSet<Crop>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; } 

        public virtual ICollection<Crop> Crops { get; set; }
    }
}
