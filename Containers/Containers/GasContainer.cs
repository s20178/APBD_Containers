using ConsoleApp1.Exceptions;
using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Containers
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; }

        public GasContainer(double height, double ownWeight, double depth, double capacity, double pressure)
            : base(height, ownWeight, depth, capacity, 0)
        {
            Pressure = pressure;
        }

        public override void LoadCargo(double cargoWeight)
        {
            if (cargoWeight > Capacity)
            {
                throw new OverfillException("Cargo weight exceeds container capacity.");
            }

            base.LoadCargo(cargoWeight);
        }

        public void EmptyContainer()
        {
            double remainingLoad = LoadMass * 0.05;
            LoadMass = remainingLoad;
        }

        public void SendNotification(string message, string containerNumber)
        {
            Console.WriteLine($"Attention, fire hazard. Container: {containerNumber}");
        }
    }

}
