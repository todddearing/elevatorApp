using System;
using ElevatorApp.Models;
using ElevatorApp.Services;
using ElevatorApp.UI;

namespace ElevatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Welcome to the Elevator Simulation App");
            Console.WriteLine("========================================\n");

            // Create the building and elevator system
            var building = new Building(4);
            var elevatorController = new ElevatorController(building);
            var ui = new ElevatorUI(building, elevatorController);

            // Run the UI
            ui.Run();
        }
    }
}