using System;
using System.Collections.Generic;
using ElevatorApp.Events;

namespace ElevatorApp.Models
{
    public class Elevator
    {
        private int _currentFloor;
        private Queue<int> _floorQueue;
        private bool _isMoving;
        private bool _waitingForSelection;
        private DateTime _selectionTimeout;
        private const int SelectionTimeoutSeconds = 5;

        public int CurrentFloor
        {
            get => _currentFloor;
            private set => _currentFloor = value;
        }

        public bool IsMoving
        {
            get => _isMoving;
            private set => _isMoving = value;
        }

        public bool WaitingForSelection
        {
            get => _waitingForSelection;
            private set => _waitingForSelection = value;
        }

        public event EventHandler<ElevatorEventArgs> ElevatorMoving;
        public event EventHandler<ElevatorEventArgs> ElevatorArrived;
        public event EventHandler<EventArgs> WaitingForFloorSelection;
        public event EventHandler<EventArgs> SelectionTimedOut;

        public Elevator()
        {
            _currentFloor = 1;
            _floorQueue = new Queue<int>();
            _isMoving = false;
            _waitingForSelection = false;
        }

        public void RequestFloor(int floor)
        {
            if (floor < 1 || floor > 4)
                throw new ArgumentOutOfRangeException(nameof(floor), "Floor must be between 1 and 4");

            // Only add if not already in queue
            if (!_floorQueue.Contains(floor))
            {
                _floorQueue.Enqueue(floor);
            }
        }

        public void SelectFloor(int floor)
        {
            if (!WaitingForSelection)
                return;

            _waitingForSelection = false;
            RequestFloor(floor);
        }

        public void ProcessNextRequest()
        {
            if (WaitingForSelection)
            {
                // Check if timeout has occurred
                if (DateTime.Now > _selectionTimeout)
                {
                    _waitingForSelection = false;
                    SelectionTimedOut?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    return; // Still waiting for selection
                }
            }

            if (_floorQueue.Count == 0)
            {
                // Return to first floor if no requests
                if (_currentFloor != 1)
                {
                    MoveToFloor(1);
                }
                return;
            }

            int nextFloor = _floorQueue.Dequeue();
            MoveToFloor(nextFloor);
        }

        private void MoveToFloor(int targetFloor)
        {
            if (targetFloor == _currentFloor)
            {
                ArriveAtFloor();
                return;
            }

            _isMoving = true;
            int direction = targetFloor > _currentFloor ? 1 : -1;
            string directionName = direction > 0 ? "UP" : "DOWN";

            while (_currentFloor != targetFloor)
            {
                System.Threading.Thread.Sleep(1000); // Simulate movement time
                _currentFloor += direction;

                ElevatorMoving?.Invoke(this, new ElevatorEventArgs 
                { 
                    CurrentFloor = _currentFloor,
                    Direction = directionName
                });
            }

            _isMoving = false;
            ArriveAtFloor();
        }

        private void ArriveAtFloor()
        {
            ElevatorArrived?.Invoke(this, new ElevatorEventArgs 
            { 
                CurrentFloor = _currentFloor,
                Direction = "STOPPED"
            });

            // Set waiting for selection
            _waitingForSelection = true;
            _selectionTimeout = DateTime.Now.AddSeconds(SelectionTimeoutSeconds);
            WaitingForFloorSelection?.Invoke(this, EventArgs.Empty);

            System.Threading.Thread.Sleep(2000); // Brief pause at floor
        }

        public Queue<int> GetPendingRequests()
        {
            return new Queue<int>(_floorQueue);
        }

        public bool HasPendingRequests()
        {
            return _floorQueue.Count > 0 || _waitingForSelection;
        }

        public void ClearQueue()
        {
            _floorQueue.Clear();
            _waitingForSelection = false;
        }
    }
}