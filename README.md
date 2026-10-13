# 🎢 Simulation de parc d'attractions – Projet Unity

**A theme park crowd simulation in Unity, where hundreds of visitors find their way, queue up, and enjoy the rides.**

> 🚧 **Project in progress**: Unity

## About

This project is a Unity3D simulation of a theme park. Visitors walk between points of interest (POIs), wait in naturally forming queues, visit attractions, and avoid each other and obstacles using NavMesh pathfinding.

## Goals

- Gain experience with Unity3D
- Reverse-engineer existing features by copying them first
- Understand pathfinding algorithms, tools, and documentation

## Features

**Core**

- POIs with editable settings: visit time, entrance and exit positions, max simultaneous visitors
- Static obstacles: POIs and a large lake, on a Unity Terrain
- Steep slopes treated as non-walkable
- Queues that form naturally and snake around
- Realistic avoidance between visitors (about 1-2 m, except in queues)
- Support for heavy load (more than 100 agents)
- Runtime buttons: **+25 visitors** and **+25 crowd**


## Tech

- **Unity** 2021.1.2f1 (versions from 2018.1 onward should work)
- **AI Navigation package** (NavMesh), or NavMesh Components on older versions
- No Visual Scripting (C# only)

## Getting Started

1. Clone the repository
2. Open the project in Unity 2021.1.2f1 or later
3. Open the main scene and press **Play**
4. Use the on-screen buttons to add visitors and crowd