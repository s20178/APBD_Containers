using ConsoleApp1.Containers;
using ConsoleApp1.Exceptions;
using ConsoleApp1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static Dictionary<string, double> products = new Dictionary<string, double>
    {
        { "Meat", -15 },
        { "Bananas", 13.3 },
        { "Frozen pizza", -30 },
        { "Butter", 20.5 }
    };
        public static void Main(string[] args)
        {
            CargoShip alMazrah = new CargoShip(30, 100, 5000);
            CargoShip terreShip = new CargoShip(25, 110, 6000);

            Console.WriteLine("Preparing a container ship for a voyage:");
            DisplayShipInformation(alMazrah);
            alMazrah.ListContainers();

            LiquidContainer liquidContainer = new LiquidContainer(100, 100, 20, 500, true);
            LiquidContainer secondLiquidContainer = new LiquidContainer(150, 120, 25, 550, false);
            GasContainer gasContainer = new GasContainer(100, 120, 20, 500, 2);
            GasContainer secondGasContainer = new GasContainer(120, 90, 25, 400, 1.5);
            RefrigeratedContainer refrigeratedContainer = new RefrigeratedContainer(100, 200, 30, 500, "Meat", products["Meat"]);
            RefrigeratedContainer secondRefrigeratedContainer = new RefrigeratedContainer(120, 180, 35, 550, "Bananas", products["Bananas"]);


            try
            {
                liquidContainer.LoadCargo(350);
                gasContainer.LoadCargo(200);
                refrigeratedContainer.LoadCargo(400);
                secondLiquidContainer.LoadCargo(300);
                secondGasContainer.LoadCargo(250);
                secondRefrigeratedContainer.LoadCargo(500);
            }
            catch (OverfillException ex)
            {
                Console.WriteLine($"OverfillException: {ex.Message}");

                // Exception: replacing smaller container with bigger one
                LiquidContainer biggerLiquidContainer = new LiquidContainer(200, 100, 150, 750, true);
                alMazrah.ReplaceContainer(liquidContainer.SerialNumber, biggerLiquidContainer);

                Console.WriteLine("The container has been replaced by a larger container.");
            }

            alMazrah.LoadContainer(liquidContainer);
            alMazrah.LoadContainer(gasContainer);
            alMazrah.LoadContainer(refrigeratedContainer);
            alMazrah.LoadContainer(secondLiquidContainer);
            alMazrah.LoadContainer(secondGasContainer);
            alMazrah.LoadContainer(secondRefrigeratedContainer);

            Console.WriteLine("\nCurrent Status of the Ship:");
            alMazrah.ListContainers();

            alMazrah.UnloadContainer(liquidContainer.SerialNumber);

            Console.WriteLine("\nState after container removal:");
            alMazrah.ListContainers();

            LiquidContainer newLiquidContainer = new LiquidContainer(200, 100, 150, 500, false);
            alMazrah.ReplaceContainer("KON-C-5", newLiquidContainer);

            Console.WriteLine("\nState after container replacement:");
            DisplayShipInformation(alMazrah);


            terreShip.LoadContainer(refrigeratedContainer);
            alMazrah.TransferContainer(newLiquidContainer.SerialNumber, terreShip);

            Console.WriteLine("\nContainers on the alMazrah ship:");
            alMazrah.ListContainers();
            Console.WriteLine("\nContainers on the terreShip:");
            terreShip.ListContainers();
            Console.ReadLine();
        }

        public static void DisplayShipInformation(CargoShip ship)
        {
            Console.WriteLine($"Max ship speed: {ship.MaxSpeed} nodes");
            Console.WriteLine($"Max container capacity: {ship.MaxContainerCount}");
            Console.WriteLine($"Max total weight: {ship.MaxWeight} tons");

        }
    }

}

