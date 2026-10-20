# CMP6206: Artificial Intelligence for Games
## Coursework Project: Tactical Infiltration AI Simulation

**Student Name:** Pablo Martín Navarro  
**Institution:** Birmingham City University (BCU)  
**Module:** CMP6206 - Artificial Intelligence for Games (Semester 1, 2026/2027)  
**Module Leader:** Jan Krasniewicz  

---

### 1. Project Concept & Overview
This project is a 3D tactical infiltration and stealth AI simulation developed in Unity. The player navigates a high-security warehouse/facility while avoiding autonomous guard agents. 

The primary goal is to implement custom-built, modular game AI algorithms from first principles (without relying on Unity's built-in `NavMeshAgent` decision logic), focusing on transparent decision-making, physical and line-of-sight awareness, and graph-based spatial navigation.

---

### 2. Formal PEAS Specification (Russell & Norvig Framework)
To design the guard agents as rational decision-makers, their task environment is formulated under the **PEAS** framework:

* **Performance Measure (P):**
  * Detection efficiency: Time taken to spot and react to intruders.
  * Patrol coverage: Consistency in traversing assigned waypoints without leaving blind spots.
  * Search effectiveness: Rapid investigation of sound/disturbance sources and strategic clearing of last known positions.
* **Environment (E):**
  * 3D warehouse facility with dynamic obstacles (crates, pillars, walls).
  * Characteristics: *Partially Observable* (guard vision is obstructed by walls), *Dynamic* (player moves in real-time), *Continuous* (3D positions and physics-based vectors), *Multi-agent* (adversarial dynamic between guards and player).
* **Actuators (A):**
  * Kinematic/Vector-based translation and angular rotation.
  * State visualizers (dynamic field-of-view color transitions: Green = Patrol, Yellow = Investigate, Red = Alert/Chase).
  * Audio alerts and alert broadcasts to neighbouring units.
* **Sensors (S):**
  * **Line-of-Sight & Vision:** Multi-ray conical bundle (`Physics.Linecast` sweep across horizontal field-of-view).
  * **Auditory Sensor:** Proximity and volume queries (`Physics.OverlapSphere` / `SphereCast`) to detect player movement disturbances.
  * **Obstacle Clearance:** Spatial validation queries to detect walkable clearance around corners.

---

### 3. Planned AI Architecture
The agent implementation follows a modular, decoupled architecture structured across core coursework themes:

1. **Spatial Representation & Pathfinding (Weeks 1 & 4):**
   * Grid-based spatial discretization (`Grid` and `Node` structures) scanning the 3D geometry for walkable/unwalkable cells.
   * Pathfinding using **Uniform-Cost Search (UCS / Dijkstra)** on weighted terrains (e.g., normal floors vs noisy metallic surfaces/puddles) before progressing to heuristic search (**A***).
2. **Perception Layer (Week 3):**
   * Raycast bundles calculating orientation offsets via `Quaternion.AngleAxis` on the local $XZ$ plane.
   * Geometric visibility checks combining vector dot product ($\vec{u} \cdot \vec{v}$) for FOV filtering and raycasting for occlusion handling.
3. **Decision-Making Layer (Week 2):**
   * **State Design Pattern (FSM):** Encapsulated state classes (`PatrolState`, `InvestigateState`, `ChaseState`, `SearchState`) with explicit `Enter()`, `Update()`, and `Exit()` lifecycle actions.
   * Extension towards a **Stack-Based FSM (Pushdown Automaton)** to enable transient memory (e.g., investigating a noise and returning to the exact interrupted patrol route without state corruption).

---

### 4. Milestone 1 Status (21/10/2026)
* [x] Project repository initialized and public for evaluation.
* [x] Formal project design document and PEAS framework defined.
* [x] Initial Unity greybox scene setup with Player and Guard agent anchors.
* [x] Basic vector-based player movement and multi-ray vision sensor visualization.
