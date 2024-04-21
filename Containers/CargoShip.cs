using ConsoleApp1.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CargoShip
{
    public List<Container> Containers { get; private set; }
    public int MaxSpeed { get; private set; }
    public int MaxContainerCount { get; private set; }
    public double MaxWeight { get; private set; }

        public CargoShip(int maxSpeed, int maxContainerCount, double maxWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeight = maxWeight;
            Containers = new List<Container>();
        }

        public void LoadContainer(Container container)
        {
            
            AddContainer(container);
        }

        public void UnloadContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null)
            {
                container.UnloadCargo();
                Console.WriteLine($"Container {serialNumber} unloaded.");
                Containers.Remove(container);
            }
            else
            {
                Console.WriteLine("Container not found.");
            }
        }

        public void AddContainer(Container container)
        {
            if (Containers.Count >= MaxContainerCount)
            {
                Console.WriteLine("Cannot add more containers, ship is full.");
                return;
            }

            if (Containers.Sum(c => c.LoadMass + c.OwnWeight) + container.LoadMass + container.OwnWeight > MaxWeight)
            {
                Console.WriteLine("Cannot add container, it would exceed the ship's max weight.");
                return;
            }

            Containers.Add(container);
            Console.WriteLine($"Container {container.SerialNumber} added.");
        }

        public void RemoveContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null)
            {
                Containers.Remove(container);
                Console.WriteLine($"Container {serialNumber} removed.");
            }
            else
            {
                Console.WriteLine("Container not found.");
            }
        }

        public void ReplaceContainer(string containerNumber, Container newContainer)
        {
            int index = Containers.FindIndex(c => c.SerialNumber == containerNumber);
            if (index != -1)
            {
                Containers[index] = newContainer;
            }
            else
            {
                Console.WriteLine("Container not found.");
            }
        }

    public void TransferContainer(string containerNumber, CargoShip targetShip)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == containerNumber);
        if (container != null)
        {
            Containers.Remove(container);
            targetShip.AddContainer(container);
        }
        else
        {
            Console.WriteLine("Container not found.");
        }
    }

    public void ListContainers()
    {
            foreach (var container in Containers)
            {
                double totalMass;
                if (container is LiquidContainer liquidContainer)
                {
                    totalMass = liquidContainer.CargoWeight + liquidContainer.OwnWeight;
                }
                else if (container is GasContainer gasContainer)
                {
                    totalMass = gasContainer.CargoWeight + gasContainer.OwnWeight;
                }
                else if (container is RefrigeratedContainer refrigeratedContainer)
                {
                    totalMass = refrigeratedContainer.CargoWeight + refrigeratedContainer.OwnWeight;
                }
                else
                {
                    totalMass = container.TotalWeight;
                }

                Console.WriteLine($"Container Serial: {container.SerialNumber}, Total Mass: {totalMass}, Type: {container.GetType().Name}");
            }
        }
}


}
