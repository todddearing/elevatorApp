using System;
using System.Collections.Generic;

namespace ElevatorApp.Models
{
    public class Building
    {
        private List<Floor> _floors;
        public Elevator Elevator { get; }

        public Building(int numberOfFloors)
        {
            _floors = new List<Floor>();
            Elevator = new Elevator();

            // Create floors with appropriate buttons
            // Floor 1: UP button only
            // Floors 2-3: UP and DOWN buttons
            // Floor 4: DOWN button only
            for (int i = 1; i <= numberOfFloors; i++)
            {
                bool hasUp = i < numberOfFloors;
                bool hasDown = i > 1;
                _floors.Add(new Floor(i, hasUp, hasDown));
            }
        }

        public List<Floor> GetFloors()
        {
            return _floors;
        }

        public Floor GetFloor(int floorNumber)
        {
            if (floorNumber < 1 || floorNumber > _floors.Count)
                throw new ArgumentOutOfRangeException(nameof(floorNumber));

            return _floors[floorNumber - 1];
        }

        public int TotalFloors => _floors.Count;
    }
}