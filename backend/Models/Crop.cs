using System;
using System.Collections.Generic;

namespace backendAg.Models
{
    public partial class Crop
    {
        public int CropId { get; set; }
        public string CropName { get; set; } = null!;
        public string CropType { get; set; } = null!;
        public string SeedTime { get; set; } = null!;
        public int FarmerId { get; set; }

        public virtual Farmer Farmer { get; set; } = null!;
    }
}
