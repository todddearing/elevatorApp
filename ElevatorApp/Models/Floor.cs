using System;

namespace ElevatorApp.Models
{
    public class Floor
    {
        public int FloorNumber { get; }
        public bool HasUpButton { get; }
        public bool HasDownButton { get; }

        public Floor(int floorNumber, bool hasUpButton, bool hasDownButton)
        {
            FloorNumber = floorNumber;
            HasUpButton = hasUpButton;
            HasDownButton = hasDownButton;
        }

        public override string ToString()
        {
            return $"Floor {FloorNumber}: " +
                   (HasUpButton ? "UP " : "") +
                   (HasDownButton ? "DOWN" : "");
        }
    }
}