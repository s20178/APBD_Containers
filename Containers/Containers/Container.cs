using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Containers
{
    public class Container
    {
        private static int counter = 1;

        public string SerialNumber { get; }
        public double LoadMass { get; protected set; }
        public double OwnWeight { get; }
        public double Height { get; }
        public double Depth { get; }
        public double Capacity { get; }
        public double CargoWeight { get; }

 

        public Container(double height, double ownWeight, double depth, double capacity, double cargoWeight)
        {
            SerialNumber = GenerateSerialNumber();
            Height = height;
            OwnWeight = ownWeight;
            Depth = depth;
            Capacity = capacity;
            CargoWeight = cargoWeight;

        }

        private string GenerateSerialNumber()
        {
            string prefix;
            if (GetType() == typeof(RefrigeratedContainer))
            {
                prefix = "C";
            }
            else
            {
                prefix = GetType().Name.Substring(0, 1);
            }

            return $"KON-{prefix}-{counter++}";
        }

        public virtual void LoadCargo(double cargoWeight)
        {
            LoadMass = cargoWeight;
        }

        public void UnloadCargo()
        {
            LoadMass = 0;
        }

        public double TotalWeight => LoadMass + OwnWeight;

        public bool ExceedsMaxWeight(double maxWeight)
        {
            return TotalWeight > maxWeight;
        }
    }



}
