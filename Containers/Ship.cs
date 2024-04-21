using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Containers;
using Container = ConsoleApp1.Containers.Container;

namespace ConsoleApp1
{
    public class Ship
    {
        public List<Container> Containers { get; } = new List<Container>();
        public int MaxSpeed { get; }
        public int MaxContainerCount { get; }
        public double MaxCargoWeight { get; }
        public double CurrentCargoWeight => Containers.Sum(c => c.LoadMass + c.OwnWeight);

        public Ship(int maxSpeed, int maxContainerCount, double maxCargoWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxCargoWeight = maxCargoWeight;
        }

        public void LoadContainer(Container container, double cargoWeight)
        {
            if (Containers.Count >= MaxContainerCount)
            {
                throw new InvalidOperationException("Ship cannot carry more containers.");
            }

            if (CurrentCargoWeight + cargoWeight +  container.LoadMass + container.OwnWeight > MaxCargoWeight)
            {
                throw new InvalidOperationException("Ship cannot carry more weight.");
            }

            container.LoadCargo(cargoWeight);
            Containers.Add(container);
        }

        public void UnloadContainer(Container container)
        {
            Containers.Remove(container);
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
                Containers.Add(newContainer);
            }
        }

        public void TransferContainer(Container container, CargoShip targetShip)
        {
            if (Containers.Contains(container))
            {
                UnloadContainer(container);
                targetShip.LoadContainer(container);
            }
            else
            {
                throw new InvalidOperationException("Container not found on this ship.");
            }
        }
    }
}
