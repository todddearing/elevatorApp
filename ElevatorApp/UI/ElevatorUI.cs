using System;
using System.Collections.Generic;
using ElevatorApp.Models;
using ElevatorApp.Services;

namespace ElevatorApp.UI
{
    public class ElevatorUI
    {
        private Building _building;
        private ElevatorController _controller;
        private bool _running;

        public ElevatorUI(Building building, ElevatorController controller)
        {
            _building = building;
            _controller = controller;
            _running = true;
        }

        public void Run()
        {
            DisplayBuildingDiagram();
            DisplayElevatorControlPanel();
            DisplayMainMenu();
        }

        private void DisplayBuildingDiagram()
        {
            Console.WriteLine("\n╔═══════════════════════════════╦═════════════════════╗");
            Console.WriteLine("║      BUILDING DIAGRAM         ║   ELEVATOR SHAFT    ║");
            Console.WriteLine("╠═══════════════════════════════╬═════════════════════╣");
            
            for (int floor = _building.TotalFloors; floor >= 1; floor--)
            {
                Floor currentFloor = _building.GetFloor(floor);
                string floorLabel = $"Floor {floor}";
                
                // Buttons on this floor
                string buttons = "";
                if (currentFloor.HasUpButton)
                    buttons += "[UP]  ";
                if (currentFloor.HasDownButton)
                    buttons += "[DOWN]";
                
                string elevatorPosition = _building.Elevator.CurrentFloor == floor ? "[ ]" : "   ";
                
                Console.WriteLine($"║ {floorLabel,-29} ║     {elevatorPosition}      ║");
                Console.WriteLine($"║ {buttons,-29} ║                     ║");
                
                if (floor > 1)
                    Console.WriteLine("╟───────────────────────────────╢─────────────────────╢");
            }
            
            Console.WriteLine("╚═══════════════════════════════╩═════════════════════╝");
        }

        private void DisplayElevatorControlPanel()
        {
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║  ELEVATOR CONTROL PANEL        ║");
            Console.WriteLine("╠════════════════════════════════╣");
            Console.WriteLine("║  [1] Floor 1                   ║");
            Console.WriteLine("║  [2] Floor 2                   ║");
            Console.WriteLine("║  [3] Floor 3                   ║");
            Console.WriteLine("║  [4] Floor 4                   ║");
            Console.WriteLine("╚════════════════════════════════╝");
        }

        private void DisplayMainMenu()
        {
            while (_running)
            {
                Console.WriteLine("\n╔════════════════════════════════╗");
                Console.WriteLine("║       MAIN MENU                ║");
                Console.WriteLine("╠════════════════════════════════╣");
                Console.WriteLine("║  [F] Call elevator from floor  ║");
                Console.WriteLine("║  [E] Select floor in elevator  ║");
                Console.WriteLine("║  [S] Start elevator            ║");
                Console.WriteLine("║  [D] Display building status   ║");
                Console.WriteLine("║  [Q] Quit                      ║");
                Console.WriteLine("╚════════════════════════════════╝");
                
                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine()?.ToUpper() ?? "";

                switch (choice)
                {
                    case "F":
                        HandleFloorCall();
                        break;
                    case "E":
                        HandleElevatorSelection();
                        break;
                    case "S":
                        HandleStartElevator();
                        break;
                    case "D":
                        DisplayBuildingDiagram();
                        break;
                    case "Q":
                        _running = false;
                        Console.WriteLine("\nThank you for using the Elevator Simulation App!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void HandleFloorCall()
        {
            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║  SELECT FLOOR TO CALL FROM    ║");
            Console.WriteLine("╠════════════════════════════════╣");
            
            for (int i = 1; i <= _building.TotalFloors; i++)
            {
                Floor floor = _building.GetFloor(i);
                Console.WriteLine($"║  [{i}] Floor {i,-24} ║");
            }
            
            Console.WriteLine("╚════════════════════════════════╝");
            
            Console.Write("Enter floor number: ");
            if (int.TryParse(Console.ReadLine(), out int floor) && floor >= 1 && floor <= _building.TotalFloors)
            {
                Floor selectedFloor = _building.GetFloor(floor);
                
                if (selectedFloor.HasUpButton || selectedFloor.HasDownButton)
                {
                    string direction = "";
                    
                    if (selectedFloor.HasUpButton && selectedFloor.HasDownButton)
                    {
                        Console.WriteLine("\nWhat direction?");
                        Console.WriteLine("[U] Up");
                        Console.WriteLine("[D] Down");
                        Console.Write("Enter choice: ");
                        direction = Console.ReadLine()?.ToUpper() ?? "";
                    }
                    else if (selectedFloor.HasUpButton)
                    {
                        direction = "U";
                    }
                    else
                    {
                        direction = "D";
                    }
                    
                    _controller.HandleFloorButtonPress(floor, direction == "U" ? "UP" : "DOWN");
                }
                else
                {
                    Console.WriteLine("No buttons available on this floor.");
                }
            }
            else
            {
                Console.WriteLine("Invalid floor number.");
            }
        }

        private void HandleElevatorSelection()
        {
            if (!_building.Elevator.WaitingForSelection)
            {
                Console.WriteLine("\nElevator is not waiting for floor selection.");
                return;
            }

            Console.WriteLine("\n╔════════════════════════════════╗");
            Console.WriteLine("║  SELECT DESTINATION FLOOR     ║");
            Console.WriteLine("╠════════════════════════════════╣");
            
            for (int i = 1; i <= _building.TotalFloors; i++)
            {
                Console.WriteLine($"║  [{i}] Floor {i,-24} ║");
            }
            
            Console.WriteLine("╚════════════════════════════════╝");
            
            Console.Write("Enter floor number: ");
            if (int.TryParse(Console.ReadLine(), out int floor) && floor >= 1 && floor <= _building.TotalFloors)
            {
                _controller.HandleElevatorControlPanelPress(floor);
            }
            else
            {
                Console.WriteLine("Invalid floor number.");
            }
        }

        private void HandleStartElevator()
        {
            Console.WriteLine("\nStarting elevator...");
            _controller.ProcessElevatorLogic();
            Console.WriteLine("\nElevator returned to Floor 1.");
        }
    }
}