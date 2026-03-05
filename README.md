# Elevator Simulation App

A C# console application that simulates the operation of an elevator system with event-driven architecture.

## Overview

This application demonstrates how elevators use events and event handlers to process floor calls and passenger requests. The elevator queues requests in the order they're received and services them sequentially while handling timeouts and returning to the ground floor when idle.

## Features

- **4-Floor Building Simulation**: Complete building layout with elevator shaft
- **Realistic Floor Buttons**: 
  - Floor 1: UP button only
  - Floors 2-3: UP and DOWN buttons
  - Floor 4: DOWN button only
- **Elevator Control Panel**: Buttons for all floors inside the elevator
- **Request Queuing**: All floor requests are queued and processed in order
- **Floor Selection Timeout**: 5-second timeout for passengers to select a floor
- **Return to Ground Floor**: Elevator automatically returns to Floor 1 when no requests pending

## Architecture

### Event-Driven Design

The application implements single event handlers for multiple button types:

- **Floor Buttons Handler**: One handler processes all UP/DOWN button presses from all floors
- **Control Panel Handler**: One handler processes all floor selection buttons in the elevator
- **Elevator Events**: Multiple events for movement, arrival, and timeout scenarios

### Project Structure

```
ElevatorApp/
├── Models/
│   ├── Elevator.cs          # Elevator logic and event definitions
│   ��── Building.cs          # Building layout and floor management
│   └── Floor.cs             # Individual floor information
├── Events/
│   ├── ElevatorEventArgs.cs # Custom event arguments for elevator
│   └── FloorRequestEventArgs.cs # Custom event arguments for floor requests
├── Services/
│   └── ElevatorController.cs # Handles events and controls elevator
└── UI/
    └── ElevatorUI.cs        # Console UI for user interaction
```

## Usage

### Running the Application

```bash
dotnet run
```

### Calling the Elevator

1. Select "Call elevator from floor" from the main menu
2. Choose which floor you want to call from
3. Select the direction (if applicable for that floor)
4. The elevator will queue your request

### Selecting a Floor

1. Wait for the elevator to arrive at your floor
2. The UI will indicate it's waiting for floor selection
3. Select "Select floor in elevator" from the main menu
4. Choose your destination floor
5. The elevator will proceed to that floor

### Starting the Elevator

Select "Start elevator" to begin processing all queued requests. The elevator will:
- Travel to each requested floor in order
- Wait up to 5 seconds for passengers to select their destination
- Move to the selected floor
- Return to Floor 1 when all requests are complete

## User Stories Implemented

✅ User can see a cross section diagram of a building with four floors, an elevator shaft, the elevator, and appropriate buttons on each floor.

✅ User can see the elevator control panel with a button for each floor.

✅ User can click the up and down button on any floor to call the elevator.

✅ User can expect that clicking buttons on any floor will queue requests in sequence.

✅ User can see the elevator move up and down the shaft to called floors.

✅ User can click the elevator control panel to select a destination floor.

✅ User can expect the elevator to pause for 5 seconds waiting for a floor button. If not clicked within that time, it will process the next request.

✅ User can expect the elevator to return to the first floor when there are no requests.

## Design Patterns Used

- **Event Pattern**: Implements the observer pattern for elevator events
- **Queue Pattern**: Uses a Queue<T> to manage sequential floor requests
- **State Pattern**: Elevator tracks movement and waiting states
- **Singleton Pattern**: Single Elevator and Building per application

## Technical Details

- **Language**: C# (.NET 6.0)
- **UI**: Console-based with ASCII art diagrams
- **Threading**: Uses thread sleep for simulating movement delays
- **Events**: Custom EventArgs for passing elevator state information

## Future Enhancements

- Add multiple elevators
- Implement intelligent elevator scheduling
- Add passenger simulation
- Create graphical UI
- Add weight/capacity limits
- Implement emergency features

## License

MIT