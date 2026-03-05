using System;

namespace ElevatorApp.Events
{
    public class ElevatorEventArgs : EventArgs
    {
        public int CurrentFloor { get; set; }
        public string Direction { get; set; }
    }
}