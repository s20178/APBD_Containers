using ConsoleApp1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Containers
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; }
        public double RequiredTemperature { get; }
        public double Temperature { get; private set; }

        public RefrigeratedContainer(double height, double ownWeight, double depth, double capacity, string productType, double requiredTemperature)
            : base(height, ownWeight, depth, capacity, 0)
        {
            ProductType = productType;
            RequiredTemperature = requiredTemperature;
            Temperature = requiredTemperature;
        }

        public override void LoadCargo(double cargoWeight)
        {

            if (Temperature < RequiredTemperature)
            {
                throw new InvalidOperationException($"Cannot load cargo. Container temperature ({Temperature}°C) is lower than required ({RequiredTemperature}°C) for {ProductType}.");
            }

            base.LoadCargo(cargoWeight);
        }
    }

}
