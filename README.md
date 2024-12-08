# Robot Simulation in Unity

This project is a Unity simulation of a basic differential drive robot navigating a randomly generated terrain. The simulation includes physics-based movement, stability control, terrain generation, and boundary walls to prevent the robot from leaving the simulation area. The robot can be controlled using keyboard inputs, and it displays real-time information such as position, speed, pitch, yaw, and roll.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Project Setup](#project-setup)
- [Scripts Overview](#scripts-overview)
  - [RobotController](#robotcontroller)
  - [RandomTerrainGenerator](#randomterraingenerator)
  - [RobotPositionAdjuster](#robotpositionadjuster)
  - [TerrainWallBuilder](#terrainwallbuilder)
- [Usage](#usage)
  - [Controls](#controls)
  - [Simulation Behavior](#simulation-behavior)
- [Customization](#customization)
- [Contributing](#contributing)
- [License](#license)

---

## Features

- **Differential Drive Robot Simulation**: Simulate a robot with forward/backward movement and rotation using keyboard inputs.
- **Physics-Based Interaction**: The robot interacts with the terrain and obstacles realistically, including collision detection and gravity.
- **Stability Control**: Monitors the robot's pitch, yaw, and roll to prevent flipping and handle flips when they occur.
- **Random Terrain Generation**: Generates a new, gently rolling terrain each time the simulation starts.
- **Automatic Robot Positioning**: Ensures the robot starts above the terrain surface to prevent falling through.
- **Boundary Walls**: Adds walls around the terrain to prevent the robot from leaving the simulation area.
- **Real-Time Debug Display**: Shows the robot's position, speed, pitch, yaw, and roll on the screen during the simulation.

---

## Getting Started

### Prerequisites

- **Unity**: Version 2019.4 or later is recommended.
- **Basic Knowledge of Unity**: Familiarity with the Unity Editor and basic concepts like GameObjects, Components, and scripting in C#.

### Project Setup

1. **Clone or Download the Project Repository**:

   ```bash
   git clone https://github.com/yourusername/robot-simulation-unity.git

2. **Open the Project in Unity**:

   - Launch Unity Hub.
   - Click on **Add** and navigate to the cloned project folder.
   - Select the project and open it.

3. **Set Up the Scene**:

   - Open the main scene (e.g., `Assets/Scenes/MainScene.unity`).
   - Ensure all scripts and assets are imported correctly.

4. **Configure the Terrain**:

   - The terrain should already be set up with the necessary components and scripts attached.
   - If not, follow the instructions in the [Scripts Overview](#scripts-overview) section.

---

## Scripts Overview

### RobotController

**Purpose**: Controls the robot's movement, handles stability logic, and displays real-time information on the screen.

**Key Features**:

- **Movement Control**: Uses keyboard inputs to move and rotate the robot.
- **Stability Monitoring**: Checks pitch, yaw, and roll to detect instability.
- **Flip Handling**: Stops movement if the robot flips and provides options to reset or await self-correction.
- **On-Screen Display**: Shows position, speed, pitch, yaw, and roll.

**Setup**:

1. **Attach the Script**:

   - Attach `RobotController.cs` to the robot GameObject.

2. **Assign References**:

   - In the Inspector, assign the `Terrain` object to the `terrain` field.

3. **Configure Parameters**:

   - Adjust `moveSpeed`, `turnSpeed`, and `stabilityThreshold` as needed.

### RandomTerrainGenerator

**Purpose**: Generates a random terrain with gentle slopes every time the simulation starts.

**Key Features**:

- **Perlin Noise Generation**: Creates natural-looking terrain using Perlin noise.
- **Smoothing Function**: Applies smoothing to reduce sharp edges and create rolling hills.
- **Dynamic Size Adaptation**: Adjusts to cover the entire terrain area.

**Setup**:

1. **Attach the Script**:

   - Attach `RandomTerrainGenerator.cs` to an empty GameObject or the terrain itself.

2. **Assign References**:

   - Assign the `Terrain` object in the Inspector.

3. **Configure Parameters**:

   - Adjust `terrainDepth`, `scale`, and smoothing iterations if necessary.

### RobotPositionAdjuster

**Purpose**: Positions the robot above the terrain surface at the start of the simulation.

**Key Features**:

- **Terrain Height Sampling**: Uses the terrain's height data to position the robot.
- **Offset Application**: Adds an offset to ensure the robot doesn't clip into the terrain.

**Setup**:

*Note: This functionality has been integrated into the `RobotController` script.*

- Ensure the `terrain` reference is assigned in the `RobotController` script.
- The robot will automatically adjust its position in the `Start()` method.

### TerrainWallBuilder

**Purpose**: Creates boundary walls around the terrain to prevent the robot from falling off the edges.

**Key Features**:

- **Automatic Wall Creation**: Generates walls based on the terrain's size.
- **Customizable Dimensions**: Allows adjustment of wall height and thickness.
- **Physical Barriers**: Walls have colliders to interact with the robot physically.

**Setup**:

1. **Attach the Script**:

   - Attach `TerrainWallBuilder.cs` to an empty GameObject.

2. **Assign References**:

   - Assign the `Terrain` object in the Inspector.

3. **Configure Parameters**:

   - Set `wallHeight` and `wallThickness` according to your needs.

---

## Usage

### Controls

- **Forward/Backward**: `W` / `S` keys
- **Turn Left/Right**: `A` / `D` keys
- **Reset Simulation**: Click the "Restart Simulation" button when prompted.

### Simulation Behavior

- **Stability Monitoring**:

  - If the robot's pitch or roll exceeds the `stabilityThreshold`, it will stop moving.
  - An on-screen prompt will appear if the robot flips, offering the option to reset.

- **Self-Correction**:

  - If the robot rights itself, a message will display for 10 seconds before auto-closing.

- **Terrain Generation**:

  - A new terrain is generated each time you start the simulation, providing varied environments.

- **Boundary Walls**:

  - Walls prevent the robot from leaving the terrain area, ensuring the simulation stays within bounds.

---

## Customization

- **Adjusting Movement Speeds**:

  - Modify `moveSpeed` and `turnSpeed` in the `RobotController` script.

- **Changing Stability Threshold**:

  - Adjust `stabilityThreshold` to make the robot more or less sensitive to flipping.

- **Terrain Settings**:

  - In `RandomTerrainGenerator`, tweak `terrainDepth`, `scale`, and the smoothing function to change terrain roughness.

- **Wall Dimensions**:

  - In `TerrainWallBuilder`, alter `wallHeight` and `wallThickness` to adjust the size of the boundary walls.

- **Visual Appearance**:

  - Add textures or materials to the terrain and walls to enhance visual fidelity.

---

## Contributing

Contributions are welcome! Please open an issue or submit a pull request with your improvements.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

