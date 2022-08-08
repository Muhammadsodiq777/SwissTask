using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwissTask.Data
{
    public class Car
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }

        public double FuelTankVolume { get; set; }

        public double Speed { get; set; }
    }
}
