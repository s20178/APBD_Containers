
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Exceptions;

namespace ConsoleApp1.Containers
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; }

        public LiquidContainer(double height, double ownWeight, double depth, double capacity, bool isHazardous)
            : base(height, ownWeight, depth, capacity, 0)
        {
            IsHazardous = isHazardous;
        }

        public override void LoadCargo(double cargoWeight)
        {
            double maxLoadPercentage = IsHazardous ? 0.5 : 0.9;
            double maxLoad = Capacity * maxLoadPercentage;

            if (cargoWeight > maxLoad)
            {
                throw new OverfillException("Cargo weight exceeds container capacity.");
            }

            base.LoadCargo(cargoWeight);
        }

        public void SendNotification(string message, string containerNumber)
        {
            Console.WriteLine($"Attention, liquid hazard {containerNumber}: {message}");
        }
    }


}
